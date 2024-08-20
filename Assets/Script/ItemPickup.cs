using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour, IInteractable
{
    public Item item;
    private AudioButton audioButton;

    public void Interact()
    {
        audioButton = FindObjectOfType<AudioButton>();
        Pickup();
    }

    void Pickup()
    {
        if (audioButton != null)
        {
            audioButton.PlayPickUpSound();
        }
        Debug.Log("Picked up " + item.itemName);
        InventoryManager.Instance.Add(item);
        Destroy(gameObject); // Hapus item dari dunia setelah dipungut
    }
}
