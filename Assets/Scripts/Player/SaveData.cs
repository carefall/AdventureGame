using System;
using System.Collections.Generic;
using UnityEngine;
[Serializable]

public class SaveData 
{
    public string Name;
    public int Class;
    public bool male;
    public Vector3 position=new Vector3(146.360001f, 0f, 205.350006f), rotation= new Vector3(0, 0,0);
    public int level = 1;
    public int hp;
    public long timeStampInTicks = System.DateTime.Now.Ticks;

    [Serializable]
    public struct QuestStates
    {
        public int id;
        public int[] states;
        public int[] amounts;
    }
    [Serializable]
    public struct SaveEntity
    {
        public string name;
        public Vector3 position;
        public Quaternion rotation;
    }

    public QuestStates[] quests;
    [Serializable]
    public struct InventoryItem
    {
        public Vector2Int position;
        public string itemUniqueName;
        public int amount;
    }
    public InventoryItem[] inventoryContent;
    internal int[] completedQuests = new int[0];
    internal int[] failedQuests = new int[0];
    internal string[] deadUniqueNPCs = new string[0];
    internal int currentQuest;
    public SaveEntity[] savedUniqueEntities = new SaveEntity[0];
}
