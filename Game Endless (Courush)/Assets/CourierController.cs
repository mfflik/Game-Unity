using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourierController : MonoBehaviour {

    private static CourierController _Instance;

    public static CourierController Instance
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

    public TargetController CurrentTarget;
    public string CurrentPhase;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "TargetPhase")
        {
            CurrentTarget = collision.GetComponentInParent<TargetController>();
            
            CurrentPhase = collision.gameObject.name;
            if (!collision.GetComponentInParent<TargetController>().Sent) {
                switch (CurrentPhase)
                {
                    case "early":
                        collision.GetComponentInParent<TargetController>().CloudIndicator.color = Color.yellow;
                        break;
                    case "perfect":
                        collision.GetComponentInParent<TargetController>().CloudIndicator.color = Color.green;
                        break;
                    case "late":
                        collision.GetComponentInParent<TargetController>().CloudIndicator.color = Color.red;
                        break;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "late")
        {
            CurrentPhase = "";
            CurrentTarget = null;
        }

    }

    public void ProcessParcel(string parcel)
    {
        GetComponent<Animator>().SetTrigger("Throw");
        float max = float.Parse(GameController.Instance.MaxEnergy.ToString());
        var money = 0;
        var energy = 0;
        if (CurrentTarget != null && CurrentTarget.ExpectedSprite.sprite.name == parcel)
        {
            CurrentTarget.Sent = true;
            switch (CurrentPhase)
            {
                case "early":
                    money = 50;
                    energy = Mathf.FloorToInt(0.15f * max);
                    break;
                case "perfect":
                    money = 100;
                    energy = Mathf.FloorToInt(0.25f * max);
                    break;
                case "late":
                    money = 25;
                    energy = Mathf.FloorToInt(0.5f * max);
                    break;
            }
            AudioManager.Instance.PlaySound(AudioManager.SOUND_TARGET_SUCCESS);
            Random.InitState(System.Environment.TickCount);
            var name = CurrentTarget.GetComponent<SpriteRenderer>().sprite.name;
            if(name.Split('_')[1] == "d")
            {

                if (GameController.Instance.dayAmbient)
                {
                    var i = Random.Range(0, GameController.Instance.AmbientDay.Count);
                    var idx = GameController.Instance.AmbientDay[i];
                    Debug.Log(idx);
                    if (idx > -1)
                    {
                        AudioManager.Instance.PlaySound(idx);
                        AudioManager.Instance.ChangeSoundVolume(idx, 0.5f);
                        GameController.Instance.AmbientDay.RemoveAt(i);
                        GameController.Instance.dayAmbient = false;
                    }
                    if (GameController.Instance.AmbientDay.Count == 0)
                    {
                        GameController.Instance.GenerateAmbientDay();
                    }
                }
            }
            else
            {
                if (!GameController.Instance.dayAmbient)
                {
                    var i = Random.Range(0, GameController.Instance.AmbientNight.Count);
                    var idx = GameController.Instance.AmbientNight[i];
                    Debug.Log(idx);
                    if (idx > -1)
                    {
                        AudioManager.Instance.PlaySound(idx);
                        AudioManager.Instance.ChangeSoundVolume(idx, 0.5f);
                        GameController.Instance.AmbientNight.RemoveAt(i);
                        GameController.Instance.dayAmbient = true;
                    }
                    if (GameController.Instance.AmbientNight.Count == 0)
                    {
                        GameController.Instance.GenerateAmbientNight();
                    }
                }
            }
        }
        else
        {
            
            if (CurrentTarget == null)
            {
                energy = -Mathf.FloorToInt(0.15f * max);
            }
            else
            {
                energy = -Mathf.FloorToInt(0.25f * max);
            }
            AudioManager.Instance.PlaySound(AudioManager.SOUND_TARGET_WRONG);
        }
        GameController.Instance.TargetHit(money, energy);
    }
}
