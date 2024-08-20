using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public GameObject bulletPrefab;  // Prefab peluru yang akan di-spawn
    public float spawnInterval = 1f; // Interval waktu antara spawn
    public float initialForce = 10f;  // Gaya awal peluru
    public float acceleration = 2f;  // Percepatan peluru
    public Transform player;         // Referensi ke transform pemain
    public float followRangeX = 10f; // Rentang spawn dari posisi objek spawn
    public AudioClip spawnSound;     // Klip audio untuk suara spawn

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnBullet();
            timer = 0f;
        }

        // Perbarui posisi objek spawn agar mengikuti sudut X dari pemain
        if (player != null)
        {
            Vector3 newPosition = transform.position;
            newPosition.x = Mathf.Clamp(player.position.x, -followRangeX, followRangeX);
            transform.position = newPosition;
        }
    }
    void SpawnBullet()
    {
        // Posisi spawn random dari -10 hingga 10 relatif terhadap posisi objek spawn
        float spawnPositionX = Random.Range(transform.position.x - 20f, transform.position.x + 20f);
        Vector3 spawnPosition = new Vector3(spawnPositionX, transform.position.y, transform.position.z);

        // Instantiate bullet
        GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);


        // Mengatur arah peluru agar bergerak ke belakang dengan gaya awal
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(Vector3.back * initialForce, ForceMode.Impulse);
            Bullet bulletScript = bullet.AddComponent<Bullet>();
            bulletScript.acceleration = acceleration;
            bulletScript.spawnSound = spawnSound;  // Setel suara spawn
        }

        // Mainkan suara spawn di posisi spawn
        AudioSource.PlayClipAtPoint(spawnSound, spawnPosition);
    }
}
