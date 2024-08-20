using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite itemIcon;
    public int itemID;
    [TextArea(15, 20)]
    public string description;
    public int stock;
}
