using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VRC.SDK3.Avatars.Components;
using VRC.SDK3.Avatars.ScriptableObjects;
#if UNITY_EDITOR
using UnityEditor.Animations;

namespace jp.illusive_isc.RirikaOptimizer
{
    [AddComponentMenu("")]
    internal class IllRirikaParamMesugakiFace : IllRirikaParam
    {
        VRCAvatarDescriptor descriptor;
        AnimatorController animator;

        private static readonly List<string> MenuParameters = new()
        {
            "mesugaki face",
            "mesugaki face random",
            "mesugaki face particle",
        };

        public IllRirikaParamMesugakiFace Initialize(
            VRCAvatarDescriptor descriptor,
            AnimatorController animator
        )
        {
            this.descriptor = descriptor;
            this.animator = animator;
            return this;
        }

        public IllRirikaParamMesugakiFace DeleteParam()
        {
            animator.parameters = animator
                .parameters.Where(parameter => !MenuParameters.Contains(parameter.name))
                .ToArray();
            return this;
        }

        public IllRirikaParamMesugakiFace DeleteFx()
        {
            foreach (
                var layer in animator.layers.Where(layer => layer.name is "LeftHand" or "RightHand")
            )
            {
                foreach (var sub in layer.stateMachine.stateMachines)
                    if (sub.stateMachine.name == "mesugaki face")
                    {
                        layer.stateMachine.RemoveStateMachine(sub.stateMachine);
                        break;
                    }
                foreach (var t in layer.stateMachine.anyStateTransitions)
                    t.conditions = t
                        .conditions.Where(c => c.parameter != "mesugaki face")
                        .ToArray();

                foreach (var state in layer.stateMachine.states)
                {
                    foreach (
                        var transition in state.state.transitions.Where(t => t.isExit).ToArray()
                    )
                        state.state.RemoveTransition(transition);
                }
            }
            return this;
        }

        public IllRirikaParamMesugakiFace DeleteFxBT()
        {
            foreach (var layer in animator.layers.Where(layer => layer.name == "MainCtrlTree"))
            {
                foreach (var state in layer.stateMachine.states)
                {
                    if (state.state.motion is BlendTree blendTree)
                    {
                        blendTree.children = blendTree
                            .children.Where(c => CheckBT(c.motion, MenuParameters))
                            .ToArray();
                    }
                }
            }
            return this;
        }

        public IllRirikaParamMesugakiFace DeleteVRCExpressions(
            VRCExpressionsMenu menu,
            VRCExpressionParameters param
        )
        {
            param.parameters = param
                .parameters.Where(parameter => !MenuParameters.Contains(parameter.name))
                .ToArray();

            foreach (var control in menu.controls)
            {
                if (control.name == "Gimmick")
                {
                    var expressionsSubMenu = control.subMenu;

                    foreach (var control2 in expressionsSubMenu.controls)
                    {
                        if (control2.name == "loli gimmik")
                        {
                            var expressionsSub2Menu = control2.subMenu;

                            foreach (var control3 in expressionsSub2Menu.controls)
                            {
                                if (control3.name is "mesugaki face")
                                {
                                    expressionsSub2Menu.controls.Remove(control3);
                                }
                                break;
                            }
                            foreach (var control3 in expressionsSub2Menu.controls)
                            {
                                if (control3.name is "mesugaki face particle")
                                {
                                    expressionsSub2Menu.controls.Remove(control3);
                                }
                                break;
                            }
                            control2.subMenu = expressionsSub2Menu;
                            break;
                        }
                    }
                    control.subMenu = expressionsSubMenu;
                    break;
                }
            }
            return this;
        }

        public IllRirikaParamMesugakiFace DestroyObj()
        {
            DestroyObj(descriptor.transform.Find("Advanced/mesugakiparticle"));
            return this;
        }
    }
}
#endif
