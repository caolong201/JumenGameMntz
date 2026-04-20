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
		Transform hitTransform = other.transform;
		Transform enemyTransform = FindParentByTag(hitTransform, GameConstance.EnemyTag);
		if (enemyTransform != null)
		{
			if (PlayerState.Instance.playerCurrState == State.AutoRun) 
			{
				BarrierItem barrierItem = enemyTransform.GetComponent<BarrierItem>();
				if (barrierItem != null)
					barrierItem.Boom(true);
				SoundManager.Instance.PlayBoom();
			}
			else
			{
				SoundManager.Instance.PlayerHadMistake();
				playerCurrState = State.PrepareDeath;
				//tinh toan tien tu score khj die
				AvPlayerManager.Instance.CaculatorMoney(ScoreManager.Instance.Score);
			}
			return;
		}

		Transform bonusTransform = FindParentByTag(hitTransform, GameConstance.BonusTag);
		if (bonusTransform != null)
		{
			playerCurrState = State.AutoRun;
			Destroy(bonusTransform.gameObject, 0.03f);
			return;
		}

		Transform changeCameraTransform = FindParentByTag(hitTransform, GameConstance.ChangeCameraTag);
		if (changeCameraTransform != null) {
			//change camera
			CameraFollow.isChangeCamera =true;
			Destroy(changeCameraTransform.gameObject,0.03f);
			SoundManager.Instance.PlayChangeDirection();
		}
	}

	private Transform FindParentByTag(Transform trans, string tagName)
	{
		Transform current = trans;
		while (current != null)
		{
			if (current.CompareTag(tagName))
				return current;

			current = current.parent;
		}

		return null;
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
