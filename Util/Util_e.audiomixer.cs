#if UNITY_EDITOR
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;

partial class Util_e
{
    [MenuItem("Assets/" + nameof(Util_e) + "." + nameof(LogAudioMixerParameters))]
    static void LogAudioMixerParameters() => LogExposedParameters((AudioMixer)Selection.activeObject);

    [MenuItem("CONTEXT/" + nameof(AudioMixer) + "/" + nameof(Util_e) + "." + nameof(LogExposedParameters))]
    static void LogExposedParameters(MenuCommand command) => LogExposedParameters((AudioMixer)command.context);

    static void LogExposedParameters(in AudioMixer mixer)
    {
        StringBuilder log = new();
        SerializedObject dynMixer = new(mixer);
        SerializedProperty parameters = dynMixer.FindProperty("m_ExposedParameters");

        log.AppendLine("public enum Parameters : byte\n{");

        for (int i = 0; i < parameters.arraySize; i++)
        {
            SerializedProperty param = parameters.GetArrayElementAtIndex(i);
            SerializedProperty nameProp = param.FindPropertyRelative("name");
            log.AppendLine($"{nameProp.stringValue},");
        }

        log.Append("}");

        string _log = log.ToString();

        Debug.Log(_log);
        GUIUtility.systemCopyBuffer = _log;
    }
}
#endif