using UnityEngine;

public class UndergroundDoor : MonoBehaviour
{
    [SerializeField] GameObject Lock, Trigger2, EnterTrigger;
    private void OnTriggerEnter(Collider other)
    {
        Lock.SetActive(true); Trigger2.SetActive(false);
        EnterTrigger.SetActive(false);
    }
}
