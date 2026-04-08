using UnityEngine;
using System.Collections;

public class ScoreManager : SingletonMono<ScoreManager> {



	private int score = 0;
	private int highScore = 0;
	[HideInInspector]
	public bool isSavehighScore = false;
	public int Score
	{
		set
		{
			score = value;
			if(score > highScore)
			{
				highScore = score;
				isSavehighScore = true;
			}			
		}
		get
		{
			//Debug.Log(score);
			return score;
		}
	}
	public int HighScore
	{
		get
		{
			return highScore;
		}
	}
	public void IncreaseScore()
	{
		Score += 1;
		 if (Score == 50) {
			PlatformManager.newTimePlatfromdown = GameConstance.TimePlatformDown_Over50;
		} else if (Score == 200) {
			PlatformManager.newTimePlatfromdown = GameConstance.TimePlatformDown_Over200;
		} else if (Score == 300) {
			PlatformManager.newTimePlatfromdown = GameConstance.TimePlatformDown_Over300;
		}
	}
	void Start()
	{
		highScore = PlayerPrefs.GetInt ("HighScore",0);
		//Debug.Log ("dvdfsvver");
	}
	void OnDestroy()
	{
		PlayerPrefs.SetInt ("HighScore", highScore);
		//Debug.Log ("dsgdfhrt");
	}
}
