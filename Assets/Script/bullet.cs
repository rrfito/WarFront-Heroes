using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float acceleration;
    public AudioClip spawnSound; // Tambahkan variabel untuk klip audio
    public float spawnSoundVolume = 1f; // Tambahkan variabel untuk volume suara
    public int damage = 1; // Jumlah damage yang diberikan peluru

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = transform.forward * acceleration;
        }

        // Mainkan suara spawn saat bullet di-spawn dengan volume yang ditentukan
        if (spawnSound != null)
        {
            AudioSource.PlayClipAtPoint(spawnSound, transform.position, spawnSoundVolume);
        }
    }

    void Update()
    {
        if (rb != null)
        {
            rb.AddForce(transform.forward * acceleration * Time.deltaTime, ForceMode.Acceleration);
        }

        // Jika bullet mencapai z=0 atau di bawahnya, maka bullet akan dihancurkan
        if (transform.position.z <= 0f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Benteng") || other.CompareTag("NPC"))
        {
            HandleCollision(other.gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Benteng") || collision.gameObject.CompareTag("NPC"))
        {
            HandleCollision(collision.gameObject);
        }
    }

    private void HandleCollision(GameObject target)
    {
        if (target.CompareTag("NPC"))
        {
            NPCHealth npcHealth = target.GetComponent<NPCHealth>();
            if (npcHealth != null)
            {
                npcHealth.TakeDamage(damage);
            }
        }

        // Hancurkan peluru setelah tabrakan
        Destroy(gameObject);
    }
}
