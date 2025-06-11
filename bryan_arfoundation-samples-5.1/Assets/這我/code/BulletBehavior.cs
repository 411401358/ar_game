using UnityEngine;

using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        string targetName = other.gameObject.name;

        if (targetName.Contains("Cube") || targetName.Contains("Fireball"))
        {
            Destroy(other.gameObject); // 銷毀被打到的物體
            Destroy(gameObject);       // 銷毀子彈自己
        }
        else
        {
            // 若非目標，也可選擇讓子彈自動消失
            Destroy(gameObject);
        }
    }
}

