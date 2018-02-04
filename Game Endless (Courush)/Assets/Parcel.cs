using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Parcel : MonoBehaviour {

    public Sprite Item;
    public bool IsRevealed = true;

    private void Start()
    {
        IsRevealed = true;
    }

    public void Init(Sprite item)
    {
        Item = item;
        Reveal();
    }

    public void Interact()
    {
        if (IsRevealed)
        {
            var name = Item.name;
            //Item = null;
            //IsRevealed = false;
            //GetComponent<Image>().sprite = Resources.Load<Sprite>("Parcel/closed");
            CourierController.Instance.ProcessParcel(name);
            
        }
        else
        {
            IsRevealed = true;
            Reveal();
        }
    }

    public void Reveal()
    {
        GetComponent<Image>().sprite = Item;
    }
}
