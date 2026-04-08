using UnityEngine;

[CreateAssetMenu(fileName = "ItemSO", menuName = "Custom Scriptable Object/New Item")]
public class ItemSO : ScriptableObject
{
    public string ItemName;
    public bool isStackable = false;
    public Texture icon;
    public GameObject prefab;
}