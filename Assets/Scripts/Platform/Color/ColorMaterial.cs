using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorMaterial : MonoBehaviour
{

    private Renderer rend;

    void Awake()
    {
        rend = GetComponent<Renderer>();
    }

    void Start()
    {

        rend.material.color = GameManager.Instance.allColors[Random.Range(0, GameManager.Instance.allColors.Length)];

    }
}