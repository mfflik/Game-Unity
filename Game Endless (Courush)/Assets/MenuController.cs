using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {
    public Text Title1, Title2;
	
	// Update is called once per frame
	void Update () {
        if(BackgroundScroller.Instance.transform.position.x < -20 && BackgroundScroller.Instance.transform.position.x > -40)
        {
            Title1.color = Color.white;
            Title2.color = Color.white;
        }
        else
        {
            Title1.color = Color.black;
            Title2.color = Color.black;
        }
	}

    public void Play()
    {
        GameController.Instance.SwitchGameplay();
    }

    public void About()
    {

    }

    public void Exit()
    {
        Application.Quit();
    }
}
