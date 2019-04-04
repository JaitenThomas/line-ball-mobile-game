using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GapScoreAdder : MonoBehaviour {

	public GapObjectPool gapObjectPool;

	// Use this for initialization
	void Start () {
		gapObjectPool = FindObjectOfType<GapObjectPool> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag == "Ball"){
			UIManager.Instance.AddScore ();
			other.GetComponent<PlayerController> ().speed += 0.01f;
			gapObjectPool.SpawnObstacle ();
		}
	}
}
