using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopScoreAdder : MonoBehaviour {

	public HoopObjectPool hoopObjectPool;

	// Use this for initialization
	void Start () {
		hoopObjectPool = FindObjectOfType<HoopObjectPool> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag == "Ball"){
			UIManager.Instance.AddScore ();
			other.GetComponent<PlayerController> ().speed += 0.01f;
			hoopObjectPool.SpawnObstacle ();
            transform.parent.GetComponent<Animator>().SetTrigger("Score");
		}
	}
}
