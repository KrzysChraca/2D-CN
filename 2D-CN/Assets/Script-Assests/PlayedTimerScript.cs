using UnityEngine;
using System.Collections;

public class PlayedTimerScript : MonoBehaviour {

	float timer;
	string timerFormatted;
	float pSecond;

	// Use this for initialization
	void Start () {
	
		timer = 0.0f;

	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (Timer ());

	}


	string Timer(){
	
		timer += Time.deltaTime;

		float pSecond = (timer % 3600f) % 60f;

		System.TimeSpan t = System.TimeSpan.FromSeconds(pSecond);
		timerFormatted = string.Format("{0:D2}h:{1:D2}m:{2:D2}s:{3:D3}ms", t.Hours, t.Minutes, t.Seconds, t.Milliseconds);
		return timerFormatted;

	}

	void StopTimer(){
		//Create function to stop timer when in pause menu or when exiting game.
	}

	void SaveTimer(){	
		//create function to save timer.
	}
}
