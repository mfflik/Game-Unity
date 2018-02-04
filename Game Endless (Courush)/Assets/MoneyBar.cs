using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyBar : MonoBehaviour {

    private static MoneyBar _Instance;

    public static MoneyBar Instance
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

    public Text TextMoney;

    public void Add(int money)
    {
        GetComponent<Animator>().SetTrigger("Run");
        GameController.Instance.Money += money;
        TextMoney.text = "$"+GameController.Instance.Money.ToString("N0");
    }
}
