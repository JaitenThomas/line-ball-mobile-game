using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSprite : MonoBehaviour
{

    private SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Start()
    {     
        sr.color = GameManager.Instance.allColors[Random.Range(0, GameManager.Instance.allColors.Length)];
    }
}
