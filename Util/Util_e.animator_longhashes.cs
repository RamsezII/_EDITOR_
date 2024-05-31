﻿#if UNITY_EDITOR
using System.Text;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

public static partial class Util_e
{
    [MenuItem("Assets/" + nameof(Util_e) + "." + nameof(LogAnimatorInfos_fullhash))]
    static void LogAnimatorInfos_fullhash() => LogAnimatorInfos_fullhash((AnimatorController)Selection.activeObject);

    [MenuItem("CONTEXT/" + nameof(Animator) + "/" + nameof(Util_e) + "." + nameof(LogAnimatorInfos_fullhash))]
    static void LogAnimatorInfos_fullhash(MenuCommand command) => LogAnimatorInfos_fullhash(((Animator)command.context).runtimeAnimatorController as AnimatorController);

    public static void LogAnimatorInfos_fullhash(this AnimatorController animator)
    {
        StringBuilder log = new();

        if (animator.layers.Length > 0)
        {
            log.Append("public enum Layers : byte { ");
            foreach (AnimatorControllerLayer layer in animator.layers)
                log.Append(layer.name + ", ");
            log.AppendLine("_last_ }");
        }

        if (animator.layers.Length > 0)
            foreach (AnimatorControllerLayer layer in animator.layers)
            {
                log.AppendLine();
                log.AppendLine($"public enum {layer.name}States");
                log.AppendLine("{");

                AllStates(layer.stateMachine, string.Empty, layer.name);

                log.AppendLine("}");

                void AllStates(in AnimatorStateMachine stateMachine, in string branch_name, in string branch_hash)
                {
                    if (stateMachine.stateMachines != null)
                        foreach (ChildAnimatorStateMachine subStateMachine in stateMachine.stateMachines)
                            if (string.IsNullOrEmpty(branch_name))
                                AllStates(subStateMachine.stateMachine, $"{subStateMachine.stateMachine.name}_", $"{branch_hash}.{subStateMachine.stateMachine.name}");
                            else
                                AllStates(subStateMachine.stateMachine, $"{branch_name}_{subStateMachine.stateMachine.name}_", $"{branch_hash}.{subStateMachine.stateMachine.name}");

                    if (stateMachine.states != null)
                        foreach (var subState in stateMachine.states)
                        {
                            log.Append($"    {branch_name}{subState.state.name} = ");
                            if (string.IsNullOrEmpty(branch_hash))
                                log.AppendLine(subState.state.nameHash + ",");
                            else
                            {
                                string fullhashname = branch_hash + "." + subState.state.name;
                                log.AppendLine(Animator.StringToHash(fullhashname) + ",");
                            }
                        }
                }
            }

        if (animator.parameters.Length > 0)
        {
            log.AppendLine();
            log.AppendLine("public enum Parameters");
            log.AppendLine("{");

            foreach (var p in animator.parameters)
                log.AppendLine($"    {p.name} = {p.nameHash},");

            log.AppendLine("}");
        }

        string _log = log.ToString();
        Debug.Log(_log);
        _log.WriteToClipboard();
    }
}
#endif