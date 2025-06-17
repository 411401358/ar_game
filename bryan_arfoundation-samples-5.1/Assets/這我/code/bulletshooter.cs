using UnityEngine;

public class BulletShooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;

    public AudioClip shootSound;
    private AudioSource audioSource;

    void Awake()  // 或用 Start() 也可以
    {
        audioSource = GetComponent<AudioSource>();
    }
    // �o�g�l�u
    public void Shoot()
    {
        // �b�۾�����m�e��@�I�ͦ��l�u
        Transform cam = Camera.main.transform;
        Vector3 spawnPos = cam.position + cam.forward * 0.5f;

        GameObject bullet = Instantiate(bulletPrefab, spawnPos, Quaternion.identity);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.linearVelocity = cam.forward * bulletSpeed;

        // �۰ʧR���l�u
        Destroy(bullet, 5f);

        if (shootSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(shootSound);
        }
    }
}
