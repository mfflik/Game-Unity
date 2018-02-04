using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DawnDetector : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.name == "early")
        {
            collision.GetComponentInParent<SpriteRenderer>().sprite = GameController.Instance.TargetNightSprites[collision.GetComponentInParent<TargetController>().idxSprite];
        }
    }
}
