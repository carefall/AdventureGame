using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] Sprite blank;
    private static Inventory instance;
    public static Sprite Blank()
    {
        if (instance == null)
        {
            return null;
        }
        return instance.blank;
    }
    private void OnEnable()
    {
        instance = this;
    }
    void Start()
    {
        transform.parent.gameObject.SetActive(false);
    }
    void Update()
    {

    }
    public void AddItem(ItemSO item)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Slot s = transform.GetChild(i).GetComponent<Slot>();
            if (s.item == null)
            {
                continue;
            }
            if (s.item.uniqueName == item.uniqueName)
            {
                if (s.amount < item.maxStackSize)
                {
                    s.Add();
                    return;
                }
            }
        }
        for (int i = 0; i < transform.childCount; i++)
        {
            Slot s = transform.GetChild(i).GetComponent<Slot>();
            if (s.item == null)
            {
                s.SetItem(item);
                return;
            }
        }
    }
    public void TransferItem(Slot start, Slot end)
    {

    }
    public void DropItem(Slot slot)
    {

    }
    public void UseItem(Slot slot)
    {

    }
}
