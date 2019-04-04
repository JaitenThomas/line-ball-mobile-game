using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPosition : MonoBehaviour
{

    private Vector3 curTarget;

    public float speed;
    public float waitTime;

    public bool reached;


    void Start()
    {
        curTarget = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 0));
        curTarget.z = -7.5f;
        
        
    }

    void Update()
    {

        if (Vector3.Distance(transform.position, curTarget) <= 0.1f && reached == false)
        {
            reached = true;
            StartCoroutine(waitUntilMove());
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, curTarget, speed * Time.deltaTime);
            Debug.Log(curTarget);
        }
    }

    IEnumerator waitUntilMove()
    {
        yield return new WaitForSeconds(waitTime);
        curTarget = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 0));
        curTarget.z = -7.5f;
        reached = false;
    }

}
