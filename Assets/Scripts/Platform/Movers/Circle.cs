using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{

    private Vector2 startPosition;

    float timeCounter = 0;

    public float speed;
    public float height;
    public float width;

    public float minSpeed;
    public float maxSpeed;


    public float minWidth;
    public float maxWidth;


    public float minHeight;
    public float maxHeight;



    // Use this for initialization
    void Start()
    {
        startPosition = (Vector2)transform.position;

        //Speed
        speed = Random.Range(minSpeed, maxSpeed);

        //Width
        width = Random.Range(minWidth, maxWidth);

        //Height
        height = Random.Range(minHeight, maxHeight);
    }

    // Update is called once per frame
    void Update()
    {
        timeCounter += Time.deltaTime * speed;

        float x = startPosition.x + Mathf.Cos(timeCounter) * width;
        float y = startPosition.y + Mathf.Sin(timeCounter) * height;

        transform.localPosition = new Vector2(x, y);
    }
}
