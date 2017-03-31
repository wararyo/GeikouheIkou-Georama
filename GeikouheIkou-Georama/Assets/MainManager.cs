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
	public CueScene cueSceneEnd;

	public enum GameState
	{
		Title,
		Playing,
		Result
	}

	public GameState gameState = GameState.Title;

	// Use this for initialization
	void Start () {
		timeCounter.OnTime += OnTimeCounterTime;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Fire1")) {
			switch (gameState) {
			case GameState.Title:
				cueScenePlayer.Play (cueSceneStart);
				gameState = GameState.Playing;
				break;
			case GameState.Playing:

				break;
			case GameState.Result:
				cueScenePlayer.Play (cueSceneEnd);
				gameState = GameState.Title;
				break;
			}
		}
		if (timeCounter.isCounting) {
			remainTimeText.text = timeCounter.RemainTimeString;
		}
	}

	private void OnTimeCounterTime(){
		cueScenePlayer.Play (cueSceneTimeUp);
		gameState = GameState.Result;
	}
}
