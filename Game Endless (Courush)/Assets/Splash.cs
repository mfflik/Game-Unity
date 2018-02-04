﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Splash : MonoBehaviour {

    private void Start()
    {
        StartCoroutine(CloseSplash());
    }

    IEnumerator CloseSplash()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(Global.SCENE_GAMEPLAY);
    }
}
