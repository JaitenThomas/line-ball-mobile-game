using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayBall : MonoBehaviour
{
    private RectTransform rect;
    private Image image;
    public float rotateSpeed;

    private void Awake()
    {
        image = GetComponent<Image>();
        rect = GetComponent<RectTransform>();
    }

    void Start()
    {
        image.sprite = FindObjectOfType<PlayerManager>().playerSprites[SaveManager.Instance.state.ballIndex];
    }

    void Update()
    {
        rect.Rotate(new Vector3(0, 0, rotateSpeed * Time.deltaTime));
    }
}
