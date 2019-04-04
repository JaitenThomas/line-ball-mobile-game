using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Size : MonoBehaviour {

    private SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Start ()
    {
        //Size the object
        float size = Random.Range(1.0f, 2.5f);
        gameObject.transform.localScale = new Vector2(size, size);
        
    }
}
