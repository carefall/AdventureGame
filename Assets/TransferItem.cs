using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TransferItem : MonoBehaviour
{
    internal void Setup(Sprite sprite)
    {
        GetComponent<Image>().sprite = sprite;
        gameObject.SetActive(true);
    }

    internal void Stop()
    {
        gameObject.SetActive(false);
    }
    void Update()
    {
        Vector2 p = Mouse.current.position.ReadValue();
        transform.position = p;
    }
}
