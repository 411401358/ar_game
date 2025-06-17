using UnityEngine;
using UnityEngine.EventSystems;

public class ChickenManger : MonoBehaviour
{
    public Transform targetTrans;
    public GameObject bullet;
    public GameObject bulletPosition;
    float shoottime;

    public AudioClip deathSound; // 拖雞死掉的音效
    private AudioSource audioSource;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetTrans = GameObject.Find("Main Camera").transform;
        shoottime = Time.time + 1.0f;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.LookAt(targetTrans);
        if (Time.time > shoottime)
        {

            Instantiate(bullet, bulletPosition.transform.position, bulletPosition.transform.rotation);
            shoottime = Time.time + 2.0f;
        }
    }
    private void OnMouseDown()
{
    if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
        return; // 點到 UI，不處理

    var level = GameObject.Find("Level")?.GetComponent<LevelManger>();
    if (level != null)
    {
        level.AddScore();
    }

    // 播放音效（這招不需要 AudioSource，也不怕物件被刪）
    if (deathSound != null)
    {
        AudioSource.PlayClipAtPoint(deathSound, transform.position);
    }

    Destroy(this.gameObject);
}

}
