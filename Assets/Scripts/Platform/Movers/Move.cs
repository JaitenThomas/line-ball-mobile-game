using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {


    public Vector2 targetA;
    public Vector2 targetB;

    public float speed;
    public int dir;

    

	// Use this for initialization
	void Start ()
    {
        targetA = transform.position;
        
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(Vector2.Distance(transform.position, targetA) < 0.1f)
        {
            dir = -1;
        }

        if (Vector2.Distance(transform.position, targetB) < 0.1f)
        {
            dir = 1;
        }

        if(dir == 1)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetA, speed * Time.deltaTime);
        }

        else if (dir == -1)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetB, speed * Time.deltaTime);
        }
    }
}
