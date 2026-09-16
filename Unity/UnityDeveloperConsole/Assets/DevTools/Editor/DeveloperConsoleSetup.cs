using DevConsole.Core;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem.UI;
#endif

namespace DevConsole.EditorTools
{
    /// <summary>
    /// One-click setup: adds the Developer Console prefab (and FPS counter) to the active scene,
    /// creating an EventSystem only if the scene doesn't already have one — Unity does not
    /// support more than one active EventSystem per scene.
    /// </summary>
    public static class DeveloperConsoleSetup
    {
        private const string PrefabPath = "Assets/DevTools/PF_DeveloperConsole.prefab";

        [MenuItem("Tools/Developer Console/Setup In Scene", false, 0)]
        private static void SetupInScene()
        {
            if (Object.FindFirstObjectByType<DeveloperConsole>() != null)
            {
                Debug.LogWarning("Developer Console is already present in the scene.");
                return;
            }

            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(PrefabPath);
            if (prefab == null)
            {
                Debug.LogError($"Developer Console prefab not found at '{PrefabPath}'.");
                return;
            }

            var instance = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
            Undo.RegisterCreatedObjectUndo(instance, "Add Developer Console");

            EnsureEventSystem();

            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
            Selection.activeGameObject = instance;
        }

        [MenuItem("Tools/Developer Console/Setup In Scene", true)]
        private static bool ValidateSetupInScene()
        {
            return Object.FindFirstObjectByType<DeveloperConsole>() == null;
        }

        private static void EnsureEventSystem()
        {
            if (Object.FindFirstObjectByType<EventSystem>() != null)
            {
                return;
            }

            var eventSystemGO = new GameObject("EventSystem", typeof(EventSystem));
#if ENABLE_INPUT_SYSTEM
            eventSystemGO.AddComponent<InputSystemUIInputModule>();
#else
            eventSystemGO.AddComponent<StandaloneInputModule>();
#endif
            Undo.RegisterCreatedObjectUndo(eventSystemGO, "Add EventSystem");
        }
    }
}
