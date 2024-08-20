using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<Item> Items = new List<Item>();
    public Transform itemContent;
    public GameObject inventoryItem;
    public GameObject UIinventory;
    private bool isInventoryActive = false; // Flag to track the active state of the inventory UI

    public TextMeshProUGUI itemDescriptionText; // Reference to the TextMeshProUGUI component for item description in the NPC panel
  
    private NpcUIManager npcUIManager; // Referensi ke NpcUIManager

    private void Awake()
    {
        Instance = this;
        UIinventory.SetActive(false); // Set UIinventory inactive initially
    }

    private void Start()
    {
        npcUIManager = FindObjectOfType<NpcUIManager>();
    }

    public void Add(Item item)
    {
        // Check if item already exists in the inventory
        Item existingItem = Items.Find(i => i.itemName == item.itemName);
        if (existingItem != null)
        {
            // If item exists, increase the stock
            existingItem.stock += item.stock;
        }
        else
        {
            // If item does not exist, add it to the inventory
            Items.Add(item);
        }
    }

    public void Remove(Item item)
    {
        // Check if item exists in the inventory
        Item existingItem = Items.Find(i => i.itemName == item.itemName);
        if (existingItem != null)
        {
            // Decrease the stock by 1
            if (existingItem.stock > 1)
            {
                existingItem.stock -= 1;
            }
            else
            {
                // If stock is 1 or less, remove the item
                Items.Remove(existingItem);
            }
        }
    }

    public void ListItems()
    {
        // Toggle the active state of UIinventory
        isInventoryActive = !isInventoryActive;
        UIinventory.SetActive(isInventoryActive);

        if (isInventoryActive)
        {
            // Clear existing items in the UI
            foreach (Transform item in itemContent)
            {
                Destroy(item.gameObject);
            }

            // Add items to the UI
            foreach (var item in Items)
            {
                GameObject obj = Instantiate(inventoryItem, itemContent);
               
                var itemIcon = obj.transform.Find("itemIcon").GetComponent<Image>();
                var itemStock = obj.transform.Find("stock").GetComponent<Text>(); // Assuming there is a Text component to show the stock

              
                itemIcon.sprite = item.itemIcon;
                itemStock.text = item.stock.ToString(); // Display the stock count

                // Add button listener
                Button itemButton = obj.GetComponent<Button>();
                itemButton.onClick.AddListener(() => ShowItemInNpcPanel(item));
            }
        }
    }

    private void ShowItemInNpcPanel(Item item)
    {
        if (npcUIManager != null)
        {
            npcUIManager.ShowItemDescription(item);
        }
    }
}
