using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCameraControls : MonoBehaviour {

	public Transform target;
	public Vector2 offset = Vector2.zero;
	public float speed = 10.0f;


	void LateUpdate(){
		transform.position = Vector3.Lerp (transform.position,
			new Vector3 (target.position.x + offset.x, target.position.y + offset.y,
				transform.position.z), Time.deltaTime * speed);
	
	
}
}
