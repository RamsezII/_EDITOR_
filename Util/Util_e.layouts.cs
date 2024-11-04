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

    [MenuItem("CONTEXT/" + nameof(VerticalLayoutGroup) + "/" + nameof(AutoHeight))]
    public static void AutoHeight(MenuCommand command)
    {
        VerticalLayoutGroup verticalLayoutGroup = (VerticalLayoutGroup)command.context;
        RectTransform rT = (RectTransform)verticalLayoutGroup.transform;

        Vector2 sizeDelta = rT.sizeDelta;
        sizeDelta.y = verticalLayoutGroup.preferredHeight;
        rT.sizeDelta = sizeDelta;
    }

    [MenuItem("CONTEXT/" + nameof(VerticalLayoutGroup) + "/" + nameof(AutoHeightParent))]
    public static void AutoHeightParent(MenuCommand command)
    {
        VerticalLayoutGroup verticalLayoutGroup = (VerticalLayoutGroup)command.context;
        RectTransform rT = (RectTransform)verticalLayoutGroup.transform.parent;

        Vector2 sizeDelta = rT.sizeDelta;
        sizeDelta.y = verticalLayoutGroup.preferredHeight;
        rT.sizeDelta = sizeDelta;
    }
}
#endif