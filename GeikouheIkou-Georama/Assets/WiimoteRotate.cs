using UnityEngine;
using System.Collections;
using WiimoteApi;

public class WiimoteRotate : MonoBehaviour {

	Wiimote wiimote;

	// Use this for initialization
	void Start () {
		if (wiimote == null) {
			Debug.Log ("hote");
			if (WiimoteManager.FindWiimotes ()) {
				wiimote = WiimoteManager.Wiimotes [0];
				Debug.Log ("Wiimote Found");
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (WiimoteManager.HasWiimote ()) {
			//transform.rotation = Quaternion.LookRotation(new Vector3(wiimote.MotionPlus.YawSpeed,wiimote.MotionPlus.PitchSpeed,wiimote.MotionPlus.RollSpeed));

			int ret = wiimote.ReadWiimoteData ();

			if (ret > 0 && wiimote.current_ext == ExtensionController.MOTIONPLUS) {
				Vector3 offset = new Vector3 (-wiimote.MotionPlus.PitchSpeed,
					                 wiimote.MotionPlus.YawSpeed,
					                 wiimote.MotionPlus.RollSpeed) / 95f; // Divide by 95Hz (average updates per second from wiimote)
			
				transform.Rotate (offset);
			}
		}
	}
}
