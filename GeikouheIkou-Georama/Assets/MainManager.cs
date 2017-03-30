using UnityEngine;
using System.Collections;
using wararyo.EclairCueMaker;
using UnityEngine.UI;

public class MainManager : MonoBehaviour {

	public CueScenePlayer cueScenePlayer;
	public TimeCounter timeCounter;
	public Text remainTimeText;

	public CueScene cueSceneStart;
	public CueScene cueSceneTimeUp;

	// Use this for initialization
	void Start () {
		timeCounter.OnTime += OnTimeCounterTime;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Fire1")) {
			cueScenePlayer.Play (cueSceneStart);
		}
		if (timeCounter.isCounting) {
			remainTimeText.text = timeCounter.RemainTimeString;
		}
	}

	private void OnTimeCounterTime(){
		cueScenePlayer.Play (cueSceneTimeUp);
	}
}
