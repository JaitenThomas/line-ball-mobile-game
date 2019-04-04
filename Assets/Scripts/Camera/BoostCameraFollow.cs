using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostCameraFollow : MonoBehaviour {

    public GameObject player;       //Public variable to store a reference to the player game object


    private Vector3 offset;         //Private variable to store the offset distance between the player and camera

    public Vector3 playerStartPos;


    private void Start()
    {

        player = GameObject.FindGameObjectWithTag("Ball");

        player.transform.position = playerStartPos;

        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        offset = transform.position - playerStartPos;

        
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        transform.position = new Vector3(player.transform.position.x, 2) + offset;
    }
}