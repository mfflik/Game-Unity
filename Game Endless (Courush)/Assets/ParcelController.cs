using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParcelController : MonoBehaviour {

    private static ParcelController _Instance;

    public static ParcelController Instance
    {
        get
        {
            return _Instance;
        }
    }

    public Sprite[] Parcels;
    public List<Sprite> SelectedParcels;

    private void Awake()
    {
        _Instance = this;
        List<int> Idxs = new List<int>();
        for (int i = 0; i < Parcels.Length; i++)
        {
            Idxs.Add(i);
        }
        while (SelectedParcels.Count < 4)
        {
            var idx = Idxs[Random.Range(0, Idxs.Count)];
            SelectedParcels.Add(Parcels[idx]);
            Idxs.Remove(idx);
        }
    }

	// Use this for initialization
	void Start () {
        Randomize();
    }
	
	public void Randomize()
    {
        
        List<string> Available = new List<string>();
        Dictionary<string, int> ParcelsDict = new Dictionary<string, int>();
        for (int i = 0; i < SelectedParcels.Count; i++)
        {
            Available.Add(SelectedParcels[i].name);
            ParcelsDict.Add(SelectedParcels[i].name, i);
        }
        foreach (Transform t in transform)
        {
            if(t.GetComponent<Parcel>().Item != null)
            {
                var name = t.GetComponent<Parcel>().Item.name;
                if (Available.Contains(name))
                {
                    Available.Remove(name);
                }
            }
        }
        foreach (Transform t in transform)
        {
            if (t.GetComponent<Parcel>().Item == null)
            {
                Random.InitState(System.Environment.TickCount);
                var idx = Random.Range(0, Available.Count);
                
                t.GetComponent<Parcel>().Init(SelectedParcels[ParcelsDict[Available[idx]]]);
                Available.RemoveAt(idx);
            }
        }


    }
}
