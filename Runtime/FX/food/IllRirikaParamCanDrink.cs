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
    internal class IllRirikaParamCanDrink : IllRirikaParam
    {
        VRCAvatarDescriptor descriptor;
        AnimatorController animator;

        private static readonly List<string> MenuParameters = new() { "drink" };

        public IllRirikaParamCanDrink Initialize(
            VRCAvatarDescriptor descriptor,
            AnimatorController animator
        )
        {
            this.descriptor = descriptor;
            this.animator = animator;
            return this;
        }

        public IllRirikaParamCanDrink DeleteParam()
        {
            animator.parameters = animator
                .parameters.Where(parameter => !MenuParameters.Contains(parameter.name))
                .ToArray();
            return this;
        }

        public IllRirikaParamCanDrink DeleteFxBT()
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

        public IllRirikaParamCanDrink DeleteVRCExpressions(
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
                        if (control2.name == "food")
                        {
                            var expressionsSub2Menu = control2.subMenu;

                            foreach (var control3 in expressionsSub2Menu.controls)
                            {
                                if (control3.name is "ジュース")
                                {
                                    expressionsSub2Menu.controls.Remove(control3);
                                    break;
                                }
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

        public IllRirikaParamCanDrink DestroyObj()
        {
            DestroyObj(descriptor.transform.Find("Advanced/food/can drink Hand"));
            return this;
        }
    }
}
#endif
