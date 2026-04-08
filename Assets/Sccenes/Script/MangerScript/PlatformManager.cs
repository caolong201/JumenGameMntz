using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlatformManager : MonoBehaviour
{
	public static bool isCreateNewPlatform = false;
	public GameObject[] platforms;
	private int nextPos = -3;
	private int index = 0;
	private int countToCreateBarrier = 0;
	private Transform player;

	//const
	float timePlatformdown = GameConstance.TimePlatformDown_Start;
	public static  float newTimePlatfromdown = GameConstance.TimePlatformDown_Start;
	float timePlatformAutodown = GameConstance.TimePlatformAutodown;
	public static int countPos = 0;
	int random ;


	void Start ()
	{ 
		//random platforms
		random = UnityEngine.Random.Range (0, 3);
		//tao platform ban dau
		for (int i =0; i<8; i++) {
			CreateNewPlatform ();
		}
		InvokeRepeating ("PlatformDown", 1.5f, .001f);
		player = GameObject.FindGameObjectWithTag (GameConstance.PlayerTag).transform;
		if (player == null) {
			Debug.LogError ("Player null");
			return;
		}
	}	

	void Update ()
	{
		if (isCreateNewPlatform) {
			//khi player di chuyen thi tao tiep
			CreateNewPlatform ();
		}
	}

	private void CreateNewPlatform ()
	{
		countToCreateBarrier += 1;
		isCreateNewPlatform = false;
		nextPos += 1;
		index += 1;

		//tao platform
		GameObject newPlatform = Instantiate (platforms[random], new Vector3 (nextPos + 3, platforms[random].transform.position.y, platforms[random].transform.position.z), Quaternion.identity) as GameObject;
		newPlatform.name = GameConstance.PlatformName + index.ToString ();
		newPlatform.GetComponent <PlatformItem> ().InitNewPlatform (nextPos);
		if (countToCreateBarrier == 2) {
			//tao barrier
			newPlatform.GetComponent<PlatformItem> ().Init ();
			countToCreateBarrier = 0;	
		}
	}
	int indexDown = 1;
	/// <summary>
	/// Platforms down.
	/// </summary>
	void PlatformDown ()
	{
		if (TutorialsManager.Instance.isTutorialing == false) {
			if (PlayerState.Instance.isFistTap == true && PlayerState.Instance.playerCurrState == State.Live && !CameraFollow.isChangeCamera) {
				timePlatformdown -= Time.deltaTime;
				if (timePlatformdown <= 0) {
					timePlatformdown = newTimePlatfromdown;
					FindPlatform ();
					indexDown += 1;
				}
			} else if (PlayerState.Instance.playerCurrState == State.AutoRun) {
				timePlatformAutodown -= Time.deltaTime;
				if (timePlatformAutodown <= 0) {
					timePlatformAutodown = GameConstance.TimePlatformAutodown;
					FindPlatform ();
					indexDown += 1;
				}
			} 
		}
		
	}
	/// <summary>
	/// Finds the platform.
	/// </summary>
	void FindPlatform ()
	{

		//dowm
		GameObject obj = GameObject.Find ("Platform_" + indexDown.ToString ());
		if (obj != null) {
			obj.GetComponent<PlatformItem> ().isFall = true;
			Transform barrier = obj.GetComponent<Transform> ().transform;
			if (barrier != null) 
			{
				if ( player != null)
				{
					if (barrier.position.x >= player.position.x) 
					{// neu player chay hk kip thi die haha
						PlayerState.Instance.playerCurrState = State.PrepareDeath;
						
					}
				}
				else
					player	= GameObject.FindGameObjectWithTag (GameConstance.PlayerTag).transform;
				//huy platform sau 1 giay
				Destroy (obj, 1f);
			}
				
		} else
			Debug.LogError ("hk tim thay name: " + "Platform_" + indexDown.ToString ());

	}
}
