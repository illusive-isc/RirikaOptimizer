using System.Collections.Generic;
using System.Data;
using System.Linq;
using UnityEngine;
using VRC.SDK3.Avatars.Components;
using VRC.SDK3.Avatars.ScriptableObjects;
#if UNITY_EDITOR
using UnityEditor.Animations;

namespace jp.illusive_isc.RirikaOptimizer
{
    [AddComponentMenu("")]
    internal class IllRirikaParamAccessory : IllRirikaParam
    {
        VRCAvatarDescriptor descriptor;
        AnimatorController animator;
        private static readonly List<string> MenuParameters = new()
        {
            "cloth1",
            "cloth7",
            "cloth8",
            "cloth9",
        };

        public IllRirikaParamAccessory Initialize(
            VRCAvatarDescriptor descriptor,
            AnimatorController animator
        )
        {
            this.descriptor = descriptor;
            this.animator = animator;
            return this;
        }

        public IllRirikaParamAccessory DeleteFxBT()
        {
            var targetLayer = animator.layers.FirstOrDefault(l => l.name == "MainCtrlTree");
            if (targetLayer == null)
                return this;

            foreach (var state in targetLayer.stateMachine.states)
            {
                if (state.state.motion is not BlendTree rootTree)
                    continue;
                rootTree.children = rootTree
                    .children.Where(c => CheckBT(c.motion, MenuParameters))
                    .ToArray();
                var RirikaClosetTree = rootTree
                    .children.Select(c => c.motion)
                    .OfType<BlendTree>()
                    .FirstOrDefault(bt => bt.name == "cloth1custom");
                if (RirikaClosetTree != null)
                    RirikaClosetTree.children = RirikaClosetTree
                        .children.Where(c => CheckBT(c.motion, MenuParameters))
                        .ToArray();
            }

            return this;
        }

        public IllRirikaParamAccessory DeleteParam()
        {
            animator.parameters = animator
                .parameters.Where(parameter => !MenuParameters.Contains(parameter.name))
                .ToArray();
            return this;
        }

        public IllRirikaParamAccessory DeleteVRCExpressions(
            VRCExpressionsMenu menu,
            VRCExpressionParameters param
        )
        {
            param.parameters = param
                .parameters.Where(parameter => !MenuParameters.Contains(parameter.name))
                .ToArray();

            foreach (var control1 in menu.controls)
            {
                if (control1.name == "closet")
                {
                    var expressionsSubMenu1 = control1.subMenu;

                    foreach (var control2 in expressionsSubMenu1.controls)
                    {
                        if (control2.name == "acce")
                        {
                            expressionsSubMenu1.controls.Remove(control2);
                            break;
                        }
                    }
                    control1.subMenu = expressionsSubMenu1;
                    break;
                }
            }
            return this;
        }

        public IllRirikaParamAccessory DestroyObj(
            bool accessoryFlg1,
            bool accessoryFlg2,
            bool accessoryFlg3,
            bool accessoryFlg4
        )
        {
            // DestroyObj(descriptor.transform.Find("Armature/Hips/Spine/Chest/Neck/Head/acce_wing_transform"));
            // DestroyObj(descriptor.transform.Find("Armature/Hips/Spine/Chest/Neck/Head/earring_root"));
            if (descriptor.transform.Find("cloth_Accessories") is Transform accessoryObj1)
            {
                accessoryObj1
                    .gameObject.GetComponent<SkinnedMeshRenderer>()
                    .SetBlendShapeWeight(3, accessoryFlg1 ? 0 : 100);
                accessoryObj1
                    .gameObject.GetComponent<SkinnedMeshRenderer>()
                    .SetBlendShapeWeight(4, accessoryFlg1 ? 0 : 100);
            }
            if (descriptor.transform.Find("cloth_Accessories") is Transform accessoryObj2)
                accessoryObj2
                    .gameObject.GetComponent<SkinnedMeshRenderer>()
                    .SetBlendShapeWeight(1, accessoryFlg2 ? 0 : 100);
            if (descriptor.transform.Find("cloth_Accessories") is Transform accessoryObj3)
                accessoryObj3
                    .gameObject.GetComponent<SkinnedMeshRenderer>()
                    .SetBlendShapeWeight(6, accessoryFlg3 ? 0 : 100);
            if (descriptor.transform.Find("cloth_Accessories") is Transform accessoryObj7)
                accessoryObj7
                    .gameObject.GetComponent<SkinnedMeshRenderer>()
                    .SetBlendShapeWeight(7, accessoryFlg4 ? 0 : 100);
            return this;
        }
    }
}
#endif
