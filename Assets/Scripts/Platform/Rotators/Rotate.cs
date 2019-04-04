using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{

    public float minRotate = 0;
    public float maxRotate = 360;

    void Start()
    {
        gameObject.transform.eulerAngles = new Vector3(0, 0, Random.Range(minRotate, maxRotate + 1));
    }

}
