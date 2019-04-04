using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineManager : MonoBehaviour {

	public Vector2 myVelocity;
	public float gravity = -10;
	private Rigidbody2D rb;


	void Awake(){
		rb = GetComponent<Rigidbody2D> ();
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D other){
		if(other.gameObject.tag == "Ball"){

			float speedCheck = other.gameObject.GetComponent<Rigidbody2D> ().velocity.magnitude;
			Debug.Log (speedCheck);
			//Once a the ball reaches a certain speed no more force will be added
			if (speedCheck < 7.0f) {
				Vector2 dir = transform.position - other.transform.position;
				dir.Normalize ();

				dir *= 5;

				other.gameObject.GetComponent<Rigidbody2D> ().AddForce (dir);

				Debug.Log (speedCheck);
			}


			int chance = Random.Range (0, 9); //1 out of 8 chance to spawn a star
			Debug.Log ("Chance");
			if(chance == 1){
				FindObjectOfType<ProtectObjectPool> ().SpawnObstacle ();
			}

			UIManager.Instance.AddScore();
			Destroy (this.gameObject);

		}
	}
}
