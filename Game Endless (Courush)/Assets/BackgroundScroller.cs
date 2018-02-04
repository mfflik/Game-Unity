using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour {

    private static BackgroundScroller _Instance;

    public static BackgroundScroller Instance
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

    public float scrollSpeed;
    public float tileSizeZ;
    public Material Day, Night, Dusk, Dawn;

    private Vector3 startPosition;

    void Start()
    {
        //Resize();
        startPosition = transform.position;
        
    }

    void Update()
    {
        float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSizeZ);
        var Vector = startPosition + Vector3.right * newPosition;
        if (!float.IsNaN(Vector.x))
        {
            transform.position = startPosition + Vector3.right * newPosition;
        }
    }

    void Resize()
    {
        Renderer sr = GetComponent<Renderer>();
        if (sr == null) return;

        transform.localScale = new Vector3(1, 1, 1);

        float width = sr.bounds.size.x;
        float height = sr.bounds.size.y;


        float worldScreenHeight = Camera.main.orthographicSize * 2f;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;


        Vector3 xSize = transform.localScale;
        xSize.x = worldScreenWidth / width;
        xSize.y = worldScreenHeight / height;
        transform.localScale = xSize;

        //transform.localScale.y = worldScreenHeight / height;

    }
}
