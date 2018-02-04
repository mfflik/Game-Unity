using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScript : MonoBehaviour {

	bool isJump = true;
	private Rigidbody2D _rigidbody;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space))
			Jump ();
		
		
	}

	private void OnCollisionStay2D(Collision2D collision){
		if (isJump) {
			isJump = false;
		}
	
	}

	private void OnCollisionExit2D(Collision2D collision){
		isJump = true;
	}


	public void Jump(){
		if (!isJump) {
			if (GetComponent<Rigidbody2D> ().velocity.y < 1)
				gameObject.GetComponent<Rigidbody2D> ().AddForce (Vector2.up * 230f);
		}
	
	}
}
