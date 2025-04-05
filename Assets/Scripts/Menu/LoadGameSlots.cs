using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadGameSlots : MonoBehaviour
{
    [SerializeField] private GameObject Slot1, Slot2, Slot3;
    [SerializeField] private Color EmptyColor;
    private void OnEnable()
    {
        if (File.Exists(Application.dataPath + "/slot" + 1 + ".json")) //1
        {
            SaveData data = JsonUtility.FromJson<SaveData>(File.ReadAllText(Application.dataPath + "/slot" + 1 + ".json"));
            Slot1.GetComponentInChildren<TextMeshProUGUI>().text = "Slot 1\n" + data.Name;
            Slot1.GetComponentInChildren<Button>().interactable = true;
        }
        else
        {
            Slot1.GetComponentInChildren<TextMeshProUGUI>().text = "Slot 1\nEmpty";
            Slot1.GetComponentInChildren<Button>().interactable = false;
        }
        if (File.Exists(Application.dataPath + "/slot" + 2 + ".json")) //2
        {
            SaveData data = JsonUtility.FromJson<SaveData>(File.ReadAllText(Application.dataPath + "/slot" + 2 + ".json"));
            Slot2.GetComponentInChildren<TextMeshProUGUI>().text = "Slot 2\n" + data.Name;
            Slot2.GetComponentInChildren<Button>().interactable = true;
        }
        else
        {
            Slot2.GetComponentInChildren<TextMeshProUGUI>().text = "Slot 2\nEmpty";
            Slot2.GetComponentInChildren<Button>().interactable = false;
        }
        if (File.Exists(Application.dataPath + "/slot" + 3 + ".json")) //3
        {
            SaveData data = JsonUtility.FromJson<SaveData>(File.ReadAllText(Application.dataPath + "/slot" + 3 + ".json"));
            Slot3.GetComponentInChildren<TextMeshProUGUI>().text = "Slot 3\n" + data.Name;
            Slot3.GetComponentInChildren<Button>().interactable = true;
        }
        else
        {
            Slot3.GetComponentInChildren<TextMeshProUGUI>().text = "Slot 3\nEmpty";
            Slot3.GetComponentInChildren<Button>().interactable = false;
        }
    }
    public void LoadGame(int slot)
    {
        PlayerPrefs.SetInt("slot", slot);
        PlayerPrefs.SetInt("newgame", 0);
        PlayerPrefs.Save();
        SceneManager.LoadScene("OpenWorld");
    }
}
