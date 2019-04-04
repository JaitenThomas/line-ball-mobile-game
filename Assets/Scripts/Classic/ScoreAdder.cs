using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreAdder : MonoBehaviour {

	public ObjectPool objectPool;

	// Use this for initialization
	void Start () {
		objectPool = FindObjectOfType<ObjectPool> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag == "Ball"){
			UIManager.Instance.AddScore ();
			other.GetComponent<PlayerController> ().speed += 0.02f;
			objectPool.SpawnObstacle ();
		}
	}
}
