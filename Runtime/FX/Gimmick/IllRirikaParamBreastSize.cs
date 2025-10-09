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
    internal class IllRirikaParamBreastSize : IllRirikaParam
    {
        VRCAvatarDescriptor descriptor;
        AnimatorController animator;
        bool BreastSizeFlg2;

        private static readonly List<string> MenuParameters = new() { "BreastSize" };

        public IllRirikaParamBreastSize Initialize(
            VRCAvatarDescriptor descriptor,
            AnimatorController animator,
            IllRirikaOptimizer optimizer

        )
        {
            this.descriptor = descriptor;
            this.animator = animator;
            BreastSizeFlg2 = optimizer.BreastSizeFlg2;
            return this;
        }

        public IllRirikaParamBreastSize DeleteParam()
        {
            animator.parameters = animator
                .parameters.Where(parameter => !MenuParameters.Contains(parameter.name))
                .ToArray();
            return this;
        }

        public IllRirikaParamBreastSize DeleteFxBT()
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

        public IllRirikaParamBreastSize DeleteVRCExpressions(
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
                        if (control2.name == "Breast_size")
                        {
                            expressionsSubMenu.controls.Remove(control2);
                            break;
                        }
                    }
                    control.subMenu = expressionsSubMenu;
                    break;
                }
            }
            return this;
        }

        public IllRirikaParamBreastSize ChangeObj()
        {
            if (descriptor.transform.Find("Bra") is Transform BraObj)
            {
                BraObj
                    .gameObject.GetComponent<SkinnedMeshRenderer>()
                    .SetBlendShapeWeight(2, BreastSizeFlg2 ? 0 : 2.7f);
            }
            if (descriptor.transform.Find("body_b") is Transform body_b)
            {
                body_b
                    .gameObject.GetComponent<SkinnedMeshRenderer>()
                    .SetBlendShapeWeight(10, BreastSizeFlg2 ? 100 : 0);
            }
            if (descriptor.transform.Find("Outer") is Transform OuterObj)
            {
                OuterObj
                    .gameObject.GetComponent<SkinnedMeshRenderer>()
                    .SetBlendShapeWeight(0, BreastSizeFlg2 ? 100 : 6.3f);
            }
            Dictionary<string, int> dict = new() { { "Bra", 3 }, { "Cloth", 1 } };

            foreach (var item in dict)
            {
                if (descriptor.transform.Find(item.Key) is Transform itemObj)
                {
                    itemObj
                        .gameObject.GetComponent<SkinnedMeshRenderer>()
                        .SetBlendShapeWeight(item.Value, BreastSizeFlg2 ? 100 : 0);
                }
            }
            return this;
        }
    }
}
#endif
