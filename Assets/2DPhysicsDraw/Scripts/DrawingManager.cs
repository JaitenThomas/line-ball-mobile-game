// DrawingManager.cs is part of 2D Physics Draw asset for Uunity 3D
// Version 3.0
// Made by Daniel C Menezes
// Contact: d.cavalcante.m@gmail.com
// Asset Store URL: http://u3d.as/otD

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public enum ColliderTypeChoices { Polygon_Collider, Edge_Collider }
public enum overlapHandlingChoices { Follow_The_Edge, Cut_After_Collision, None }

public class DrawingManager : MonoBehaviour
{

    private LineRenderer pathLineRenderer;
    private EdgeCollider2D pathEdgeCollider;
    private PolygonCollider2D pathPolygonCollider;
    private Rigidbody2D pathRigidbody;
    private Color c1, c2;
    private int posCount;
    private float destroyCounter;
    static public int cloneNumber;
    private float colliderAngle;
    [HideInInspector]
    public List<Vector2> newVerticies = new List<Vector2>();
    private List<Vector2> newVerticies2 = new List<Vector2>();
    private List<Vector2> newVerticies_ = new List<Vector2>();
    public GameObject path;
    public GameObject massCenter;
    [HideInInspector]
    public GameObject mousePointer;
    GameObject massCenterClone;
    GameObject clone;
    [HideInInspector]
    public Vector2 centerOfMass = Vector2.zero;
    private int centerOfMassCount = 0;

    RaycastHit2D hit;
    RaycastHit2D mouseRay; // raycast to verify if the mouse click hist an object (verified by tag)
                            // that the drawing functions wont work (not darw in this area)
    public LayerMask layerMask;
    private bool mouseHit = false;
    private bool canDraw = true; // enables or disables the drawing when clicked on some environment object,
                                 // now it is set to not draw in objects with the tag "Wall"

    // settings ===================
    [Header("Line Settings")]
    public Color colorStart;            // start and final color of the line
    public Color colorEnd;
    public PhysicsMaterial2D material;  // physics material attached to the collider
    [Range(0.0f, 10.0f)]
    public float widthStart;            // start and final width of the line
    [Range(0.0f, 10.0f)]
    public float widthEnd;
    [Range(0.0f, 5.0f)]
    public float verticesDistance;      // minimun distance between vertices of the line
    public bool fixedPosition;          // if object will have fixed position (true) or it will be gravity driven (false)
    public bool isPermanent;            // if drawn object will be destroyed after t = lifeTime seconds, yes = true
    public float lifeTime;              // time after destroy a non permanent drawn object
    public ColliderTypeChoices colliderType; //choose between Polygon Collider 2D or Edge Collider 2D
                                             // *Obs: be aware that Edge Collider 2D don't collider with each other cause they don't have volume
    public bool showMassCenter;         // if mass center will be shown 
    public int massCenterPrecision;     // multiplier to make the center of mass calculation more precise relative to the width of the line
    public bool dynamicMass;            // if mass is calculated relative to the width of the line (true), or it has a fixed mass (false)
    [Range(0.0f, 10.0f)]
    public float massScale;             // mass scale when dynamicMass == true, or fixed mass when dynamicMass == false
    [Range(-10.0f, 10.0f)]
    public float gravityScale;          // gravity scale, when < 0 objects "float"
    public overlapHandlingChoices overlapHandling; // if start drawing over an object, doesn't draw the following part of the line after the first hit on some object
    public bool freezeWhileDrawing; // Freeze the drawn objects while drawing a new one;
    
    [Header("Ex.: Wall,Groung,RedArea"), Header("Tags divided by comma ',' where you can't draw.")]
    public string tagCantDraw = "";
    private string[] tagsCantdraw;

	int lineCount;
	public List<GameObject> drawnLines;


    // =================

    /// <summary>
    /// Variables are initalized and set here
    /// </summary>   
    void Start()
    {
		drawnLines = new List<GameObject> ();
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("OnDraw"), LayerMask.NameToLayer("OnDraw"));
        mousePointer = GameObject.Find("MousePointer");
      /*  cloneNumber = 0;
        colorStart = Color.black;
        colorEnd = Color.black;
        widthStart = 0.2f;
        widthEnd = 0.2f;
        verticesDistance = 0.1f;
        lifeTime = 2f;
        colliderType = ColliderTypeChoices.Polygon_Collider;
        isPermanent = true;
        fixedPosition = false;
        showMassCenter = false;
        massCenterPrecision = 100;
        dynamicMass = true;
        massScale = 1f;
        gravityScale = 0f;
        overlapHandling = overlapHandlingChoices.Follow_The_Edge;
        freezeWhileDrawing = true; */

        tagsCantdraw = tagCantDraw.Split(',');
        //tagCantDraw
        
        
        pathLineRenderer = path.GetComponent<LineRenderer>();
        //pathRigidbody = path.GetComponent<Rigidbody2D>();
        pathLineRenderer.useWorldSpace = false;

        // Setting up material of the line, it can be modified or placed as public so it can be changed in inspector
        // *Obs: changing the material will be facilitated in the next release of the asset
    //     pathLineRenderer.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
	//	pathLineRenderer.material = new Material(Shader.Find("Particles/Alpha Blended"));
        //pathLineRenderer.SetColors(colorStart, colorEnd);
        //pathLineRenderer.SetWidth(widthStart, widthEnd);
        posCount = 0;
    }
    
    void FixedUpdate()
    {
        mousePointer.GetComponent<TargetJoint2D>().target = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void Update()
    {
        // hit detection by mouse position for the on drawing collision handling
        if (overlapHandling == overlapHandlingChoices.Cut_After_Collision)
            hit = Physics2D.Raycast(mousePointer.transform.position, Vector2.zero,
Mathf.Infinity, layerMask);

        // mouseRay detects if the mouse is over some object
        mouseRay = Physics2D.Raycast((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero,
Mathf.Infinity, layerMask);
        
		if (Input.GetMouseButtonDown(0))
        {
            FreezeMoving.freeze = true;
		

            // disables the drawing when click on objects with the tag "Wall"
            foreach (string tag in tagsCantdraw)
            {
                if (mouseRay.collider != null && mouseRay.collider.tag == tag) 
                    canDraw = false;
            }

            if (canDraw == true)
            {
                mousePointer.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if (overlapHandling == overlapHandlingChoices.Follow_The_Edge)
                    mousePointer.GetComponent<CircleCollider2D>().isTrigger = false;

                mousePointer.transform.localScale = new Vector3(widthEnd, widthEnd, widthEnd);
                StarPrefab();
                centerOfMassCount = 0;
                centerOfMass = Vector2.zero;
            }
        }

		if (Input.GetMouseButton(0))
        {
			

            if (canDraw == true)
            {
                // if collides, sets collision handing flag
                if (hit.collider != null)
                {
                    mouseHit = true;
                }
                DrawVisibleLine();
            }
        }
		if (Input.GetMouseButtonUp(0))
        {
			if(GameManager.Instance.isStarted == false){
				GameManager.Instance.isStarted = true; // newly added
			}
			if(SceneManager.GetActiveScene().name == "Protect"){
				if(drawnLines.Count > 1){
					Destroy (drawnLines[0].gameObject);
					drawnLines.RemoveAt (0);

				}
			}
            FreezeMoving.freeze = false;
            if (canDraw == true)
            {
                mousePointer.GetComponent<CircleCollider2D>().isTrigger = true;
                mouseHit = false;


                if (colliderType == ColliderTypeChoices.Edge_Collider)
                {
                    newVerticies.Add(mousePointer.transform.position - clone.transform.position);
                }
                else
                {
                    newVerticies2.Clear();
                    newVerticies.Add(mousePointer.transform.position - clone.transform.position);
                    for (int i = 0; i < newVerticies.Count - 1; i++)
                    {
                        if (i < newVerticies.Count - 2)
                        {
                            // calculates the angle of each edge in the line
                            colliderAngle = Mathf.Atan2(newVerticies[i].y - newVerticies[i + 1].y, newVerticies[i].x - newVerticies[i + 1].x);
                            colliderAngle = colliderAngle + (Mathf.Deg2Rad * 90f);
                        }
                        else
                        {
                            // Since we have two points to have an edge and the last point is not in this for, the angle calculation is not 
                            // done for the last edge and the same angel from befor is preserved
                        }
                        float cos = Mathf.Cos(colliderAngle);   // calc cos
                        float sin = Mathf.Sin(colliderAngle);   // calc sin

                        // Get the width of the line for each point of it
                        float tempWidth = Mathf.Lerp(widthStart, widthEnd, (float)i / (newVerticies.Count - 2));

                        // To make the Polygon Collider 2D go around the line, it means each side of the line,
                        // adds one point to the beginning and one to the end of the List
                        newVerticies2.Add(new Vector2(
                                        (newVerticies[i].x) + ((tempWidth / 2) * cos),
                                        (newVerticies[i].y) + ((tempWidth / 2) * sin)));
                        newVerticies2.Insert(0, new Vector2(
                                        (newVerticies[i].x) - ((tempWidth / 2) * cos),
                                        (newVerticies[i].y) - ((tempWidth / 2) * sin)));

                    }
                    pathPolygonCollider.points = newVerticies2.ToArray();
                    pathPolygonCollider.sharedMaterial = material;
                }
                CalculatesPrevCenterOfMassAndMass(newVerticies.Count - 1);
                clone.layer = LayerMask.NameToLayer("Drawing");
                pathLineRenderer.SetVertexCount(newVerticies.Count - 1);
                for (int i = 0; i < newVerticies.Count - 1; i++)
                {

                    pathLineRenderer.SetPosition(i, newVerticies[i]);

                }

                if (dynamicMass == true)
                    pathRigidbody.mass *= massScale;
                else pathRigidbody.mass = massScale;
                centerOfMass /= centerOfMassCount;
                if (newVerticies.Count > 2 && (widthStart != 0 || widthEnd != 0))
                    pathRigidbody.centerOfMass = centerOfMass;

                // Check if you want to show the center of mass during the gameplay and instantiate the prefab in the proper position
                // *Obs: the MassCenter prefab can be personalized with an image or anything you want to show the center of mass
                if (showMassCenter)
                {
                    massCenterClone = (GameObject)Instantiate(massCenter, this.transform.position, Quaternion.identity);
                    massCenterClone.transform.parent = pathRigidbody.transform;
                    massCenterClone.transform.position = centerOfMass;
                }

                pathRigidbody.bodyType = RigidbodyType2D.Dynamic;
                posCount = 0;
                 
                // Check if the drawn line is too small or if it doesn't have visual volume and destroy the prefab if it true
                if (newVerticies.Count <= 2 || (widthStart == 0 && widthEnd == 0))
                {
                    cloneNumber--;
                    Destroy(clone);
                }
            }
            canDraw = true;

            FreezeMoving.freeze = false;
            // if set to freeze the drawn objects while drawing a new one and is not set to be fixed position, 
            // it adds the script FreezeMoving to the new drawings so it freezes when FreezeMoving.freeze variable is true
            if (fixedPosition == false && clone)
            {
                clone.gameObject.AddComponent<FreezeMoving>();
            }
            else
            {
                pathRigidbody.bodyType = RigidbodyType2D.Static;
            }
        }

    }

    /// <summary>
    /// Instantiate prefab and initialize necessary variables for the drawing construction
    /// </summary>
    void StarPrefab()
    {
        clone = (GameObject)Instantiate(path, mousePointer.transform.position, Quaternion.identity); // instantiate prefab
		drawnLines.Add (clone);
        clone.name = "Drawing" + cloneNumber;                   // Rename instantiated object so it can be easyli tracked
        cloneNumber++;                                          // Add 1 in the variable for renaming the drawings

        pathLineRenderer = clone.GetComponent<LineRenderer>();
        pathRigidbody = clone.GetComponent<Rigidbody2D>();
        pathRigidbody.bodyType = RigidbodyType2D.Kinematic;                       // Set rigidbody kinematic so it wont move during the drawing process
        pathRigidbody.gravityScale = gravityScale;              // Adjust gravity for the drawing
        clone.transform.position = Vector3.zero; // Camera.main.transform.position - new Vector3(0,0,Camera.main.transform.position.z);
        clone.transform.rotation = Quaternion.identity;
        pathRigidbody.centerOfMass = Vector2.zero;              // Initialize center of mass before calculating the real position
        //pathLineRenderer.SetColors(colorStart, colorEnd);       // Set colors
        //pathLineRenderer.SetWidth(widthStart, widthEnd);        // width
        newVerticies.Clear();                                   // Clear Lists before adding new points for the new drawing
        newVerticies_.Clear();
        newVerticies2.Clear();                                  // This is the List used to create the Polygon Collider 2D
        pathLineRenderer.SetVertexCount(1);                     // Add one position spot so it can add the first point of the Drawing
        pathLineRenderer.SetPosition(0, mousePointer.transform.position - new Vector3(0,0,Camera.main.transform.position.z)); // Add first point in the Line Renderer

        newVerticies.Add(mousePointer.transform.position - clone.transform.position); // Add first point to the array used to create the Colliders
        newVerticies_.Add(mousePointer.transform.position - clone.transform.position);

        // Check if the chosen collider is Edge or Polygon and destroy the one is not going to be used
        if (colliderType == ColliderTypeChoices.Edge_Collider)
        {
            Destroy(clone.GetComponent<PolygonCollider2D>());
            pathEdgeCollider = clone.GetComponent<EdgeCollider2D>();
        }
        else
        {
            Destroy(clone.GetComponent<EdgeCollider2D>());
            pathPolygonCollider = clone.GetComponent<PolygonCollider2D>();
            pathPolygonCollider.points = newVerticies2.ToArray(); // Bugfix: when creating a new drawing, collider was created in the center of the scene and hit some objects
        }
    }



    /// <summary>
    /// Draw the line using mouse position and adding the points to the Line Renderer
    /// </summary>
    void DrawVisibleLine()
    {
        // Check if the minimun distance (verticesDistance) from the previous vertice is reached and add the next point
		if (Vector2.Distance(mousePointer.transform.position, newVerticies_[posCount]) > verticesDistance)
        {
            posCount++;
            pathLineRenderer.SetVertexCount(posCount + 1);
            pathLineRenderer.SetPosition(posCount, mousePointer.transform.position - new Vector3(0, 0, Camera.main.transform.position.z));
            newVerticies_.Add(mousePointer.transform.position - clone.transform.position);
            if (mouseHit == false)
            {
                newVerticies.Add(mousePointer.transform.position - clone.transform.position);

                // Check the chosen collider
                if (colliderType == ColliderTypeChoices.Edge_Collider)
                {
                    //for (int i = 0; i < newVerticies.Count - 1; i++)
                    //{
                    //    // Get the width of the line for each point of it
                    //    float tempWidth = Mathf.Lerp(widthStart, widthEnd, (float)i / (newVerticies.Count - 2));
                    //    
                    //}
                    if (newVerticies.Count > 2)
                        pathEdgeCollider.points = newVerticies.ToArray();
                    pathEdgeCollider.sharedMaterial = material;
                }
                else
                {
                    newVerticies2.Clear();
                    newVerticies.Add(mousePointer.transform.position - clone.transform.position);
                    for (int i = 0; i < newVerticies.Count - 1; i++)
                    {

                        // calculates the angle of each edge in the line
                        colliderAngle = Mathf.Atan2(newVerticies[i].y - newVerticies[i + 1].y, newVerticies[i].x - newVerticies[i + 1].x);
                        colliderAngle = colliderAngle + (Mathf.Deg2Rad * 90f);


                        float cos = Mathf.Cos(colliderAngle);   // calc cos
                        float sin = Mathf.Sin(colliderAngle);   // calc sin

                        // Get the width of the line for each point of it
                        float tempWidth = Mathf.Lerp(widthStart, widthEnd, (float)i / (newVerticies.Count - 2));

                        // To make the Polygon Collider 2D go around the line, it means each side of the line,
                        // adds one point to the beginning and one to the end of the List
                        newVerticies2.Add(new Vector2(
                                        (newVerticies[i].x) + ((tempWidth / 2) * cos),
                                        (newVerticies[i].y) + ((tempWidth / 2) * sin)));
                        newVerticies2.Insert(0, new Vector2(
                                        (newVerticies[i].x) - ((tempWidth / 2) * cos),
                                        (newVerticies[i].y) - ((tempWidth / 2) * sin)));
                        
                    }
                    newVerticies.RemoveAt(newVerticies.Count - 1);
                    pathPolygonCollider.points = newVerticies2.ToArray();
                    pathPolygonCollider.sharedMaterial = material;
                }
            }
        }
    }

    /// <summary>
    /// For each point, summs up the values of the center of mass position and mass of the drawing
    /// </summary>
    /// <param name="n">Index of the points</param>
    /// <param name="width">Half of the width of the line in each point</param>
    void CalculatesPrevCenterOfMassAndMass(float width)
    {
        // Calcs the center of mass related to width and the precision set
        for (int j = 0; j <= width-2; j++)
        {
            float times = Vector2.Distance(newVerticies[j], newVerticies[j + 1])*massCenterPrecision;
            for (int i = 0; i <= times; i++)
            {
                centerOfMass += Vector2.Lerp(newVerticies[j], newVerticies[j + 1], 0.5f);
                centerOfMassCount++;
            }
        }
        // Adds mass for each point of the line
        // *Obs: This will be adjusted in onther versions to get the actual length of the line instead of number of points
        if (dynamicMass == true && newVerticies.Count > 2) pathRigidbody.mass += width;
    }

}