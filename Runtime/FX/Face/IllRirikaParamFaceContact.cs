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
    internal class IllRirikaParamFaceContact : IllRirikaParam
    {
        VRCAvatarDescriptor descriptor;
        AnimatorController animator;
        HashSet<string> paramList = new();
        public bool kamitukiFlg = false;
        public bool nadeFlg = false;
        public bool blinkFlg = false;
        private static readonly List<string> MenuParameters = new() { "Nade", "Kamituki" };

        public IllRirikaParamFaceContact Initialize(
            VRCAvatarDescriptor descriptor,
            AnimatorController animator,

            IllRirikaOptimizer optimizer
        )
        {
            this.descriptor = descriptor;
            this.animator = animator;
            kamitukiFlg = optimizer.kamitukiFlg;
            nadeFlg = optimizer.nadeFlg;
            blinkFlg = optimizer.blinkFlg;
            return this;
        }

        public IllRirikaParamFaceContact DeleteParam()
        {
            animator.parameters = animator
                .parameters.Where(parameter => !MenuParameters.Contains(parameter.name))
                .ToArray();
            return this;
        }

        public IllRirikaParamFaceContact DeleteFxBT()
        {
            var targetLayer = animator.layers.FirstOrDefault(l => l.name == "MainCtrlTree");
            if (targetLayer == null)
                return this;

            foreach (var state in targetLayer.stateMachine.states)
            {
                if (state.state.motion is not BlendTree rootTree)
                    continue;

                List<string> MenuParameters = new() { };
                if (nadeFlg)
                    MenuParameters.Add("Nade");
                if (kamitukiFlg)
                    MenuParameters.Add("Kamituki");
                rootTree.children = rootTree
                    .children.Where(c => CheckBT(c.motion, MenuParameters))
                    .ToArray();
            }

            return this;
        }

        public IllRirikaParamFaceContact DeleteVRCExpressions(
            VRCExpressionsMenu menu,
            VRCExpressionParameters param
        )
        {
            foreach (var parameter in param.parameters)
            {
                if (parameter.name is "Nade" or "Kamituki" or "Blink off")
                {
                    parameter.defaultValue = 1;
                    parameter.networkSynced = false;
                }
            }

            foreach (var control in menu.controls)
            {
                if (control.name == "Gimmick")
                {
                    var expressionsSubMenu = control.subMenu;

                    foreach (var control2 in expressionsSubMenu.controls)
                    {
                        if (control2.name == "Face")
                        {
                            var expressionsSub2Menu = control2.subMenu;
                            if (kamitukiFlg)
                                foreach (var control3 in expressionsSub2Menu.controls)
                                {
                                    if (control3.name is "噛みつき禁止")
                                    {
                                        expressionsSub2Menu.controls.Remove(control3);
                                        break;
                                    }
                                }
                            if (nadeFlg)
                                foreach (var control3 in expressionsSub2Menu.controls)
                                {
                                    if (control3.name is "なでなで")
                                    {
                                        expressionsSub2Menu.controls.Remove(control3);
                                        break;
                                    }
                                }
                            if (blinkFlg)
                                foreach (var control3 in expressionsSub2Menu.controls)
                                {
                                    if (control3.name is "Blink off")
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
    }
}
#endif
