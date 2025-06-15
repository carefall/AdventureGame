using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] Sprite blank;
    public static Inventory instance;
    private Slot start;
    [SerializeField] TransferItem item;
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
                s.SetItem(item, 1);
                return;
            }
        }
    }
    public void TransferItem(Slot end)
    {
        if (start == null) return;
        (ItemSO, int) item1 = (start.item, start.amount);
        (ItemSO, int) item2 = (end.item, end.amount);
        start.SetItem(item2.Item1, item2.Item2);
        end.SetItem(item1.Item1, item1.Item2);
        item.Stop();
        start = null;
    }
    public void BeginTransfer(Slot start)
    {
        this.start = start;
        item.Setup(start.item.sprite);
    }
    public void DropItem(Slot slot)
    {

    }
    public void UseItem(Slot slot)
    {

    }
}
