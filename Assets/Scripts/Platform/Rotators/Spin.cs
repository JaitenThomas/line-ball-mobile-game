using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    public float spinSpeed;

    public float minSpeed;
    public float maxSpeed;



    // Use this for initialization
    void Start()
    {
        spinSpeed = Random.Range(minSpeed, maxSpeed);
    }

    void Update()
    {
        transform.Rotate(Vector3.forward * spinSpeed * Time.deltaTime);
    }
}
