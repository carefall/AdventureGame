using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField] 
    private Image sprite;
    public ItemSO item;
    public int amount;
    [SerializeField] 
    private TextMeshProUGUI amountText;
    public void OnClick(BaseEventData data)
    {
        if (item == null) return;
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
    private void OnEnable()
    {
        if (item == null)
        {
            sprite.sprite = Inventory.Blank();
            amountText.text = "";
        }
    }

    public void OnRelease(BaseEventData data)
    {
        Debug.Log("a");
        Inventory.instance.TransferItem(this);
    }
    private IEnumerator Stop()
    {
        yield return new WaitForSeconds(0.25f);
        clicked = false;
        Inventory.instance.BeginTransfer(this);
    }
    private bool clicked;
    private void OnDoubleClick()
    {
    }

    internal void Add()
    {
        amount++;
        amountText.text = amount.ToString();

    }

    internal void SetItem(ItemSO item, int amount)
    {
        this.item = item;
        this.amount = amount;
        if (item == null)
        {
            sprite.sprite = Inventory.Blank();
            amountText.text = "";
            return;
        }
        sprite.sprite = item.sprite;
        amountText.text = amount.ToString();
    }
}
