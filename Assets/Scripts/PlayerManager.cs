using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour {

    public Vector3 startPosition;
    public static GameObject ball;

    public PhysicsMaterial2D noFriction;
    public PhysicsMaterial2D bounce;

    public Sprite[] playerSprites;
    public Sprite[] lineTextures;
    public Material lineMaterial;

    public Animator ballAnimator;
    private Rigidbody2D rb;

    public bool isDead;


    void Awake() {
        if (ball == null) {
            ball = this.gameObject;
        } else {
            Destroy(gameObject);
        }

        rb = GetComponent<Rigidbody2D>();

        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start() {
        GetComponent<SpriteRenderer>().sprite = playerSprites[SaveManager.Instance.state.ballIndex];

        lineMaterial.mainTexture = lineTextures[SaveManager.Instance.state.lastLineIndex].texture;
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Obstacle") {
            UIManager.Instance.GameOver();
        }

        //For boost gamemode
        if (other.gameObject.tag == "Death") {
            UIManager.Instance.GameOver();
        }

        if (other.gameObject.tag == "Star") {
            //SaveManager.Instance.state.star += 1;
        }

        if (other.gameObject.tag == "Boarder")
        {
            UIManager.Instance.GameOver();
        }
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (UIManager.Instance.gameOver == false && other.gameObject.tag == "Line" && isDead == false && UIManager.Instance.gameOver == false && SceneManager.GetActiveScene().name != "Boost" && SceneManager.GetActiveScene().name != "Main Menu")
        {
            
            if (other.gameObject.GetComponent<LineRenderer>().GetPosition(1).y > other.gameObject.GetComponent<LineRenderer>().GetPosition(0).y)
            {
                if (other.gameObject.GetComponent<LineRenderer>().GetPosition(1).x > other.gameObject.GetComponent<LineRenderer>().GetPosition(0).x)
                {
                    transform.Rotate(0, 0, 10);
                }

                if (other.gameObject.GetComponent<LineRenderer>().GetPosition(0).x > other.gameObject.GetComponent<LineRenderer>().GetPosition(1).x)
                {
                    transform.Rotate(0, 0, -10);
                }
            }

            if (other.gameObject.GetComponent<LineRenderer>().GetPosition(1).y < other.gameObject.GetComponent<LineRenderer>().GetPosition(0).y)
            {
                if (other.gameObject.GetComponent<LineRenderer>().GetPosition(1).x > other.gameObject.GetComponent<LineRenderer>().GetPosition(0).x)
                {
                    transform.Rotate(0, 0, -10);
                }

                if (other.gameObject.GetComponent<LineRenderer>().GetPosition(0).x > other.gameObject.GetComponent<LineRenderer>().GetPosition(1).x)
                {
                    transform.Rotate(0, 0, 10);
                }
            }
        }

        else if (UIManager.Instance.gameOver == false && other.gameObject.tag == "Line" && isDead == false && UIManager.Instance.gameOver == false && SceneManager.GetActiveScene().name == "Boost" && SceneManager.GetActiveScene().name != "Main Menu")
        {

            transform.Rotate(0, 0, -10);
        }
    }


	public void Died(){
        AudioManager.instance.Play("Hit");
		isDead = true;
		gameObject.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezeAll;	
		gameObject.GetComponent<PlayerManager> ().ballAnimator.SetBool ("IsDead", true);
		transform.position = rb.position;

	}
}
