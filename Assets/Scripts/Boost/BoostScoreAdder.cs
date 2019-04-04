using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostScoreAdder : MonoBehaviour {

	public BoostObjectPool boostObjectPool;

	// Use this for initialization
	void Start () {
        boostObjectPool = FindObjectOfType<BoostObjectPool> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag == "Ball"){
			UIManager.Instance.AddScore ();
			GameObject.FindObjectOfType<DrawLine>().speed += 0.02f;
            boostObjectPool.SpawnObstacle ();
            Destroy(this);
		}
	}
}
