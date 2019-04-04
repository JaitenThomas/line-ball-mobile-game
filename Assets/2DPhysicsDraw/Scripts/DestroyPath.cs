// DestroyPath.cs is part of 2D Physics Draw asset for Uunity 3D
// Made by Daniel C Menezes
// Contact: d.cavalcante.m@gmail.com
// Asset Store URL: http://u3d.as/otD

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DestroyPath : MonoBehaviour {
    
    public bool isPermanent;
    private  List<Vector2> newVerticies = new List<Vector2>();
    public float destroyCounter;
    private bool canDestroy = false;
    private Vector2 centerOfMass = Vector2.zero;
    private DrawingManager managerScript;
    private bool dynamicMass;
    private float massScale;

    void Start () {

        if (SaveManager.Instance.state.LineType == false)
        {
            managerScript = GameObject.Find("DrawingManagerCurve").GetComponent<DrawingManager>();

        }

        else if (SaveManager.Instance.state.LineType == false)
        {
            managerScript = GameObject.Find("DrawingManagerStraight").GetComponent<DrawingManager>();

        }


        newVerticies = managerScript.newVerticies;
        destroyCounter = managerScript.lifeTime;
        isPermanent = managerScript.isPermanent;
    }
	
	void Update () {

        if (Input.GetMouseButtonUp(0) && this.name.Equals("Drawing"+(DrawingManager.cloneNumber-1)))
        {
            
            foreach (Vector2 positions in newVerticies)
            {
                centerOfMass += positions;
                
            }
            centerOfMass /= newVerticies.Count;
            canDestroy = true;
        }

        if (destroyCounter > 0 && isPermanent == false && canDestroy == true)
        {
            destroyCounter -= Time.deltaTime;
            if (destroyCounter <= 0)
            {
                Destroy(this.gameObject); 
            }
        }

    }
}
