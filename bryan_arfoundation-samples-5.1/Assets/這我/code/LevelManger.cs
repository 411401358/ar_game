using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class LevelManger : MonoBehaviour

{
    public GameObject chicken;
    public Camera mainCamera;      // ��i�D��v��
    public float minDistance = 2f; // �̵u�Z��
    public float maxDistance = 4f; // �̻��Z��
    public float horizontalRange = 100.0f; // ���k�����d��
    public float verticalRange = 100.5f;   // �W�U�����d��
    float shoottime;

    public GameObject pauseMenuUI; // 拖進你的 UI Panel
    private bool isPaused = false;


    int HP = 4;
    int score = 0;
    int status = 0;
    public GameObject ui_start;
    public GameObject ui_Over;
    public GameObject ui_again;
    public Text text_hp;
    public Text text_score;
    public Text text_show_score;
    public float timespace = 2.5f;
    public GameObject ui_shoot;

    public GameObject ui_pause;
    public AudioSource bgmSource; // 拖背景音樂用的 AudioSource

    public AudioClip bgmInGame;
    public AudioClip bgmGameOver;
    public AudioClip bgmMenu;
    public AudioClip uiClickSound; // 拖進 UI 點擊音效
    public string uiAudioTag = "SFX"; // 對應 AudioSource 的 Tag


    public BulletShooter shootManager;  // 拖進有掛 BulletShooter 的物件



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayBGM(bgmMenu); // 在遊戲一開始播放背景音樂

        RegisterButtonSounds(); // 加這行

        pauseMenuUI.SetActive(false);
        ui_pause.SetActive(false);
        ui_Over.SetActive(false);
        ui_again.SetActive(false);
        text_hp.enabled = false;
        text_score.enabled = false;
        text_show_score.enabled = false;
        shoottime = Time.time + 2.0f;
        text_hp.text = "生命值: " + new string('❤', HP);
        text_score.text = "☠:  " + score;


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
                    ui_pause.SetActive(true);
                    text_score.enabled = true;
                    text_hp.enabled = true;
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
                    GameOver();
                }
                break;
            case 2:
                break;

        }

        // 讓你按 Esc 鍵也能暫停
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
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
    public void TogglePause()
    {

        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f;
            pauseMenuUI.SetActive(true);
            ui_pause.SetActive(false);
        }
        else
        {
            Time.timeScale = 1f;
            pauseMenuUI.SetActive(false);
        }
    }

    public void ResumeGame()
    {

        ui_pause.SetActive(true);
        ui_shoot.SetActive(true);
        isPaused = false;
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
    }

    public void startGame()
    {
        PlayBGM(bgmInGame, 0.6f);

        HP = 4;
        score = 0;

        text_hp.enabled = true;
        text_score.enabled = true;
        text_show_score.enabled = false; // 確保分數顯示隱藏
        text_hp.text = "生命值: " + new string('❤', HP);
        text_score.text = "☠: " + score;

        ui_shoot.SetActive(true);
        status = 1;
        ui_start.SetActive(false);
        pauseMenuUI.SetActive(false);
    }
    void hurt()
    {
        HP = Mathf.Max(0, HP - 1); // 保證 HP 不會小於 0
        text_hp.text = "生命值: " + new string('❤', HP);
        Debug.Log("HP: " + HP);

    }

    public void AddScore(int amount = 1)
    {
        score += amount;
        text_score.text = "☠: " + score;
        Debug.Log("得分 +" + amount + "，目前分數：" + score);
    }


    public void SpawnEnemy()
    {
        if (chicken == null || mainCamera == null) return;

        // �H���Z��
        float distance = Random.Range(minDistance, maxDistance);

        // �H����V�����]�b�e�誺�Y�ӽd�򤺡^
        Vector3 forward = mainCamera.transform.forward;
        Vector3 right = mainCamera.transform.right;
        Vector3 up = mainCamera.transform.up;

        Vector3 offset =
            right * Random.Range(-horizontalRange, horizontalRange) +
            up * Random.Range(-verticalRange, verticalRange);

        Vector3 spawnPosition = mainCamera.transform.position + forward * distance + offset;

        // �ͦ��ĤH
        Instantiate(chicken, spawnPosition, Quaternion.LookRotation(-forward));

    }
    public void play_again()
    {

        Time.timeScale = 1f;
        text_hp.enabled = true;
        ui_start.SetActive(false);
        ui_Over.SetActive(false);
        ui_again.SetActive(false);
        pauseMenuUI.SetActive(false); // 確保暫停畫面不在
        ui_pause.SetActive(true);     // 恢復暫停按鈕
        shoottime = Time.time + 2.0f;
        HP = 4;
        status = 1;
        text_hp.text = "生命值: " + new string('❤', HP);
        text_show_score.enabled = false; // 隱藏分數顯示
        text_score.text = "☠:  " + score;
        score = 0; // 重置分數

    }
    public void pause_game()
    {

        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            TogglePause();
        }
    }

    public void QuitGame()
    {

        GameOver();
        // 退出遊戲
    }

    void GameOver()
    {

        PlayBGM(bgmGameOver, 0.4f);
        status = 2;
        ui_Over.SetActive(true);
        ui_again.SetActive(true);

        text_show_score.text = "☠: " + score;
        text_show_score.enabled = true;
        ui_shoot.SetActive(false);
        ui_pause.SetActive(false);
        pauseMenuUI.SetActive(false);
        text_hp.enabled = false;
        text_score.enabled = false;

        GameObject[] destroyObj = GameObject.FindGameObjectsWithTag("DestroyOnGameOver");
        foreach (GameObject obj in destroyObj)
        {
            Destroy(obj);
        }

        timespace = 2.0f;
    }

    public void OnShootButtonClicked()
    {
        if (shootManager != null)
        {
            shootManager.Shoot();
        }
    }

    void PlayBGM(AudioClip clip, float volume = 0.5f)
    {
        if (bgmSource != null && clip != null)
        {
            bgmSource.Stop();
            bgmSource.clip = clip;
            bgmSource.volume = volume;
            bgmSource.Play();
        }
    }

    private HashSet<Button> buttonsWithSound = new HashSet<Button>();

    void RegisterButtonSounds()
    {
        GameObject sfxPlayer = GameObject.FindGameObjectWithTag(uiAudioTag);
        if (sfxPlayer == null || uiClickSound == null) return;

        AudioSource audioSource = sfxPlayer.GetComponent<AudioSource>();
        if (audioSource == null) return;

        // 從 Canvas 下去抓所有 UI 按鈕（包含非啟用）
        Transform canvasRoot = GameObject.Find("Canvas")?.transform;
        if (canvasRoot == null) return;

        Button[] buttons = canvasRoot.GetComponentsInChildren<Button>(true);

        foreach (Button btn in buttons)
        {
            // 避免重複註冊
            btn.onClick.RemoveListener(() => audioSource.PlayOneShot(uiClickSound));
            btn.onClick.AddListener(() => audioSource.PlayOneShot(uiClickSound));
        }
    }

}
