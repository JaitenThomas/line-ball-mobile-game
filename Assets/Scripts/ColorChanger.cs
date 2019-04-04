using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour {

    public Color[] colorList;
    public float delayTime = 1;

	// Use this for initialization
	void Start () {
        colorList = GameManager.Instance.allColors;
        StartCoroutine(ChangeColorAfterTime());
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator ChangeColorAfterTime()
    {
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
       
        Color currentcolor = (Color)colorList[UnityEngine.Random.Range(0, colorList.Length)]; 
        Color nextcolor;

        spriteRenderer.color = currentcolor;

        while (true)
        {
            nextcolor = (Color)colorList[UnityEngine.Random.Range(0, colorList.Length)];

            for (float t = 0; t < delayTime; t += Time.deltaTime)
            {
                spriteRenderer.color = Color.Lerp(currentcolor, nextcolor, t / delayTime);
                yield return null;
            }
            currentcolor = nextcolor;
        }
    }
}
