using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectObjectPool : MonoBehaviour {


	public int columnPoolSize = 1;
	public GameObject starPrefab;

	public float spawnXPosition = 1.0f;
	public float spawnYPosition = 2.5f;

	private GameObject[] stars;

	private Vector2 objectPoolPosition = new Vector2 (-15, -25);

	private int currentColumn = 0;
	private int currentStar = 0;

	void Start()
	{
		stars = new GameObject[columnPoolSize];

		for (int i = 0; i < columnPoolSize; i++) {
			stars [i] = (GameObject)Instantiate (starPrefab, objectPoolPosition, Quaternion.identity);
		}


		//SpawnObstacle ();

	}

	public void SpawnObstacle(){


		float x = Random.Range (-spawnXPosition, spawnXPosition);
		float y = Random.Range (-spawnYPosition, spawnYPosition);

		stars [currentStar].gameObject.GetComponent<SpriteRenderer> ().enabled = true;
		stars [currentStar].gameObject.GetComponent<PolygonCollider2D> ().enabled = true;

		stars [currentStar].transform.position = new Vector2 (x, y);

		currentStar++;

		if(currentStar >= columnPoolSize){
			currentStar = 0;
		}
	}
}
