#if UNITY_EDITOR
using TMPro;
using UnityEditor;
using UnityEngine;

static partial class Util_e
{
    [MenuItem("CONTEXT/" + nameof(TMP_InputField) + "/" + nameof(_LogTextBounds))]
    [MenuItem("CONTEXT/" + nameof(TextMeshProUGUI) + "/" + nameof(_LogTextBounds))]
    static void _LogTextBounds(MenuCommand command) => Debug.Log(((Component)command.context).GetComponentInChildren<TextMeshProUGUI>().GetPreferredValues());
}
#endif