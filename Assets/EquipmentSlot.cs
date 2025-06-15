using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour
{
    [SerializeField] ArmorSO.ArmorType type;
    public ArmorSO item;
    [SerializeField]
    private Image sprite;
    public void OnClick(BaseEventData data)
    {
        if (clicked)
        {
            OnDoubleClick();
            clicked = false;
            StopCoroutine("Stop");
            return;
        }
        clicked = true;
        StartCoroutine("Stop");
    }

    public void OnRelease(BaseEventData data)
    {
    }
    private IEnumerator Stop()
    {
        yield return new WaitForSeconds(0.25f);
        clicked = false;
        // drag
    }
    private bool clicked;
    private void OnDoubleClick()
    {
    }
    internal void SetItem(ArmorSO item)
    {
        this.item = item;
        sprite.sprite = item.sprite;
    }
}
