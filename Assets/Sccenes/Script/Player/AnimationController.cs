using UnityEngine;
using System.Collections;

public class AnimationController : MonoBehaviour {

	Animator ani;
	PlayerController player;
	// Use this for initialization
	void Start () {

		ani = gameObject.transform.GetComponentInChildren<Animator> ();
		player = GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {

		if (player != null && ani !=null) {

			if(player.canJump)
			{
				ani.SetBool("Jump",true);
			}
			else
			{
				ani.SetBool("Jump",false);
			}
		}
		else
		{
			ani = gameObject.transform.GetComponentInChildren<Animator> ();
			player = GetComponent<PlayerController>();
		}
	
	}
}
