#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public class ResetGrabMove : MonoBehaviour
{
    [MenuItem("Tools/Fix Deprecated GrabMoveProvider")]
    static void Fix()
    {
        var providers = Resources.FindObjectsOfTypeAll<UnityEngine.XR.Interaction.Toolkit.Locomotion.Movement.GrabMoveProvider>();
        foreach (var provider in providers)
        {
            Undo.RecordObject(provider, "Reset Grab Move");

            SerializedObject so = new SerializedObject(provider);
            var grabMoveActionProp = so.FindProperty("m_GrabMoveAction");

            if (grabMoveActionProp != null)
            {
                var referenceProp = grabMoveActionProp.FindPropertyRelative("m_Reference");
                if (referenceProp != null)
                {
                    referenceProp.objectReferenceValue = null;
                    Debug.Log($"✅ Cleared deprecated Grab Move Action reference in: {provider.name}", provider);
                }
                else
                {
                    Debug.LogWarning($"⚠️ Failed to find 'm_Reference' inside m_GrabMoveAction for: {provider.name}", provider);
                }

                so.ApplyModifiedProperties();
            }
            else
            {
                Debug.LogWarning($"⚠️ Could not find property 'm_GrabMoveAction' on {provider.name}");
            }
        }
    }
}
#endif
