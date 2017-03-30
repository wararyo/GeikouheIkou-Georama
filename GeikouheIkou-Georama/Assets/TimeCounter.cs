using UnityEngine;
using System.Collections;
using wararyo.EclairCueMaker;
using System;

public class TimeCounter : CueEventBase {

	public string RemainTimeString {
		get{
			return RemainTime.ToString ("F2");
		}
	}

	public float RemainTime{
		get{
			return Mathf.Clamp(defaultTime - (Time.realtimeSinceStartup - whenStarted),0,defaultTime);
		}
	}

	public System.Action OnTime;

	public float defaultTime = 60;

	private float whenStarted = 0;

	[System.NonSerialized]
	public bool isCounting = false;

	public override string EventName
	{
		get
		{
			return "Start TimeCounter";
		}
	}

	public override string EventID
	{
		get
		{
			return "gjc66rgy9awr33r94tjopy5";
		}
	}

	public override Type ParamType
	{
		get
		{
			return typeof(void);
		}
	}


	public override void Cue(object param)
	{
		//ここに任意の処理を記述
		whenStarted = Time.realtimeSinceStartup;
		isCounting = true;
	}

	public void Update(){
		if (isCounting && RemainTime <= 0) {
			isCounting = false;
			OnTime ();
		}
	}

}
