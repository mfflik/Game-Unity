using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour {

    public Image Fill;

    void Update()
    {
        Fill.fillAmount = (float)GameController.Instance.Energy / GameController.Instance.MaxEnergy;
    }
}
