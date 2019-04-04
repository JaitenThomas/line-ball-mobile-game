	using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Contains all the code for managing object pools and turning on the correct scripts for gamemodes
public class GameManager : MonoBehaviour {

	private static GameManager instance;
	public static GameManager Instance{get{return instance;}}

	public Color[] allColors;

	public bool isStarted;

	public ObjectPool op;
	public GapObjectPool gop;
	public HoopObjectPool hop;
	public ProtectObjectPool pop;
    public BoostObjectPool bop;



	public DrawingManager dm;

	public GameObject ball;
	public PhysicsMaterial2D bounce;

	public int checkOnce;
	public int checkOnce1;

	public GameObject drawLineStraightManager;
	public GameObject drawLineCurveManager;



	void Awake(){
		instance = this;

	}

	// Use this for initialization
	void Start () {
		isStarted = false;

		ball = GameObject.Find ("Ball");

		//Place the object pool relative to the gamemode manually, no need to fill in the rest
		if(op == null){
			op = null;
		}

		if(gop == null){
			gop = null;
		}

		if(hop == null){
			hop = null;
		}

		if(pop == null){
			pop = null;
		}

        if (bop == null)
        {
            bop = null;

        }

    
    }
	
	// Update is called once per frame
	void Update () {
		
        //Turn on gamemode scripts

		if(isStarted == true && UIManager.Instance.gameOver == false && checkOnce == 0)
		{
			dm.enabled = true;

			UIManager.Instance.handPointer.SetActive (false);
			UIManager.Instance.drawLineToStartText.SetActive (false);

			if (SceneManager.GetActiveScene ().name == "Classic") {
				ball.AddComponent<PlayerController>();

				op.enabled = true;
				dm.enabled = true;
			}

			if (SceneManager.GetActiveScene ().name == "Gap") {
				ball.AddComponent<PlayerController>();

				gop.enabled = true;
				dm.enabled = true;

			}

			if (SceneManager.GetActiveScene ().name == "Hoop") {
				ball.AddComponent<PlayerController>();

				hop.enabled = true;
				dm.enabled = true;
			}

			if(SceneManager.GetActiveScene().name == "Protect"){
				ball.AddComponent<ProtectPlayerController>();

				pop.enabled = true;
				dm.enabled = true;
			}

            if (SceneManager.GetActiveScene().name == "Boost")
            {
                ball.AddComponent<BoostPlayerController>();

                bop.enabled = true;
                dm.enabled = true;
            }

            checkOnce = 1;
		}

		
	}
		


}
