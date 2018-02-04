﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDestroyer : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "TargetPhase")
        {
            Destroy(collision.transform.parent.gameObject);
        }
    }

}
