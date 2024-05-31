#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

public static partial class Util_e
{
    [MenuItem("Assets/" + nameof(Util_e) + "." + nameof(LogAnimatorInfos_short))]
    static void LogAnimatorInfos_shorthash() => LogAnimatorInfos_shorthash((AnimatorController)Selection.activeObject);

    [MenuItem("CONTEXT/" + nameof(Animator) + "/" + nameof(Util_e) + "." + nameof(LogAnimatorInfos_short))]
    static void LogAnimatorInfos_short(MenuCommand command) => LogAnimatorInfos_shorthash(((Animator)command.context).runtimeAnimatorController as AnimatorController);

    [System.Obsolete("Use LogAnimatorInfos_fullhash instead")]
    public static void LogAnimatorInfos_shorthash(this AnimatorController animator)
    {
        string layersEnum = "public enum Layers : byte { ";
        var statesEnums = new System.Collections.Generic.List<string>();

        foreach (AnimatorControllerLayer layer in animator.layers)
        {
            layersEnum += layer.name + ", ";

            string statesEnum = "public enum " + layer.name + "States\n{\n";

            RefAllStates(layer.stateMachine, ref statesEnum);

            statesEnums.Add(statesEnum + "}");
        }

        layersEnum += " _last_ }\n\n";

        foreach (string statesEnum in statesEnums)
            layersEnum += statesEnum + "\n\n";

        layersEnum += "public enum Parameters\n{\n";

        foreach (var p in animator.parameters)
            layersEnum += "    " + p.name + " = " + p.nameHash + ",\n";

        layersEnum += "}";

        layersEnum.WriteToClipboard();
        Debug.Log(layersEnum);

        static void RefAllStates(AnimatorStateMachine stateMachine, ref string log, string branch = default)
        {
            if (stateMachine.stateMachines != null)
                foreach (var subStateMachine in stateMachine.stateMachines)
                    RefAllStates(subStateMachine.stateMachine, ref log, branch + subStateMachine.stateMachine.name + "_");

            if (stateMachine.states != null)
                foreach (var subState in stateMachine.states)
                    log += "    " + branch + subState.state.name + " = " + subState.state.nameHash + ",\n";
        }
    }
}
#endif