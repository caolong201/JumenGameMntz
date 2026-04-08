using UnityEngine;
using System.Collections;
[RequireComponent (typeof (AudioSource))]
public class SoundManager : SingletonMonoAwake<SoundManager> {

	// List of AudioClips
	public AudioSource ButtonClickAudio;
	public AudioSource BackgroundMusicAudio;
	public AudioSource tab;
	public AudioSource mistake;
	public AudioSource changeDirection;
	public AudioSource boom;
	public float maxvolumeBGM = .2f;
	public override void OnAwake ()
	{
		base.OnAwake ();
		PlayBackgroundMusic();
	}
	
	// Method used to play a specific sound/audio clip
	public void PlayChangeDirection()
	{
		if(changeDirection !=null)
		{
			if(SaveDataManager.Instance.GetSound())
			{
				changeDirection.volume =0.4f;
				changeDirection.Play();	
			}
			else
				changeDirection.Stop();
		}
	}
	public void PlayBoom()
	{
		if(boom !=null)
		{
			if(SaveDataManager.Instance.GetSound())
			{
				boom.volume =1f;
				boom.Play();	
			}
			else
				boom.Stop();
		}
	}
	public void PlayButtonClick()
	{
		// Play Audio Clip
		if(ButtonClickAudio !=null)
		{
			if(SaveDataManager.Instance.GetSound())
			{
				ButtonClickAudio.volume =0.4f;
				ButtonClickAudio.Play();	
			}
			else
				ButtonClickAudio.Stop();
		}
				
	}
	public void PlayerHadMistake()
	{
		if(mistake !=null)
		{
			if(SaveDataManager.Instance.GetSound())
			{
				mistake.Play();
			}
		}
	}
	public void PlayerMoveForward()
	{
		// Play Audio Clip
		if(tab !=null)
		{
			if(SaveDataManager.Instance.GetSound())
			{
				tab.Play();
			}
		}
		
	}
	// Method used to play the background music
	public void PlayBackgroundMusic()
	{
		if(BackgroundMusicAudio != null)
		{
			if(SaveDataManager.Instance.GetBGM())
			{
				// Assign the background music to the audio component
				//BackgroundMusicAudio.clip = BackgroundMusicAudio;
				
				// Allow the background music to be played continuously
				BackgroundMusicAudio.loop = true;
				BackgroundMusicAudio.volume = 0;
				// Play the background music
				BackgroundMusicAudio.Play(); 
				StartCoroutine(FadeInBGM(maxvolumeBGM));
			}
			else
				StartCoroutine(FadeOutBGM(maxvolumeBGM));
		}
	}

	IEnumerator FadeInBGM(float volumeMax, float volumeMin = 0)
	{
		yield return new WaitForSeconds(0.05f);
		volumeMin+= 0.025f;
		//Debug.Log (volumeMin);
		if (volumeMin < volumeMax) {
			BackgroundMusicAudio.volume = volumeMin;
			StartCoroutine (FadeInBGM (volumeMax, volumeMin));
		} else
			BackgroundMusicAudio.volume = volumeMax;
	}
	IEnumerator FadeOutBGM(float volumeMax)
	{
		yield return new WaitForSeconds(0.05f);
		volumeMax-= 0.025f;
		Debug.Log (volumeMax);
		if (volumeMax > 0) {
			BackgroundMusicAudio.volume = volumeMax;
			StartCoroutine (FadeOutBGM (volumeMax));
		} else
			BackgroundMusicAudio.volume = 0;
	}
}
