using UnityEngine;

public class NpcBehaviour : MonoBehaviour, IInteractable
{
    public Npc npcData;
    private NpcUIManager npcUIManager;

    void Start()
    {
        npcUIManager = FindObjectOfType<NpcUIManager>();
    }

    public void Interact()
    {
        npcUIManager.UpdateUI(gameObject); // Kirim referensi GameObject NPC
    }
}
