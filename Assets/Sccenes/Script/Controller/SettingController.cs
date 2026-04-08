using UnityEngine;
using System.Collections;

public class SettingController : MonoBehaviour {

	Animator ani;
	bool isShow =false;
	public GameObject btnRealease;
	public GameObject btnSoundOn, btnSoundOff, btnBMOn, btnBMOff;
	public void OnbtnSettingClicked()
	{
		SoundManager.Instance.PlayButtonClick ();



		isShow = !isShow;
		if (isShow)
		{
			btnRealease.SetActive (true);
			bool stateSound = SaveDataManager.Instance.GetSound ();
			//change image
				//sound
			if(stateSound)
			{
				
				SoundOn();
			}
			else
			{
				StartCoroutine(WaitingAniSettingShowSoundOff(.45f));
			}
				//BGM
			bool stateBGM = SaveDataManager.Instance.GetBGM ();

			if(stateBGM)
			{
				
				BGMOn();
			}
			else
			{
				StartCoroutine(WaitingAniSettingShowBGMOff(.45f));
			}
		}	
		else
		{
			btnRealease.SetActive (false);
			//for animation
			btnSoundOn.SetActive(true);
			btnSoundOff.SetActive(false);
			btnBMOn.SetActive(true);
			btnBMOff.SetActive(false);
		}
			
		if(ani == null)
			ani = gameObject.GetComponent<Animator> ();

		ani.SetBool ("settingShow", isShow);
	}
	public void OnbtnRealeaseClicked()
	{
		isShow = false;
		//for animation
		SoundOn ();
		BGMOn ();
		if(ani == null)
			ani = gameObject.GetComponent<Animator> ();
		
		ani.SetBool ("settingShow", isShow);
		btnRealease.SetActive (false);
	}
	public void OnbtnSoundClicked()
	{

		//save state sound
		bool state = SaveDataManager.Instance.GetSound ();
		state = !state;
		SaveSound (state);
		SoundManager.Instance.PlayButtonClick();
		//change image
		if(state)
		{

			SoundOn();

		}
		else
		{
			SoundOff();
		}
		//Debug.LogError(state);
	}
	public void OnbtnBGMClicked()
	{

		//save state sound
		bool state = SaveDataManager.Instance.GetBGM ();
		state = !state;
		SaveBGM (state);
		SoundManager.Instance.PlayBackgroundMusic ();
		//change image
		if(state)
		{
			BGMOn();

		}
		else
		{
			BGMOff();
		}
		Debug.LogError(state);		
	}
	void SoundOn()
	{
		btnSoundOn.SetActive(true);
		btnSoundOff.SetActive(false);
	}
	void SoundOff()
	{
		btnSoundOn.SetActive(false);
		btnSoundOff.SetActive(true);
	}
	void BGMOn()
	{
		btnBMOn.SetActive(true);
		btnBMOff.SetActive(false);
	}
	void BGMOff()
	{
		btnBMOn.SetActive(false);
		btnBMOff.SetActive(true);
	}
	void SaveBGM(bool state)
	{
		if(state)
			SaveDataManager.Instance.SaveBGM (1);
		else
			SaveDataManager.Instance.SaveBGM (0);
	}
	void SaveSound(bool state)
	{
		if(state)
			SaveDataManager.Instance.SaveSound (1);
		else
			SaveDataManager.Instance.SaveSound (0);
	}
	IEnumerator WaitingAniSettingShowSoundOff(float time)
	{
		yield return new WaitForSeconds(time);
		SoundOff ();
	}
	IEnumerator WaitingAniSettingShowBGMOff(float time)
	{
		yield return new WaitForSeconds(time);
		BGMOff ();
	}
}
