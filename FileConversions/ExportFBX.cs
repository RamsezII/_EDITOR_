#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityEditor.Formats.Fbx.Exporter;

partial class Util_e
{
    [MenuItem("Tools/" + nameof(Util_e) + "/" + nameof(ExportSelectionToFBX))]
    [MenuItem("Assets/" + nameof(Util_e) + "/" + nameof(ExportSelectionToFBX))]
    static void ExportSelectionToFBX()
    {
        GameObject selected = Selection.activeGameObject;
        string selectedPath = AssetDatabase.GetAssetPath(selected);
        string savedPath = ModelExporter.ExportObject(selectedPath, selected);
        Debug.Log($"Exported \"{selected.name}\" as FBX: ({savedPath})", selected);
    }
}
#endif