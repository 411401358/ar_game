using UnityEngine;
using UnityEngine.EventSystems;

public class ChickenManger : MonoBehaviour
{
    public Transform targetTrans;
    public GameObject bullet;
    public GameObject bulletPosition;
    float shoottime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetTrans = GameObject.Find("Main Camera").transform;
        shoottime = Time.time + 1.0f;
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
        Destroy(this.gameObject);

    }
}
