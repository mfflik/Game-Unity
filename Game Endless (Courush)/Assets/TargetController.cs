using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour {

    public int idxSprite = -1;
    public bool Moving = true;
    public SpriteRenderer ExpectedSprite;
    public SpriteRenderer CloudIndicator;
    public bool Sent = false;
    
	// Update is called once per frame
	void Update () {
        if (Moving)
        {   
            transform.position = new Vector2(transform.position.x - GameController.Instance.TargetSpeed, transform.position.y);
        }
	}
}
