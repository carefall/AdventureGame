using System;
using System.IO;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCreation : MonoBehaviour
{
    private string Name;
    private int Class = -1;
    private bool male = true;
    [SerializeField] private TextMeshProUGUI classText;
    [SerializeField] private Button playButton;
    [SerializeField] private TMP_InputField input;
    private string lasttext;
    [Multiline] 
    [SerializeField] private string warriorDesc, thiefDesc, mageDesc;
    [SerializeField] private GameObject Male, female, player, tutorial;

    public void OnNameChange(string text)
    {
        string nametext = text.Trim();
        Regex R = new Regex("^[Р-пр-џA-Za-z\\s]*$");
        if (!R.IsMatch(nametext))
        {
            input.SetTextWithoutNotify(lasttext);
        }
        else 
        {
            lasttext = nametext;
        }
        if (lasttext.Length > 2 && Class != -1)
        {
            playButton.interactable = true;
        }
        else 
        {
            playButton.interactable = false;
        }
    }
    public void OnGenderChange(Single gender)
    {
        Male.SetActive(gender == 0);
        female.SetActive(gender == 1);
        male = gender == 0;
    }
    public void OnClassChange(int newClass)
    {
        Class = newClass;
        if (Class == 0)
        {
            classText.text = warriorDesc;
        }
        else if (Class == 1)
        {
            classText.text = thiefDesc;
        }
        else
        {
            classText.text = mageDesc;
        }
        if (lasttext.Length > 2 && Class != -1)
        {
            playButton.interactable = true;
        }
        else
        {
            playButton.interactable = false;
        }
    }
    public void Play()
    {
        PlayerPrefs.SetInt("newgame", 0);
        PlayerPrefs.Save();
        Destroy(Male.transform.parent.gameObject);
        SaveData data = new SaveData();
        data.Name = lasttext;
        data.Class = Class;
        data.male = male;
        File.Create(Application.dataPath+"/slot"+PlayerPrefs.GetInt("slot")+".json").Close();
        File.WriteAllText(Application.dataPath + "/slot" + PlayerPrefs.GetInt("slot") + ".json", JsonUtility.ToJson(data, true));
        player.SetActive(true);
        tutorial.SetActive(true);
        Destroy(gameObject);
    }
}
