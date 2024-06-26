﻿#if UNITY_EDITOR
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;

public static partial class Util_e
{
    [MenuItem("CONTEXT/" + nameof(AudioSource) + "/" + nameof(_EDITOR_) + "/" + nameof(PlayAudio))]
    static void PlayAudio(MenuCommand menuCommand) => ((AudioSource)menuCommand.context).Play();


    [MenuItem("Assets/" + nameof(_EDITOR_) + "/" + nameof(ExportExposedParametersAsEnum))]
    static void ExportExposedParametersAsEnum() => ExportExposedParametersAsEnum((AudioMixer)Selection.activeObject);

    static void ExportExposedParametersAsEnum(in AudioMixer mixer)
    {
        SerializedObject dynMixer = new(mixer);
        SerializedProperty parameters = dynMixer.FindProperty("m_ExposedParameters");
        StringBuilder log = new("public enum Parameters\n{\n");

        if (parameters != null && parameters.isArray)
            for (int i = 0; i < parameters.arraySize; i++)
            {
                SerializedProperty param = parameters.GetArrayElementAtIndex(i);
                SerializedProperty nameProp = param.FindPropertyRelative("name");

                if (nameProp != null)
                    log.AppendLine($"{nameProp.stringValue},");
            }

        log.Append("}");
        string message = log.ToString();
        Debug.Log(message);
        message.WriteToClipboard();
    }
}
#endif