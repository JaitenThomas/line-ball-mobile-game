using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangerParent : MonoBehaviour {


    private SpriteRenderer sr;
    private MeshRenderer pm;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        pm = transform.parent.GetComponent<MeshRenderer>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        sr.color = pm.material.color;
	}
}
