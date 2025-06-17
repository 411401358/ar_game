
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System;
using System.Linq;

public static class HapticCleaner
{
    [MenuItem("Tools/一鍵清除 Deprecated Haptic Events")]
    public static void RemoveDeprecatedHapticEvents()
    {
        int removedCount = 0;
        var allObjects = GameObject.FindObjectsOfType<GameObject>(true);

        foreach (var go in allObjects)
        {
            var components = go.GetComponents<Component>();
            foreach (var comp in components)
            {
                if (comp == null) continue;
                var type = comp.GetType();
                string typeName = type.Name;

                if (typeName.Contains("HapticEvents") || typeName.Contains("XRInteractorHaptics"))
                {
                    Debug.Log($"🧹 移除: {typeName} on {go.name}", go);
                    GameObject.DestroyImmediate(comp);
                    removedCount++;
                }
            }
        }

        Debug.Log($"✅ 清除完成，共移除 {removedCount} 個 Deprecated Haptic 元件。");
    }
}
#endif
