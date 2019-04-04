// FreezeMoving.cs is part of 2D Physics Draw asset for Uunity 3D
// Made by Daniel C Menezes
// Contact: d.cavalcante.m@gmail.com
// Asset Store URL: http://u3d.as/otD

using UnityEngine;

// this class is related to the freezeWhileMoving variable
// it enables or disables the movement of drawn objects while drawing another
public class FreezeMoving : MonoBehaviour {

    private Rigidbody2D rigiThis;
    private GameObject drawingManager;
    
	void Start () {
        rigiThis = this.GetComponent<Rigidbody2D>();
        drawingManager = GameObject.Find("DrawingManager");
    }

    public static bool freeze = false;

    void Update() {
        if (drawingManager.GetComponent<DrawingManager>().freezeWhileDrawing == true)
        {
            if(freeze == true)
                rigiThis.bodyType = RigidbodyType2D.Static;
            else
                rigiThis.bodyType = RigidbodyType2D.Dynamic;
        }

	}
}
