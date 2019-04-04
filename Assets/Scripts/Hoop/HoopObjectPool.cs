using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopObjectPool : MonoBehaviour {


	public int columnPoolSize = 5;
	public GameObject columnPrefab;
	public GameObject starPrefab;

	public float columnXMin = -1.0f;
	public float columnYMax = 1.0f;

	private GameObject[] columns;
	private GameObject[] stars;

	private Vector2 objectPoolPosition = new Vector2 (-15, -25);

	private float spawnYPosition = -10f;
	public int currentColumn = 0;
	private int currentStar = 0;

	void Start()
	{
		columns = new GameObject[columnPoolSize];
		stars = new GameObject[columnPoolSize];

		for (int i = 0; i < columnPoolSize; i++) {
			columns [i] = (GameObject)Instantiate (columnPrefab, objectPoolPosition, Quaternion.identity);
			stars [i] = (GameObject)Instantiate (starPrefab, objectPoolPosition, Quaternion.identity);
		}

		for (int i = 0; i < 5; i++) {
			SpawnObstacle ();
		}
	}

	public void SpawnObstacle(){

        columns[currentColumn].GetComponent<Animator>().Play("Idle_Hoop");


        float spawnXPosition = Random.Range (columnXMin, columnYMax);
		columns [currentColumn].transform.position = new Vector2 (spawnXPosition, spawnYPosition);
		columns [currentColumn].transform.eulerAngles = new Vector3 (60, 0, 0);

		int chance = Random.Range (0, 9);
		if(chance == 10){
			stars [currentStar].gameObject.GetComponent<SpriteRenderer> ().enabled = true;
			stars [currentStar].transform.position = new Vector2 (spawnXPosition, spawnYPosition);
			currentStar++;
		}

		spawnYPosition -= Random.Range(3.5f, 5.0f);

		currentColumn++;



        if (currentColumn >= columnPoolSize){
			currentColumn = 0;
            
        }

		if(currentStar >= columnPoolSize){
			currentStar = 0;
		}



    }
}
