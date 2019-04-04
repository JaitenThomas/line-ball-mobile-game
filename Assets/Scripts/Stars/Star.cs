using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Star : MonoBehaviour
{

    public GameObject starCollectText;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ball")
        {

            AudioManager.instance.Play("Collect");

            UIManager.Instance.AddStar(1);

            GameObject anim = Instantiate(starCollectText.gameObject, transform.position, Quaternion.identity);
            if (SaveManager.Instance.state.broughtDoubledStars == false)
            {
                anim.GetComponent<TextMesh>().text = "+1";
            }
            else if (SaveManager.Instance.state.broughtDoubledStars == true)
            {
                anim.GetComponent<TextMesh>().text = "+2";
            }
            anim.transform.SetParent(gameObject.transform);
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            Destroy(anim, 0.6f);

            if (SceneManager.GetActiveScene().name == "Protect")
            {

                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                gameObject.GetComponent<PolygonCollider2D>().enabled = false;
            }

            else if (SceneManager.GetActiveScene().name == "Boost")
            {

                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                gameObject.GetComponent<PolygonCollider2D>().enabled = false;
                gameObject.transform.GetChild(0).GetComponent<BoxCollider2D>().enabled = false;

            }
        }
    }
}
