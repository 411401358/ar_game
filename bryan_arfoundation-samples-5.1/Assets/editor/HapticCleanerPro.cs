
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System;
using System.Linq;

public static class HapticCleanerPro
{
    [MenuItem("Tools/🔥 強化版清除 Deprecated Haptic Events")]
    public static void RemoveAllDeprecatedHaptics()
    {
        int removedCount = 0;
        int scannedCount = 0;

        var allGameObjects = Resources.FindObjectsOfTypeAll<GameObject>();

        foreach (var go in allGameObjects)
        {
            // 排除 Asset 資源與 Project Settings
            if (string.IsNullOrEmpty(go.scene.name))
                continue;

            var components = go.GetComponents<Component>();
            foreach (var comp in components)
            {
                scannedCount++;
                if (comp == null) continue;

                var type = comp.GetType();
                var typeName = type.FullName;

                if (typeName.Contains("HapticEvents") || typeName.Contains("XRInteractorHaptics"))
                {
                    Debug.Log($"🧹 移除: {typeName} on {go.name}", go);
                    GameObject.DestroyImmediate(comp);
                    removedCount++;
                }
            }
        }

        Debug.Log($"✅ 強化清除完成，掃描 {scannedCount} 個元件，移除 {removedCount} 個 Deprecated Haptic 元件。");
    }
}
#endif
