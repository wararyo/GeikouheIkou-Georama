using UnityEngine;
using System.Collections;
using WiimoteApi;

public class WiimoteRotate : MonoBehaviour {

	Wiimote wiimote;

	private Vector3 rotation = Vector3.zero;

	// Use this for initialization
	void Start () {
		if (wiimote == null) {
			WiimoteManager.FindWiimotes ();
			wiimote = WiimoteManager.Wiimotes [0];
			wiimote.RequestIdentifyWiiMotionPlus ();
			wiimote.SendDataReportMode (InputDataType.REPORT_BUTTONS_EXT19);
			if(wiimote.ActivateWiiMotionPlus ()) Debug.Log("WiiMotionPlus Activated");
			wiimote.SendPlayerLED (true, false, false, false);
			CallibrateWiimote ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (wiimote != null) {
			//transform.rotation = Quaternion.LookRotation(new Vector3(wiimote.MotionPlus.YawSpeed,wiimote.MotionPlus.PitchSpeed,wiimote.MotionPlus.RollSpeed));
			int ret;
			do {
				ret = wiimote.ReadWiimoteData ();
				if (ret > 0 && wiimote.current_ext == ExtensionController.MOTIONPLUS) {
					Vector3 offset = new Vector3 (wiimote.MotionPlus.PitchSpeed,
						                -wiimote.MotionPlus.YawSpeed,
						                wiimote.MotionPlus.RollSpeed) / 95f; // Divide by 95Hz (average updates per second from wiimote)
					//rotation += offset;
					//Debug.Log ("MOTIONPLUS" + offset.ToString("F3"));
					transform.Rotate (offset, Space.Self);
					if (wiimote.Button.home) {
						Debug.Log ("Home Pressed");
						CallibrateWiimote ();
					}
				}
			} while(ret > 0);
		}
	}

	void CallibrateWiimote(){
		StartCoroutine (CallibrateWiimote_raw ());
	}

	IEnumerator CallibrateWiimote_raw(){
		yield return new WaitForSeconds (3f);
		wiimote.MotionPlus.SetZeroValues ();
	}

	void UpdateBatteryStatus(){
		wiimote.SendStatusInfoRequest ();
		Debug.Log ("Battery : " + (wiimote.Status.battery_level / 2.56) + "%");
		if (wiimote.Status.battery_low)
			wiimote.SendPlayerLED (true, true, true, true);
	}
}
