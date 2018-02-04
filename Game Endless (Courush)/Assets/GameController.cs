using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    private static GameController _Instance;

    public static GameController Instance
    {
        get
        {
            return _Instance;
        }
    }

    private void Awake()
    {
        _Instance = this;
    }

    public Sprite[] TargetSprites;
    public Sprite[] TargetNightSprites;
    public GameObject PrefabTarget, PlayArea, Courier, CanvasGameplay, CanvasMenu;
    public Transform startTarget;
    public Text TextMoney, TextEnergy;

    public GameOverController GameOver;
    public BackgroundScroller Background;
    public PauseController PauseGame;

    public int Money;
    public int Energy;
    public int MaxEnergy = 100;
    public float TargetSpeed = 0.1f;
    public float CurrentTime = 6;
    public int PhaseTime = 6;
    public bool TimeEnabled = true;
    public bool IsGameplay = false;

    void SoundClick()
    {
        AudioManager.Instance.PlaySound(AudioManager.SOUND_BUTTON_CLICK);
    }

	// Use this for initialization
	void Start () {
        if (IsGameplay)
        {
            SwitchGameplay();
        }
        else
        {
            SwitchMainMenu();
        }
    }

    public List<int> AmbientDay, AmbientNight;
    public bool dayAmbient = true;

    public void GenerateAmbientDay()
    {
        AmbientDay = new List<int>();
        AmbientDay.Add(-1);
        AmbientDay.Add(AudioManager.SOUND_DAY_AMBULANCE);
        AmbientDay.Add(AudioManager.SOUND_DAY_CAT);
        AmbientDay.Add(AudioManager.SOUND_DAY_DOG);
    }

    public void GenerateAmbientNight()
    {
        AmbientNight = new List<int>();
        AmbientNight.Add(-1);
        AmbientNight.Add(AudioManager.SOUND_NIGHT_CROW);
        AmbientNight.Add(AudioManager.SOUND_NIGHT_KUNTI);
        AmbientNight.Add(AudioManager.SOUND_NIGHT_OWL);
        AmbientNight.Add(AudioManager.SOUND_NIGHT_WOLF);

    }

    public void SwitchGameplay()
    {
        AudioManager.Instance.StopMusic();
        CanvasGameplay.SetActive(true);
        CanvasMenu.SetActive(false);
        AudioManager.Instance.StopMusic();
        AudioManager.Instance.PlayMusic(AudioManager.MUSIC_GAMEPLAY);
        Button[] gs = GameObject.FindObjectsOfType<Button>();
        foreach (Button b in gs)
        { 
            b.onClick.AddListener(SoundClick);
        }

        AudioManager.Instance.PlayMusic(AudioManager.AMBIENT_WALKING);
        AudioManager.Instance.PlaySound(AudioManager.SOUND_GAME_START);
        Money = 0;
        Energy = MaxEnergy;
        InvokeRepeating("CreateRandomTarget", 0f, 0.9f);
        InvokeRepeating("RunOut", 0f, 0.05f);
        GenerateAmbientDay();
        GenerateAmbientNight();
    }

    public void SwitchMainMenu()
    {
        AudioManager.Instance.StopMusic();
        AudioManager.Instance.PlayMusic(AudioManager.MUSIC_MENU);
        CanvasGameplay.SetActive(false);
        CanvasMenu.SetActive(true);
    }
	
	void CreateRandomTarget()
    {
        GameObject g = Instantiate(PrefabTarget, startTarget.position, Quaternion.identity, PlayArea.transform);
        int idx = Random.Range(0, 7);
        g.GetComponent<TargetController>().idxSprite = idx;
        g.GetComponent<SpriteRenderer>().sprite = TargetSprites[idx];
        Random.InitState(System.Environment.TickCount);
        g.GetComponent<TargetController>().ExpectedSprite.sprite = ParcelController.Instance.SelectedParcels[Random.Range(0, ParcelController.Instance.SelectedParcels.Count)];
        Random.InitState(System.Environment.TickCount);
        if (Money > 2000)
        {
            if (Random.Range(0, 4) == 0)
            {
                StartCoroutine(DelayedCreateRandomTarget());
            }
        }
    }

    IEnumerator DelayedCreateRandomTarget()
    {
        yield return new WaitForSeconds(0.3f);
        CreateRandomTarget();
    }

    void RunOut()
    {
        if (TimeEnabled)
        {
            CurrentTime += 0.01f;
            if (CurrentTime >= 24f)
            {
                CurrentTime = 0f;
            }
            
            if(PhaseTime == 6)
            {
                if (CurrentTime >= 18f && CurrentTime <= 19f)
                {
                    TimeEnabled = false;
                    PhaseTime = 18;
                    CurrentTime = 18f;
                }
            }
            else
            {
                if (CurrentTime >= 6f && CurrentTime <= 7f)
                {
                    TimeEnabled = false;
                    PhaseTime = 6;
                    CurrentTime = 6f;
                }
            }
        }
        TargetSpeed += 0.0001f;
        if (Courier.transform.position.x < 4.11f)
        {
            Courier.transform.position = new Vector2(Courier.transform.position.x + 0.005f, Courier.transform.position.y);
        }
        Energy -= 1;
        TextEnergy.text = "Energy : " + Energy.ToString();
        if(Energy <= 0)
        {
            CancelInvoke("RunOut");
            EndGame();
        }
    }

    public void TargetHit(int money, int energy)
    {
        Energy += energy;
        if(Energy > 100)
        {
            Energy = 100;
        }
        //TextMoney.text = "Money : " + Money.ToString();
        MoneyBar.Instance.Add(money);
        //TextEnergy.text = "Energy : " + Energy.ToString();
        //ParcelController.Instance.Randomize();
    }

    public void EndGame()
    {
        AudioManager.Instance.PlaySound(AudioManager.SOUND_GAME_END);
        CancelInvoke("CreateRandomTarget");
        TargetController[] Found = GameObject.FindObjectsOfType<TargetController>();
        foreach(TargetController t in Found)
        {
            t.Moving = false;
        }
        Found = null;
        Background.enabled = false;
        GameOver.Init(Money);
    }

    public void TogglePause()
    {
        if(Time.timeScale == 1f)
        {
            TargetController[] g = GameObject.FindObjectsOfType<TargetController>();
            foreach (TargetController item in g)
            {
                item.Moving = false;
            }
            PauseGame.Init();
            Background.enabled = false;
            Time.timeScale = 0f;
            
        }
        else
        {
            TargetController[] g = GameObject.FindObjectsOfType<TargetController>();
            foreach (TargetController item in g)
            {
                item.Moving = true;
            }
            PauseGame.Close();
            Background.enabled = true;
            Time.timeScale = 1f;
        }
    }

}
