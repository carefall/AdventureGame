using NaughtyAttributes;
using UnityEditor;
using UnityEngine;

public class WeaponSO : ItemSO
{
    public enum Type 
    {
        Single, Shield, Bow, Double
    }
#if UNITY_EDITOR
    [MenuItem("Items/Create Weapon")]
    public static void Create()
    {
        var item = CreateInstance<WeaponSO>();
        int id = AssetDatabase.FindAssets("t: WeaponSO", new string[] { "Assets/ScriptableObjects/Items/Weapons" }).Length;
        AssetDatabase.CreateAsset(item, "Assets/ScriptableObjects/Items/Weapons/Weapon" + id + ".asset");
        ItemList itemList = AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(AssetDatabase.FindAssets("t: ItemList")[0]), typeof(ItemList)) as ItemList;
        itemList.items.Add(item);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
#endif
    public int damage;
    public Type type;
    public GameObject model;
    public string GetDrawAnimationName()
    {
        switch (type)
        {
            case Type.Single:
                return "DrawSword";
            case Type.Double:
                return "DrawDouble";
            case Type.Bow:
                return "DrawBow";
            default:
                return "";
        }
    }
    public string GetSheatheAnimationName()
    {
        switch (type)
        {
            case Type.Single:
                return "SheatheSword";
            case Type.Double:
                return "SheatheDouble";
            case Type.Bow:
                return "SheatheBow";
            default:
                return "";
        }
    }
}
