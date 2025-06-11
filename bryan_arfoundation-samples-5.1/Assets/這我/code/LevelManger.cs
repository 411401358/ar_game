using UnityEngine;
using UnityEngine.UI;

public class LevelManger : MonoBehaviour

{
    public GameObject chicken;
    public Camera mainCamera;      // 拖進主攝影機
    public float minDistance = 2f; // 最短距離
    public float maxDistance = 4f; // 最遠距離
    public float horizontalRange = 100.0f; // 左右偏移範圍
    public float verticalRange = 100.5f;   // 上下偏移範圍
    float shoottime;
    int HP = 4;
    int status = 0;
    public GameObject ui_start;
    public GameObject ui_Over;
    public GameObject ui_again;
    public Text text_hp;
    public float timespace = 2.0f;
    public GameObject ui_shoot;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ui_Over.SetActive(false);
        ui_again.SetActive(false);
        text_hp.enabled = false;
        shoottime = Time.time + 2.0f;
        text_hp.text = "HP: " + HP;
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (status)
        {
            case 0:

                break;
            case 1:
                if (Time.time > shoottime)
                {
                    SpawnEnemy();
                    shoottime = Time.time + timespace;
                    if (timespace > 0.8f)
                    {
                        timespace -= 0.1f;
                    }
                    
                }
                ui_shoot.SetActive(true);
                if (HP <= 0)
                {
                    ui_Over.SetActive(true);
                    ui_again.SetActive(true);
                    ui_shoot.SetActive(false);
                    status = 2;
                    GameObject[] destroyObj = GameObject.FindGameObjectsWithTag("DestroyOnGameOver");
                    foreach (GameObject obj in destroyObj)
                    {
                        Destroy(obj);
                    }
                    text_hp.enabled = false;
                    timespace = 2.0f;
                }
                break;
            case 2:
                break;

        }
        //if (status == 1) { 
        //    if (HP > 0) { 
        //        if (Time.time > shoottime)
        //        {
        //            SpawnEnemy();
        //            shoottime = Time.time + 2.0f;
        //        }
        //    }
        //    else
        //    {
        //        ui_Over.SetActive(true);
        //    }
        //}
    }
    public void startGame()
    {
        text_hp.enabled = true;
        status = 1;
        ui_start.SetActive(false);
    }
    void hurt()
    {
        HP = HP - 1;
        text_hp.text = "HP: " + HP;
        Debug.Log("HP: " + HP);
    }
    public void SpawnEnemy()
    {
        if (chicken == null || mainCamera == null) return;

        // 隨機距離
        float distance = Random.Range(minDistance, maxDistance);

        // 隨機方向偏移（在前方的某個範圍內）
        Vector3 forward = mainCamera.transform.forward;
        Vector3 right = mainCamera.transform.right;
        Vector3 up = mainCamera.transform.up;

        Vector3 offset =
            right * Random.Range(-horizontalRange, horizontalRange) +
            up * Random.Range(-verticalRange, verticalRange);

        Vector3 spawnPosition = mainCamera.transform.position + forward * distance + offset;

        // 生成敵人
        Instantiate(chicken, spawnPosition, Quaternion.LookRotation(-forward));
       
    }
    public void paly_again()
    {
        text_hp.enabled = true;
        ui_start.SetActive(false);
        ui_Over.SetActive(false);
        ui_again.SetActive(false);
        shoottime = Time.time + 2.0f;
        HP = 4;
        status = 1;
        text_hp.text = "HP: " + HP;
    }
}
