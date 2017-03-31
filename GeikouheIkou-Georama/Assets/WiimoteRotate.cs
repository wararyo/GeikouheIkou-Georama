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
			wiimote.SendDataReportMode (InputDataType.REPORT_EXT21);
			if(wiimote.ActivateWiiMotionPlus ()) Debug.Log("WiiMotionPlus Activated");
			wiimote.SendPlayerLED (true, false, false, false);
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
					Vector3 offset = new Vector3 (-wiimote.MotionPlus.RollSpeed,
						                wiimote.MotionPlus.YawSpeed,
						                -wiimote.MotionPlus.PitchSpeed) / 100f; // Divide by 95Hz (average updates per second from wiimote)
					//rotation += offset;
					//Debug.Log ("MOTIONPLUS" + offset.ToString("F3"));
					if(offset.magnitude > 0.05f) transform.Rotate (offset, Space.Self);
					if (Input.GetKeyDown(KeyCode.C)) {
						Debug.Log ("Callibrate");
						CallibrateWiimote ();
						transform.rotation = new Quaternion(0,0,0,0);
					}
					else if(Input.GetKeyDown(KeyCode.B)){
						UpdateBatteryStatus();
					}
				}
			} while(ret > 0);
		}
	}

	void CallibrateWiimote(){
		wiimote.MotionPlus.SetZeroValues ();
	}

	public void UpdateBatteryStatus(){
		wiimote.SendStatusInfoRequest ();
		Debug.Log ("Battery : " + (wiimote.Status.battery_level / 2.56) + "%");
		if (wiimote.Status.battery_low)
			wiimote.SendPlayerLED (true, true, true, true);
	}
}
