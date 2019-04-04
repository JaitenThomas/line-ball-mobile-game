using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSpawner : MonoBehaviour
{
    public float timer;
    public float maxTime = 2.0f;

    public GameObject tile;

    void Start()
    {

    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > maxTime)
        {
            Spawn();
            timer = 0;
        }
    }

    public void Spawn()
    {
        Vector2 left = Camera.main.ViewportToWorldPoint(new Vector2(0, Random.Range(0.0f, 0.9f) ));
        Vector2 right = Camera.main.ViewportToWorldPoint(new Vector2(1, Random.Range(0.0f, 0.9f)));
        Vector2 up = Camera.main.ViewportToWorldPoint(new Vector2(Random.Range(0.0f, 0.9f), 1));
        Vector2 down = Camera.main.ViewportToWorldPoint(new Vector2(Random.Range(0.0f, 0.9f), 0));

        left.x -= tile.GetComponent<SpriteRenderer>().bounds.extents.x;
        right.x += tile.GetComponent<SpriteRenderer>().bounds.extents.x;
        up.y += tile.GetComponent<SpriteRenderer>().bounds.extents.y;
        down.y -= tile.GetComponent<SpriteRenderer>().bounds.extents.y;

        Vector2[] temp = new Vector2[4];
        temp[0] = left;
        temp[1] = right;
        temp[2] = up;
        temp[3] = down;

        Vector2 dir = temp[Random.Range(0, temp.Length)];
        float speed = Random.Range(1, 3);
      
        GameObject go = (GameObject)Instantiate(tile, dir, Quaternion.identity);
           

        if (dir == left)
        {
            go.GetComponent<Rigidbody2D>().velocity = new Vector2(1, Random.Range(0.5f, 1.0f)) * speed;
        }

        else if (dir == right)
        {
            go.GetComponent<Rigidbody2D>().velocity = new Vector2(-1, Random.Range(-0.5f, -1.0f)) * speed;

        }

        else if (dir == up)
        {
            go.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-0.5f, -1.0f), -1) * speed;
        }

        else if (dir == down)
        {
            go.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(0.5f, 1.0f), 1) * speed;

        }

        Destroy(go.gameObject, 10.0f);
    }
}
