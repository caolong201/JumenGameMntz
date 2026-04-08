using UnityEngine;
using System.Collections;

public enum CameraPos
{
	Left,
	Right
}
public class CameraFollow : MonoBehaviour {
	public GameObject target;
	private Vector3 shouldPos;
	
	private float targetAngle = 0;
	const float rotationAmount = 3f;
	public float rDistance = 1.0f;
	public float rSpeed = 15.0f;
	private bool isRandom = true;
	public static bool isChangeCamera =false;
	public CameraPos CameraPos =CameraPos.Right;

	void Start()
	{
		if(target == null)
			target = GameObject.FindGameObjectWithTag (GameConstance.PlayerTag);
	}
	// Update is called once per frame
	void Update () {
	
		if (target != null ) {
			if(PlayerState.Instance.playerCurrState == State.Live || PlayerState.Instance.playerCurrState == State.AutoRun )
			{
				//Debug.LogError("bgerbertb");
				shouldPos = Vector3.Lerp (transform.position,target.transform.position,Time.deltaTime*12);
				transform.position = new Vector3 (shouldPos.x, transform.position.z,.8f);
				//transform.position = new Vector3(target.transform.position.x, transform.position.z, .8f);
				if (PlayerState.Instance.playerCurrState == State.AutoRun)
				{
					
					if (isRandom) {
						isRandom = false;
						int rand = UnityEngine.Random.Range (0, 2);
						
						if (rand == 0) {
							targetAngle -= 360.0f;
						} else if (rand == 1) {
							targetAngle += 360.0f;
						}

					}
					isChangeCamera =false;
					
				}else if (isChangeCamera)//thay doi goc nhin camera
				{
					isChangeCamera =false;
					if (CameraPos ==CameraPos.Right) {
						CameraPos = CameraPos.Left;
						targetAngle -= 60;//phai la So chia het cho 3 nhe
                   
                    }
					else if (CameraPos == CameraPos.Left) {
						CameraPos = CameraPos.Right;
						targetAngle += 60;
					}
				} 
				
				//rotate camera
				if (targetAngle != 0) {
					Rotate();
					//Debug.LogWarning(targetAngle);
				}
				else
				{
					isRandom =true;
				}
			}
		}
		else if(PlayerState.Instance.playerCurrState == State.Live)
		{
			//Debug.LogError("tai sao hk zo day");
			target = GameObject.FindGameObjectWithTag (GameConstance.PlayerTag);
			if(PlayerState.Instance.isFistTap ==false)
				transform.position = new Vector3 (target.transform.position.x, transform.position.z,.8f);
		}
				
	}
	private void Rotate()
	{
		float step = rSpeed * Time.deltaTime;
		float orbitCircumfrance = 2F * rDistance * Mathf.PI;
		float distanceDegrees = (rSpeed / orbitCircumfrance) * 360;
		float distanceRadians = (rSpeed / orbitCircumfrance) * 2 * Mathf.PI;

		if (targetAngle > 0)
		{
			transform.RotateAround(target.transform.position, Vector3.up, -rotationAmount);
			targetAngle -= rotationAmount;
		}
		else if (targetAngle < 0)
		{
			transform.RotateAround(target.transform.position, Vector3.up, rotationAmount);
            targetAngle += rotationAmount;

        }

	}

}
