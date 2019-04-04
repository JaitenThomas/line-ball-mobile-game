using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GapObjectPool : MonoBehaviour {


	public int columnPoolSize = 5;
	public GameObject columnPrefab;
	public GameObject starPrefab;

	public float columnXMin = -3.0f;
	public float columnXMax = 3.0f;

	private GameObject[] columns;
	private GameObject[] stars;

	private Vector2 objectPoolPosition = new Vector2 (-15, -25);

	private float spawnYPosition = -10f;
	private int currentColumn = 0;
	private int currentStar = 0;

	void Start()
	{
		columns = new GameObject[columnPoolSize];
		stars = new GameObject[columnPoolSize];

		for (int i = 0; i < columnPoolSize; i++) {
			columns [i] = (GameObject)Instantiate (columnPrefab, objectPoolPosition, Quaternion.identity);
			stars [i] = (GameObject)Instantiate (starPrefab, objectPoolPosition, Quaternion.identity);
		}

		for (int i = 0; i < 3; i++) {
			SpawnObstacle ();
		}
	}

	public void SpawnObstacle(){
		
		float spawnXPosition = Random.Range (columnXMin, columnXMax);
		columns [currentColumn].transform.position = new Vector2 (spawnXPosition, spawnYPosition);

	

		int chance = Random.Range (0, 9);
		if(chance == 1){
			stars [currentStar].gameObject.GetComponent<SpriteRenderer> ().enabled = true;
			stars [currentStar].transform.position = new Vector2 (spawnXPosition, spawnYPosition);
			currentStar++;
		}
		spawnYPosition -= Random.Range(5.0f, 6.0f);

		currentColumn++;


		if(currentColumn >= columnPoolSize){
			currentColumn = 0;

		}

		if(currentStar >= columnPoolSize){
			currentStar = 0;
		}
	}
}
