#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace _EDITOR_
{
    internal static class AutoRefreshBeforePlay
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void OnBeforeSceneLoad()
        {
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        //--------------------------------------------------------------------------------------------------------------

        static void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            switch (state)
            {
                case PlayModeStateChange.ExitingEditMode:
                case PlayModeStateChange.EnteredEditMode:
                    {
                        Debug.Log("🔁 Auto Refresh avant Play...".ToSubLog());

                        double start = Util.TotalMilliseconds;
                        AssetDatabase.Refresh();
                        double stop = Util.TotalMilliseconds;

                        Debug.Log($"🔁 Auto Refresh terminé en {(stop - start) / 1000.0}s.".ToSubLog());
                    }
                    break;
            }
        }
    }
}
#endif