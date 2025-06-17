// 給所有按鈕統一加點擊音
using UnityEngine;
using UnityEngine.UI;

public class UIButtonSoundManager : MonoBehaviour
{
    public AudioClip clickSound;

    void Start()
    {
        foreach (Button btn in FindObjectsByType<Button>(FindObjectsSortMode.None))
        {
            btn.onClick.AddListener(() =>
            {
                AudioSource.PlayClipAtPoint(clickSound, Camera.main.transform.position);
            });
        }
    }
}

