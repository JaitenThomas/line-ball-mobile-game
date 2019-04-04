using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedCamera : MonoBehaviour
{

    public GameObject player;       //Public variable to store a reference to the player game object


    public Vector3 playerStartPos;

    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Ball");

        player.transform.position = playerStartPos;
    }
	
}
