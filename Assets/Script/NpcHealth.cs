using UnityEngine;

public class NPCHealth : MonoBehaviour
{
    public int maxHealth = 5; // Jumlah maksimum kesehatan NPC
    private int currentHealth; // Kesehatan saat ini

    public GameObject healthIcon; // GameObject untuk ikon darah
    public Sprite[] healthSprites; // Array sprite untuk tingkat kesehatan
    public AudioClip damageSound; // Klip audio untuk suara damage
    private AudioSource audioSource; // Referensi ke AudioSource
    private EndGameManager endGameManager;

    private void Start()
    {
        currentHealth = maxHealth; // Set kesehatan awal ke nilai maksimum
        audioSource = GetComponent<AudioSource>(); // Dapatkan referensi ke AudioSource
        endGameManager = FindObjectOfType<EndGameManager>();
        if (audioSource == null)
        {
            // Jika tidak ada AudioSource di gameObject, tambahkan satu
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    // Metode untuk mengurangi kesehatan NPC
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount; // Kurangi kesehatan sesuai dengan jumlah kerusakan

        // Mainkan suara damage jika ada
        if (damageSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(damageSound);
        }

        // Periksa apakah kesehatan mencapai atau kurang dari 0
        if (currentHealth <= 0)
        {
            endGameManager.ShowGameOver();
            DestroyNPC();
            

        }
        else
        {
            // Perbarui sprite ikon darah
            UpdateHealthIcon();
        }
    }

    // Metode untuk menghancurkan NPC
    private void DestroyNPC()
    {
        // Tambahkan logika lain jika diperlukan sebelum menghancurkan NPC
        Destroy(gameObject); // Hancurkan objek NPC
        Debug.Log("NPC dihancurkan!");
    }

    // Metode untuk memperbarui sprite ikon darah
    private void UpdateHealthIcon()
    {
        // Pastikan indeks sprite dalam rentang yang valid
        int spriteIndex = Mathf.Clamp(currentHealth, 0, healthSprites.Length - 1);

        // Atur sprite ikon darah sesuai dengan jumlah kesehatan yang tersisa
        if (healthIcon != null)
        {
            SpriteRenderer spriteRenderer = healthIcon.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.sprite = healthSprites[spriteIndex];
            }
        }
    }
}
