using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour {

    public const string SCORE_CURRENT = "current";
    public const string SCORE_BEST = "best";

    public Text TextCurrent;
    public Text TextBest;

    public Sprite Silver, Gold;
    public Image Piala;

	public void Init(int money)
    {
        PlayerPrefs.SetInt(SCORE_CURRENT, money);
        var best = PlayerPrefs.GetInt(SCORE_BEST, 0);
        Piala.sprite = Silver;
        if (money > best)
        {
            Piala.sprite = Gold;
            PlayerPrefs.SetInt(SCORE_BEST, money);
            best = money;
            AudioManager.Instance.PlaySound(AudioManager.SOUND_NEW_SCORE_1);
            AudioManager.Instance.PlaySound(AudioManager.SOUND_NEW_SCORE_2);
        }
        TextCurrent.text = "Current Money : " + money;
        TextBest.text = "Best Money : " + best;

        gameObject.transform.parent.gameObject.SetActive(true);
        gameObject.SetActive(true);
        PlayerPrefs.Save();
    }

    public void Restart()
    {
        SceneManager.LoadScene(Global.SCENE_GAMEPLAY);
    }
}
