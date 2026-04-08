using UnityEngine;
using System.Collections;

public class BarrierItem : MonoBehaviour {
	private Vector3 _vec3 = Vector3.zero;
	private Vector3 _newVec3 = Vector3.zero;
	private bool _isHit = false;
	public GameObject effectBoom;

	public void Init(Vector3 vec3, float newPos)
	{
		_vec3 = vec3;
		_newVec3.y = newPos;

	}
	// Update is called once per frame
	void Update ()
	{
		if(_isHit)
		{
			GameObject obj = Instantiate(effectBoom) as GameObject;
					obj.transform.position = transform.position;
			Destroy(gameObject);
			Destroy(obj,1f);
			
		}

		if (_vec3.y > _newVec3.y) 
		{
			if(transform.position.y <= _newVec3.y+.4f)
				return;
			transform.position -= new Vector3(0,.1f,0);
		}
	}
	public void Boom(bool isHit)
	{
		_isHit = isHit;
	}
}
