using UnityEngine;
using System.Collections;


public enum State
{
	None,
	Idle,
	Live,
	AutoRun,
	PrepareDeath,
	Death,
}
public class PlayerState : SingletonMono<PlayerState> {
	//[HideInInspector]
	public State playerCurrState;
	public bool isFistTap = false;//tap lan dau tien 
	Animator ani;
	void Update()
	{
		//Debug.LogError (playerCurrState);
		if (playerCurrState == State.PrepareDeath)
		{
			playerCurrState = State.None;
			PlayerDeath();
		}

	}
	void OnTriggerEnter(Collider other) 
	{
		if(other.gameObject.tag == GameConstance.EnemyTag)
		{
			if (PlayerState.Instance.playerCurrState == State.AutoRun) 
			{
				other.gameObject.GetComponent<BarrierItem>().Boom(true);
				SoundManager.Instance.PlayBoom();
			}
			else
			{
				SoundManager.Instance.PlayerHadMistake();
				playerCurrState = State.PrepareDeath;
				//tinh toan tien tu score khj die
				AvPlayerManager.Instance.CaculatorMoney(ScoreManager.Instance.Score);
			}
				
		}
		else if(other.gameObject.tag == GameConstance.BonusTag)
		{
			playerCurrState = State.AutoRun;
			//Debug.Log("Bonus");
		}
		else if (other.gameObject.tag == GameConstance.ChangeCameraTag) {
			//change camera
			CameraFollow.isChangeCamera =true;
			Destroy(other.gameObject,0.03f);
			SoundManager.Instance.PlayChangeDirection();
		}
	}
	private void PlayerDeath()//khi nao muon player die thi goi ham nay
	{
		ani = gameObject.transform.GetComponentInChildren<Animator> ();
		if (ani != null) {
		
			ani.SetBool ("Death", true);
			Destroy (transform.GetComponentInChildren<Animator> ().gameObject, 2f);
			//ani.StopPlayback();
			StartCoroutine (WaitingAnimationDeath (1.9f));
			Debug.Log("PLayer Death");
		} else
			Debug.LogError ("Animation was null");
	}
	IEnumerator WaitingAnimationDeath(float time)
	{
		yield return new WaitForSeconds (time);
		playerCurrState = State.Death;
	}
}
