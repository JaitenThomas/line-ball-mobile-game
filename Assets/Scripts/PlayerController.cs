using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb;
    private CircleCollider2D cc;
    private SpriteRenderer sr;

    public float speed = 2f; //225
    public float speedCheck;

    public float maxSpeed = 4;
    public Vector2 velocity;

    public float speedCheckTimer = 2;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        cc = GetComponent<CircleCollider2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        //rb.AddForce (Vector2.down * downForce, ForceMode2D.Impulse);
        //	cc.radius = sr.bounds.size.x;
    }


    void Update()
    {

        if (speed > maxSpeed)
        {
            speed = maxSpeed;
        }

        //speed += 0.1f * Time.deltaTime;
        //Debug.Log (speed);

        speedCheck = rb.velocity.magnitude;
        speedCheckTimer -= Time.deltaTime;

        if (speedCheckTimer <= 0)
        {
            speedCheckTimer = 0;
        }

        //checks the player speed
        if (speedCheck < 0.1f && speedCheckTimer == 0 && GetComponent<PlayerManager>().isDead == false && UIManager.Instance.gameOver == false)
        {
            UIManager.Instance.GameOver();
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

            //Debug.Log ("Speed Check");

        }


    }

    void FixedUpdate()
    {

        velocity = Vector2.down;

        rb.velocity = velocity * speed * Time.fixedDeltaTime * 100f;
        //rb.AddForce(velocity * speed * Time.fixedDeltaTime * 100f, ForceMode2D.Force);

    }


}
