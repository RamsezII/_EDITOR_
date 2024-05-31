#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public static partial class Util_e
{
    [MenuItem("CONTEXT/" + nameof(VerticalLayoutGroup) + "/" + nameof(LogPreferredHeight))]
    public static void LogPreferredHeight(MenuCommand command)
    {
        VerticalLayoutGroup verticalLayoutGroup = (VerticalLayoutGroup)command.context;
        Debug.Log($"Preferred Height: {verticalLayoutGroup.preferredHeight}");
    }
}
#endif