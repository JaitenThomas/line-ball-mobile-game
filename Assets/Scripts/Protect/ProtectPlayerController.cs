using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectPlayerController : MonoBehaviour {

	private Rigidbody2D rb;
	private CircleCollider2D cc;
	private SpriteRenderer sr;

	public float speed = 100;




	void Awake(){
		rb = GetComponent<Rigidbody2D> ();
		cc = GetComponent<CircleCollider2D> ();
		sr = GetComponent<SpriteRenderer> ();

        GetComponent<Collider2D>().sharedMaterial = GetComponent<PlayerManager>().bounce;
    }

	void Start () {
		rb.AddForce (Vector2.down * speed);
	}
}
