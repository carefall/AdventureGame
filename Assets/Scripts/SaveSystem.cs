using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static SaveData;

public class SaveSystem
{


    public static void CreateNewSaveFile(int slot)
    {
        File.Create(Application.dataPath+$"/slot{slot}.json").Close();
        SaveData data = new();
        data.completedQuests = new int[0];
        data.failedQuests = new int[0];
        data.deadUniqueNPCs = new string[0];
        data.currentQuest = 0;
        List<InventoryItem> items = new();
        data.inventoryContent = items.ToArray();
        File.WriteAllText(Application.dataPath + $"/slot{slot}.json", JsonUtility.ToJson(data, true));
    }

    public static SaveData GetSaveData()
    {
        return (SaveData)JsonUtility.FromJson(File.ReadAllText(Application.dataPath + $"/slot{PlayerPrefs.GetInt("slot")}.json"), typeof(SaveData));
    }

    public static bool SaveDataExists()
    {
        return File.Exists(Application.dataPath + $"/slot{PlayerPrefs.GetInt("slot")}.json");
    }

    public static void Save(PlayerData data)
    {
        List<Entity>entities = data.GetEntities();
        SaveData save = new();
        save.male = data.IsMale();
        save.position = data.transform.position;
        save.rotation = data.transform.eulerAngles;
        save.level = data.GetLevel();
        save.hp = data.GetHp();
        save.timeStampInTicks = DateTime.Now.Ticks;
        save.quests = QuestsDataToStates(data.questsCache);
        save.completedQuests = data.GetCompletedQuests();
        save.failedQuests = data.GetFailedQuests();
        save.deadUniqueNPCs = data.GetDeadUniqueNPCs();
        Quest q = data.GetCurrentQuest();
        save.currentQuest = q!=null ? q.id : -1;
        List<SaveEntity> se = new List<SaveEntity>();
        foreach (Entity entity in entities)
        {
            if (entity.uniqueName != null && entity.uniqueName.Length != 0)
            {
                se.Add(new SaveEntity()
                {
                    name = entity.uniqueName,
                    position = entity.transform.position,
                    rotation = entity.transform.rotation
                });
            }
        }
        save.savedUniqueEntities = se.ToArray();
    // inventoryContent = InventoryWindow.instance.GetContent()

    File.WriteAllText(Application.dataPath + $"/slot{PlayerPrefs.GetInt("slot")}.json", JsonUtility.ToJson(save, true));
    }

    private static QuestStates[] QuestsDataToStates(List<Quest> cache)
    {
        QuestStates[] questsStates = new QuestStates[cache.Count];
        for (int i = 0; i < cache.Count; i++)
        {
            int[] states = new int[cache[i].objectives.Length];
            int[] amounts = new int[cache[i].objectives.Length];
            for (int j = 0; j < cache[i].objectives.Length; j++)
            {
                states[j] = cache[i].objectives[j].state;
                amounts[j] = cache[i].objectives[j].currentAmount;
            }
            QuestStates questStates = new()
            {
                id = cache[i].id,
                states = states,
                amounts = amounts
            };
            questsStates[i] = questStates;
        }
        return questsStates;
    }

}
