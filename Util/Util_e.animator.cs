#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

public static partial class Util_e
{
    [MenuItem("CONTEXT/" + nameof(Animator) + "/" + nameof(_EDITOR_) + "/" + nameof(LogHumanScale))]
    static void LogHumanScale(MenuCommand command) => Debug.Log(((Animator)command.context).humanScale);

    [MenuItem("CONTEXT/" + nameof(Animator) + "/" + nameof(_EDITOR_) + "/" + nameof(LogAnimatorClips))]
    static void LogAnimatorClips(MenuCommand command) => LogAnimatorClips((Animator)command.context);

    public static void LogAnimatorClips(this Animator animator)
    {
        string log = "";
        int i = 0;

        foreach (var clip in animator.runtimeAnimatorController.animationClips)
            log += (i++) + " : " + clip.name + "\n";

        Debug.Log(log);
    }

    [MenuItem("Assets/" + nameof(_EDITOR_) + "/" + nameof(LogAnimatorInfos))]
    static void LogAnimatorInfos() => LogAnimatorInfos((AnimatorController)Selection.activeObject);

    [MenuItem("CONTEXT/" + nameof(Animator) + "/" + nameof(_EDITOR_) + "/" + nameof(LogAnimatorInfos))]
    static void LogAnimatorInfos(MenuCommand command) => LogAnimatorInfos(((Animator)command.context).runtimeAnimatorController as AnimatorController);

    public static void LogAnimatorInfos(this AnimatorController animator)
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