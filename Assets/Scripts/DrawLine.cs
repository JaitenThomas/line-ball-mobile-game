using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DrawLine : MonoBehaviour {
	
	//reference to LineRenderer component
	public LineRenderer line; 
	//car to store mouse position on the screen
	private Vector3 mousePos;
	//assign a material to the Line Renderer in the Inspector
	public Material material;
	//number of lines drawn
	private int currLines = 0;

	private List<Vector2> m_Points;

	public PhysicsMaterial2D linePhysicMaterial;

	public Material tempMaterial;

    public float speed = 3;



	void Awake(){
		m_Points = new List<Vector2>(); 
	}

	void Start(){
		tempMaterial.mainTexture = material.mainTexture;
	}

	void Update ()
	{
		//Create new Line on left mouse click(down)
		if(Input.GetMouseButtonDown(0) && UIManager.Instance.gameOver == false)
		{
			//check if there is no line renderer created
			if(line == null){
				//create the line
				createLine();
			}
			//get the mouse position
			mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			//set the z co ordinate to 0 as we are only interested in the xy axes
			mousePos.z = 0;
			//set the start point and end point of the line renderer
			line.SetPosition(0,mousePos);
			line.SetPosition(1,mousePos);



			m_Points.Add (mousePos);
		}
		//if line renderer exists and left mouse button is click exited (up)
		else if(Input.GetMouseButtonUp(0) && line && UIManager.Instance.gameOver == false)
		{
			mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			mousePos.z = 0;
			//set the end point of the line renderer to current mouse position
			line.SetPosition(1,mousePos);



			GameManager.Instance.isStarted = true;

			m_Points.Add (mousePos);

			line.gameObject.AddComponent<EdgeCollider2D> ();
		//	line.gameObject.GetComponent<EdgeCollider2D> ().

			line.gameObject.GetComponent<EdgeCollider2D> ().points = m_Points.ToArray();
			//line.gameObject.GetComponent<EdgeCollider2D> ().sharedMaterial = linePhysicMaterial;

			line.gameObject.GetComponent<EdgeCollider2D> ().edgeRadius = 0.04f;


            //Only for protect Gamemode
            if (SceneManager.GetActiveScene().name == "Protect")
            {
                AddProtectComponents();
            }


            //Only for Boost Gamemode
            else if (SceneManager.GetActiveScene().name == "Boost")
            {
                AddBoostComponents();
            }



            line.material = material;

			//set line as null once the line is created
			line = null;

			//clear temp points
			m_Points.Clear ();

            //material.color = new Color (material.color.r, material.color.g, material.color.b, 1);

            AudioManager.instance.Play("Pop");
           

            currLines++;
		}
		//if mouse button is held clicked and line exists
		else if(Input.GetMouseButton(0) && line && UIManager.Instance.gameOver == false)
		{
			mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			mousePos.z = 0;
			//set the end position as current position but dont set line as null as the mouse click is not exited
			line.SetPosition(1, mousePos);
		}
	}

	//method to create line
	private void createLine()
	{
		//create a new empty gameobject and line renderer component
		line = new GameObject("Line"+currLines).AddComponent<LineRenderer>();
		line.tag = "Line";



		tempMaterial.color = new Color (material.color.r, material.color.g, material.color.b, 0.5f);
		line.numCapVertices = 10;


		//assign the material to the line
		line.material = tempMaterial;
		//line.material = material;
		//set the number of points to the line
		line.SetVertexCount(2);
		//set the width
		line.SetWidth(0.1f,0.1f);
		//render line to the world origin and not to the object's position
		line.useWorldSpace = false;
        //creates collider





    }

    void AddProtectComponents()
    {
        line.gameObject.AddComponent<LineManager>();
        if (line != null)
        {
            Destroy(GameObject.Find("Line" + (currLines - 1).ToString()));
        }
        
    }

    void AddBoostComponents()
    {
        line.gameObject.AddComponent<SurfaceEffector2D>();
        line.gameObject.GetComponent<EdgeCollider2D>().usedByEffector = true;
        line.gameObject.GetComponent<SurfaceEffector2D>().speed = speed;
        line.gameObject.GetComponent<SurfaceEffector2D>().forceScale = 1;
    }
    
}
