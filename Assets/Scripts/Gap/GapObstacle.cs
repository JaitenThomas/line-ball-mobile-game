using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GapObstacle : MonoBehaviour {

	public GameObject left;
	public GameObject right;
	public Color color;

	// Use this for initialization
	void Start () {
		///color = GameManager.Instance.allColors [Random.Range (0, GameManager.Instance.allColors.Length)];
		left.GetComponent<SpriteRenderer> ().color = color;
		right.GetComponent<SpriteRenderer> ().color = color;
	}
	
	// Update is called once per frame
	void Update ()
    {
       
        right.GetComponent<SpriteRenderer>().color = left.GetComponent<SpriteRenderer>().color;
    }
}
