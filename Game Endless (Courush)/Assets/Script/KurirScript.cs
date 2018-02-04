using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class KurirScript : MonoBehaviour {

	[SerializeField]
	Transform[] waypoints;

	[SerializeField]
	float moveSpeed = 3f;

	int waypointIndex = 0;

	// Use this for initialization
	void Start () {
		transform.position = waypoints [waypointIndex].transform.position;

	}

	// Update is called once per frame
	void Update () {
		Move();

	}

	void Move(){

		transform.position = Vector2.MoveTowards (transform.position,
			waypoints [waypointIndex].transform.position, moveSpeed * Time.deltaTime);

		if (transform.position == waypoints [waypointIndex].transform.position) {
			waypointIndex += 1;
		}

		if (waypointIndex == waypoints.Length)
			waypointIndex = 0;
	}
}
