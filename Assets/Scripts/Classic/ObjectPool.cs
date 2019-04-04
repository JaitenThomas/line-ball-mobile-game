using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{


    public GameObject[] columnPrefabs; // All objects require for spawn
    public GameObject starPrefab;

    public int columnPoolSize = 5;       //How many columns to keep on standby.

    public float columnMin = -2.5f;      //Minimum y value of the column position.
    public float columnMax = 2.5f;       //Maximum y value of the column position.

    private GameObject[] columns;
    private GameObject[] stars;


    public Transform[] spawnPoints;

    private int currentColumn = 0;    //Index of the current column in the collection.
    private int currentStar = 0;


    private Vector2 objectPoolPosition = new Vector2(-15, -25);     //A holding position for our unused columns offscreen.
    public float spawnXPosition = -5f; // Starting x position
    public float spawnYPosition; // Starting x position




    void Start()
    {
        for (int i = 0; i < 15; i++)
        {
            SpawnObstacle();

        }
    }

    public void SpawnObstacle()
    {


        spawnXPosition = Random.Range(-2.5f, 2.5f);
       


        Vector2 spawnPosition = new Vector2(spawnXPosition, spawnYPosition);

        int starChance = Random.Range(0, 10);

        if (starChance != 0)
        {
            GameObject curObject;

            if (UIManager.Instance.score == 0)
            {
                curObject = columnPrefabs[0];
                Instantiate(curObject, spawnPosition, curObject.transform.rotation);
                
            }

            else if (UIManager.Instance.score > 0 && UIManager.Instance.score < 30)
            {
                curObject = columnPrefabs[Random.Range(0, 1 + 1)];
                Instantiate(curObject, spawnPosition, curObject.transform.rotation);
            }

            else if (UIManager.Instance.score >= 30 && UIManager.Instance.score < 45)
            {
                curObject = columnPrefabs[Random.Range(0, 2 + 1)];
                Instantiate(curObject, spawnPosition, curObject.transform.rotation);

            }

            else if (UIManager.Instance.score >= 45)
            {
                curObject = columnPrefabs[Random.Range(0, 3 + 1)];
                Instantiate(curObject, spawnPosition, curObject.transform.rotation);

            }
        }

        else
        {
            Instantiate(starPrefab, spawnPosition, starPrefab.transform.rotation);

        }
        spawnYPosition -= Random.Range(1.5f, 2.5f);



    }

}
