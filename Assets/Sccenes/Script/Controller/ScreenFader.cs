using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFader : MonoBehaviour {

	public Image FadeImg;
	public float fadeSpeed = 1.5f;
	private bool sceneStarting =true ;
	private bool sceneEnding = false;
	private int _sceneNumber=0;
	void Awake()
	{
		FadeImg.rectTransform.localScale = new Vector2(Screen.width, Screen.height);

	}
	
	void Update()
	{
		// If the scene is starting...
		if (sceneStarting)
			StartScene ();
		else if (sceneEnding)
			EndScene (_sceneNumber);
	}
	
	
	void FadeToClear()
	{
		// Lerp the colour of the image between itself and transparent.
		FadeImg.color = Color.Lerp(FadeImg.color, Color.clear, fadeSpeed * Time.deltaTime);
	}
	
	
	void FadeToBlack()
	{
		// Lerp the colour of the image between itself and black.
		FadeImg.color = Color.Lerp(FadeImg.color, Color.black, fadeSpeed * Time.deltaTime);
	}
	
	/// <summary>
	/// Starts the scene.
	/// </summary>
	void StartScene()
	{

		// Make sure the RawImage is enabled.
		FadeImg.enabled = true;
		// Fade the texture to clear.
		FadeToClear();

		// If the texture is almost clear...
		if (FadeImg.color.a <= 0.05f)
		{
			// ... set the colour to clear and disable the RawImage.
			FadeImg.color = Color.clear;
			FadeImg.enabled = false;
			
			// The scene is no longer starting.
			sceneStarting = false;
		}
	}
	
	/// <summary>
	/// Ends the scene.
	/// </summary>
	/// <param name="SceneNumber">Scene number.</param>
	void EndScene(int SceneNumber)
	{
		// Make sure the RawImage is enabled.
		FadeImg.enabled = true;
		
		// Start fading towards black.
		FadeToBlack();

		// If the screen is almost black...
		if (FadeImg.color.a >= 0.9f)
		{
			FadeImg.color = Color.black;
			sceneEnding = false;
            // ... reload the level
            Application.LoadLevel(SceneNumber);
            sceneStarting = true;
		}
			
	}
	public void ScreenFadeIn()
	{
		sceneStarting = true;

	}
	public void ScreenFadeOut(int sceneNumber=0)
	{
		_sceneNumber = sceneNumber;
		sceneEnding = true;
	}
}
