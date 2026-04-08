using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BGManager : SingletonMonoAwake<BGManager> {

	public Image BG;
	public Sprite[] listBg;
	void Start()
	{
		//Debug.LogError ("edfbeberbe");
		BG.GetComponent<RectTransform> ().sizeDelta = new Vector2 (Screen.width, Screen.height);
	}
	public void RandomBG()
	{
		if(listBg.Length >=2)
		{
			int rand = UnityEngine.Random.Range (0,listBg.Length);
			BG.sprite = listBg[rand];
		}
	}
}
