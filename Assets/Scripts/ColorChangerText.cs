using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ColorChangerText : MonoBehaviour {

    public Color[] colorList;
    public float delayTime = 1;

    public Text otherText;

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Main Menu")
        {
            colorList = MainMenuManager.instance.colorArray;
        }
        else
        {
            colorList = GameManager.Instance.allColors;
        }
      
        StartCoroutine(ChangeColorAfterTime());

    }

    IEnumerator ChangeColorAfterTime()
    {
        Text textRenderer = gameObject.GetComponent<Text>();

        Color currentcolor = (Color)colorList[UnityEngine.Random.Range(0, colorList.Length)];
        Color nextcolor;

        textRenderer.color = currentcolor;

        while (true)
        {
            nextcolor = (Color)colorList[UnityEngine.Random.Range(0, colorList.Length)];

            for (float t = 0; t < delayTime; t += Time.deltaTime)
            {
                textRenderer.color = Color.Lerp(currentcolor, nextcolor, t / delayTime);
                otherText.color = textRenderer.color;
                yield return null;
            }
            currentcolor = nextcolor;
        }
    }
}

