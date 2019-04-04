using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StarAdder : MonoBehaviour
{

    public ObjectPool op;
    public BoostObjectPool bop;
    public HoopObjectPool hop;
    public ProtectObjectPool pop;
    public GapObjectPool gop;


    void Awake ()
    {
        if (op == null)
        {
            op = null;
        }

        else if (bop == null)
        {
            bop = null;
        }

        else if (hop == null)
        {
            hop = null;
        }

        else if (pop == null)
        {
            pop = null;
        }

        else if (gop == null)
        {
            gop = null;
        }
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Classic")
        {
            op = GameObject.FindObjectOfType<ObjectPool>();
        }

        else if (SceneManager.GetActiveScene().name == "Boost")
        {
            bop = GameObject.FindObjectOfType<BoostObjectPool>();
        }

        else if (SceneManager.GetActiveScene().name == "Hoop")
        {
            hop = GameObject.FindObjectOfType<HoopObjectPool>();
        }

        else if (SceneManager.GetActiveScene().name == "Protect")
        {
            pop = GameObject.FindObjectOfType<ProtectObjectPool>();
        }

        else if (SceneManager.GetActiveScene().name == "Gap")
        {
            gop = GameObject.FindObjectOfType<GapObjectPool>();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ball")
        {

            if (SceneManager.GetActiveScene().name == "Classic")
            {
                other.GetComponent<PlayerController>().speed += 0.05f;
                op.SpawnObstacle();
            }

            else if (SceneManager.GetActiveScene().name == "Boost")
            {
                GameObject.FindObjectOfType<DrawLine>().speed += 0.05f;
                bop.SpawnObstacle();
            }

            else if (SceneManager.GetActiveScene().name == "Hoop")
            {
                other.GetComponent<PlayerController>().speed += 0.05f;
                hop.SpawnObstacle();
            }

            else if (SceneManager.GetActiveScene().name == "Gap")
            {
                other.GetComponent<PlayerController>().speed += 0.05f;
                gop.SpawnObstacle();
            }

            else if (SceneManager.GetActiveScene().name == "Protect")
            {
                //No need to add speed, speed comes from collision with the line.
                pop.SpawnObstacle();
            }
        }
    }
}
