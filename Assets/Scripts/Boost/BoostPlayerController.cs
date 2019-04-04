using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostPlayerController : MonoBehaviour {

	private Rigidbody2D rb;
	private CircleCollider2D cc;
	private SpriteRenderer sr;

	public float speed = 2.0f;
	public float speedCheck;

	public int maxSpeed = 4;
	public Vector2 velocity;

	public float speedCheckTimer = 2;

    


	void Awake(){
		rb = GetComponent<Rigidbody2D> ();
		cc = GetComponent<CircleCollider2D> ();
		sr = GetComponent<SpriteRenderer> ();
	}

	void Start () {
        rb.gravityScale = 1;
	}


	void Update(){

		if(speed > maxSpeed){
			speed = maxSpeed;
		}

		//speed += 0.1f * Time.deltaTime;
		//Debug.Log (speed);

		speedCheck = rb.velocity.magnitude;
		speedCheckTimer -= Time.deltaTime;

		if(speedCheckTimer <= 0){
			speedCheckTimer = 0;
		}

		//checks the player speed
		if (speedCheck < 0.1f && speedCheckTimer == 0 && GetComponent<PlayerManager>().isDead == false && UIManager.Instance.gameOver == false) {
			UIManager.Instance.GameOver ();
			GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezeAll;	
			
            //Debug.Log ("Speed Check");

		} 
	}


    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Line"))
        {
            Destroy(other.gameObject);
        }
    }

    
}
