using UnityEngine;
using TMPro;
using Cinemachine;
using UnityEngine.UI;

public class NpcUIManager : MonoBehaviour
{
    public TextMeshProUGUI descriptionText; // Pastikan ini diisi di Inspector
    public GameObject npcUI; // Pastikan ini diisi di Inspector
    public CinemachineFreeLook freeLookCamera; // Pastikan ini diisi di Inspector
    public Image itemIconImage; // Tambahkan Image untuk ikon item
    public Button giveButton; // Button untuk memberikan item
    private bool isNpcUIActive;
    private Player player; // Referensi ke skrip Player
    private PlayerLives playerLives; // Referensi ke PlayerLives
    private InventoryManager inventoryManager;
    private Item selectedItem; // Item yang dipilih dari inventory
    private GameObject currentNpc; // NPC yang saat ini ditampilkan
    public TextMeshProUGUI descItem;
    private ScoreManager scoreManager;
    public TextMeshProUGUI itemname;
    public AudioButton audioButton;

    private void Start()
    {
        isNpcUIActive = false;
        npcUI.SetActive(isNpcUIActive); // Pastikan UI awalnya tidak aktif
        player = FindObjectOfType<Player>(); // Cari referensi ke pemain
        playerLives = player.GetComponent<PlayerLives>(); // Cari referensi ke PlayerLives
        inventoryManager = FindObjectOfType<InventoryManager>();
        scoreManager = FindObjectOfType<ScoreManager>();

        // Pastikan giveButton memiliki listener
        giveButton.onClick.AddListener(OnGiveButtonClicked);
      
    }

    public void UpdateUI(GameObject npc)
    {
        isNpcUIActive = true;
        npcUI.SetActive(isNpcUIActive); // Aktifkan UI
        Npc npcData = npc.GetComponent<NpcBehaviour>().npcData; // Ambil data NPC
        descriptionText.text = npcData.description;
        currentNpc = npc;

        // Nonaktifkan gerakan pemain dan input kamera
        if (player != null)
        {
            player.canMove = false;
        }
        if (freeLookCamera != null)
        {
            freeLookCamera.enabled = false;
        }

       
    }

    public void OnExitButtonClicked()
    {
        isNpcUIActive = false;
        npcUI.SetActive(isNpcUIActive); // Nonaktifkan UI

        // Aktifkan kembali gerakan pemain dan input kamera
        if (player != null)
        {
            player.canMove = true;
        }
        if (freeLookCamera != null)
        {
            freeLookCamera.enabled = true;
        }

        // Nonaktifkan inventaris
        if (inventoryManager != null)
        {
            inventoryManager.UIinventory.SetActive(false);
        }

        // Bersihkan item yang dipilih
        selectedItem = null;
        itemIconImage.sprite = null;
        descItem.text=null;
        itemname.text = null;

      // Reset currentNpc
      currentNpc = null;
    }

    public void ShowItemDescription(Item item)
    {
        if (isNpcUIActive)
        {
            itemname.text =item.itemName;
            itemIconImage.sprite = item.itemIcon;
            selectedItem = item;
            descItem.text = item.description;
        }
    }

    private void OnGiveButtonClicked()
    {
        if (currentNpc != null && selectedItem != null)
        {
            Npc npcData = currentNpc.GetComponent<NpcBehaviour>().npcData;
            if (selectedItem.itemName == npcData.item.ToString())
            {
                Debug.Log("berhasil");
                // Jalankan skrip NPC follow
                NpcFollow npcFollow = currentNpc.GetComponent<NpcFollow>();
                if (npcFollow != null)
                {
                    npcFollow.isFollowing = true;
                    inventoryManager.Remove(selectedItem);
                }
                // Mainkan suara berhasil
                if (audioButton != null)
                {
                    audioButton.PlaySuccessSound();
                }
                // Nonaktifkan UI NPC
                OnExitButtonClicked();
            }
            else
            {
                Debug.Log("Item tidak sesuai");
                // Kurangi nyawa pemain
                if (playerLives != null)
                {
                    playerLives.LoseLife();
                    if (!playerLives.HasLivesLeft())
                    {
                        // Tambahkan logika jika nyawa habis
                        Debug.Log("Player kehabisan nyawa!");
                    }
                }
                // Mainkan suara gagal
                if (audioButton != null)
                {
                    audioButton.PlayFailureSound();
                }
            }
        }
    }

}