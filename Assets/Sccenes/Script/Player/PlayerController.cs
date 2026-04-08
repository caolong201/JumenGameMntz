using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum PlayerPos
{
	None,
	Left,
	Right
}

public class PlayerController : MonoBehaviour
{

	private Vector3 startPos;
	private float _distance;
	public bool canJump;
	public GameObject effectPlayerAutoRun;
	private bool isMove = false;
	private bool isChangeState = false;
	public PlayerPos playerNewState = PlayerPos.None;
	private PlayerPos playerCurrState = PlayerPos.Left;

	void Start ()
	{
		//load player prefabs
		GameObject go =	AvGameObjectUtils.LoadGameObject (transform, GameConstance.PathOfPlayerPrefabs+AvPlayerManager.Instance.CurrentPlayerID);
		//change Bg
		BGManager.Instance.RandomBG ();
		ResetData ();
	}

	private float timeToPlayerAutoRun = GameConstance.TimeToPlayerAutoRun;
	private float timeToPlayerAutoRunNextStep = GameConstance.TimeToPlayerAutoRunNextStep;
	// Update is called once per frame
	void Update ()
	{
		//truong hop player tu chay
		if (PlayerState.Instance.playerCurrState == State.AutoRun) {
			if (effectPlayerAutoRun != null && effectPlayerAutoRun.activeSelf == false)
				effectPlayerAutoRun.SetActive (true);

			timeToPlayerAutoRun -= Time.deltaTime;
			timeToPlayerAutoRunNextStep -= Time.deltaTime;
			if (timeToPlayerAutoRunNextStep <= 0) {
				timeToPlayerAutoRunNextStep = GameConstance.TimeToPlayerAutoRunNextStep;
				PlatformManager.isCreateNewPlatform = true;
				ScoreManager.Instance.IncreaseScore ();
			}

			transform.position += new Vector3 (.15f, 0, 0);
			if (timeToPlayerAutoRun <= 0) {//dung lai choi binh thuong
				if (effectPlayerAutoRun != null && effectPlayerAutoRun.activeSelf == true)
					effectPlayerAutoRun.SetActive (false);

				//lam tron vi tri cho de tinh sau nay
				transform.position = new Vector3 (Mathf.Round (transform.position.x), transform.position.y, transform.position.z);
				//reser lai time
				timeToPlayerAutoRun = GameConstance.TimeToPlayerAutoRun;
				timeToPlayerAutoRunNextStep = GameConstance.TimeToPlayerAutoRunNextStep;
				PlayerState.Instance.playerCurrState = State.Live;
			}
			//=====================


		} else if (Input.GetMouseButtonDown (0) && PlayerState.Instance.playerCurrState == State.Live) {

			if (TutorialsManager.Instance.isTutorialing) {
				if(TutorialsManager.Instance.tutorialSate == TutorialState.Step1)
				{
					if (Input.mousePosition.x > Screen.width / 2) {
						return;
					}
				} else if(TutorialsManager.Instance.tutorialSate == TutorialState.Step2)
				{
					if (Input.mousePosition.x < Screen.width / 2) {
						return;
					}
				}else if(TutorialsManager.Instance.tutorialSate == TutorialState.Step3)
				{
					if (Input.mousePosition.x > Screen.width / 2) {
						return;
					}
				}
				TutorialsManager.Instance.currentStepCount += 1;
				//Debug.LogError(TutorialsManager.Instance.currentStepCount);
				TutorialsManager.Instance.isCreated = false;

			}
			PlayerState.Instance.isFistTap = true;
			canJump = true;
			//xac dinh tab trai hay phai
			if (Input.mousePosition.x > Screen.width / 2) {
				//Debug.Log("Right");
				playerNewState = PlayerPos.Right;
				//transform.position =new Vector3(transform.position.x, transform.position.y,0);
			} else {
				//Debug.Log("Left");
				playerNewState = PlayerPos.Left;
				//transform.position =new Vector3(transform.position.x, transform.position.y,1.5f);
			}
			//kiem tra xem co thay doi trang thai hay hk
			if (playerCurrState != playerNewState) {
				isChangeState = true;
			} else {
				isMove = true;
				PlatformManager.isCreateNewPlatform = true;
				ScoreManager.Instance.IncreaseScore ();
			}
			startPos = transform.position;
		
		}

		if (isMove) { //player di chuyen ve phia truoc


			transform.position += new Vector3(.15f, 0, 0);
			_distance = transform.position.x - startPos.x;
			if (_distance >= 1) {
				//play sound
				SoundManager.Instance.PlayerMoveForward();
				//lam tron vi tri cho de tinh sau nay
				transform.position = new Vector3 (Mathf.Round (transform.position.x), transform.position.y, transform.position.z);
				isMove = false;
				canJump = false;
				// cong them 1 diem cho user
					
			}
				
		} else if (isChangeState) { // player nhay qua trai hoac phai
			if (playerNewState == PlayerPos.Right) {
				transform.position -= new Vector3 (0, 0, .2f);
				if (transform.position.z <= .05f) {
					transform.position = new Vector3 (transform.position.x, transform.position.y, .05f);
					isChangeState = false;
					canJump = false;
					playerCurrState = playerNewState;
				}
			} else if (playerNewState == PlayerPos.Left) {
				transform.position += new Vector3 (0, 0, .2f);
				if (transform.position.z >= 1.5f) {
					transform.position = new Vector3 (transform.position.x, transform.position.y, 1.5f);
					isChangeState = false;
					canJump = false;
					playerCurrState = playerNewState;
				}
			}
				
		}
	} 
	/// <summary>
	/// data nao muon reset thi dua vao day
	/// </summary>
	void ResetData ()
	{
		ScoreManager.Instance.Score = 0;
		PlatformManager.newTimePlatfromdown = GameConstance.TimePlatformDown_Start;
	}
 

}
