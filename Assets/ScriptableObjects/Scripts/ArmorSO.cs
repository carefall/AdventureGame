using NaughtyAttributes;
using System;
using UnityEditor;
using UnityEngine;

public class ArmorSO : ItemSO
{

#if UNITY_EDITOR
    [MenuItem("Items/Create Armor")]
    public static void Create()
    {
        var item = CreateInstance<ArmorSO>();
        int id = AssetDatabase.FindAssets("t: ArmorSO", new string[] { "Assets/ScriptableObjects/Items/Armor" }).Length;
        AssetDatabase.CreateAsset(item, "Assets/ScriptableObjects/Items/Armor/Armor" + id + ".asset");
        ItemList itemList = AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(AssetDatabase.FindAssets("t: ItemList")[0]), typeof(ItemList)) as ItemList;
        itemList.items.Add(item);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
#endif
    public ArmorType type;

    public enum ArmorType
    {
        ARMOR, HELMET, BOOTS, PANTS
    }
}
