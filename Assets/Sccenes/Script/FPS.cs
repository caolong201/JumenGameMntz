using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FPS : MonoBehaviour {
	public Text txtFps;

	int frameCount = 0;
	float dt = 0.0f;
	float fps = 0.0f;
	float updateRate = 4.0f;  // 4 updates per sec.
	void Awake()
	{
		Application.targetFrameRate = 60;
	}
	void Update()
	{
		frameCount++;
		dt += Time.deltaTime;
		if (dt > 1.0f/updateRate)
		{
			fps = frameCount / dt ;
			txtFps.text = fps.ToString();
			frameCount = 0;
			dt -= 1.0f/updateRate;
		}
	}
}
