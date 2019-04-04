using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineDisplay : MonoBehaviour
{

    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    // Use this for initialization
    void Start()
    {
        image.sprite = FindObjectOfType<PlayerManager>().lineTextures[SaveManager.Instance.state.lineIndex];
    }

    // Update is called once per frame
    void Update()
    {

    }
}
