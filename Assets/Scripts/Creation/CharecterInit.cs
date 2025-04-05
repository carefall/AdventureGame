using System.IO;
using UnityEngine;

public class CharecterInit : MonoBehaviour
{
    [SerializeField] ThirdPersonController player;
    [SerializeField] Animator male, female;
    [SerializeField] GameObject Camera;
    private void OnEnable()
    {
        SaveData data = SaveSystem.GetSaveData();
        if (data.male)
        {
            Destroy(female.gameObject);
            player.animator = male;
            male.gameObject.SetActive(true);
        }
        else 
        {
            Destroy(male.gameObject);
            player.animator = female;
            female.gameObject.SetActive(true);
        }
        player.Name = data.Name;
        player.Class = data.Class;
        player.transform.position = data.position;
        player.transform.eulerAngles = data.rotation;
        PlayerData.instance.Fill(data.hp, data.level, data.male, data.quests, data.currentQuest, data.completedQuests, data.failedQuests, data.deadUniqueNPCs, data.inventoryContent);
        Camera.SetActive(true);
        player.enabled = true;
        Destroy(this);
    }
}
