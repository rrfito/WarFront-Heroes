using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    public float rotationSpeed = 5.0f;
    private float currentYRotation = 0f;

    void Start()
    {
        // Mendapatkan rotasi awal di sumbu Y
        currentYRotation = transform.eulerAngles.y;
    }

    // Update dipanggil sekali per frame
    void Update()
    {
        // Tambahkan rotasi berdasarkan waktu dan kecepatan
        currentYRotation += rotationSpeed * Time.deltaTime;

        // Tetapkan rotasi baru hanya pada sumbu Y
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, currentYRotation, transform.eulerAngles.z);
    }
}

