using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VRC.Dynamics;
using VRC.SDK3.Avatars.Components;
using VRC.SDK3.Avatars.ScriptableObjects;
#if UNITY_EDITOR
using UnityEditor.Animations;

namespace jp.illusive_isc.RirikaOptimizer
{
    [AddComponentMenu("")]
    internal class IllRirikaParamCloth : IllRirikaParam
    {
        VRCAvatarDescriptor descriptor;
        AnimatorController animator;
        private static readonly List<string> MenuParameters = new()
        {
            "Ririka_Outer",
            "Ririka_Tsyatu",
            "Ririka_armcover",
            "Ririka_gloves",
            "Ririka_Pants",
            "Ririka_boots",
        };

        public IllRirikaParamCloth Initialize(
            VRCAvatarDescriptor descriptor,
            AnimatorController animator
        )
        {
            this.descriptor = descriptor;
            this.animator = animator;
            return this;
        }

        public IllRirikaParamCloth DeleteFxBT()
        {
            var targetLayer = animator.layers.FirstOrDefault(l => l.name == "MainCtrlTree");
            if (targetLayer == null)
                return this;
            foreach (var state in targetLayer.stateMachine.states)
            {
                if (state.state.motion is not BlendTree rootTree)
                    continue;

                rootTree.children = rootTree
                    .children.Where(c => c.motion.name != "cloth1custom")
                    .ToArray();
            }

            return this;
        }

        public IllRirikaParamCloth DeleteParam()
        {
            animator.parameters = animator
                .parameters.Where(parameter => !MenuParameters.Contains(parameter.name))
                .ToArray();
            return this;
        }

        public IllRirikaParamCloth DeleteVRCExpressions(
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
                        if (control2.name == "cloth")
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

        public IllRirikaParamCloth DestroyObjects(bool clothFlg9)
        {
            DestroyObj(descriptor.transform.Find("Bag"));
            DestroyObj(descriptor.transform.Find("Boots"));
            DestroyObj(descriptor.transform.Find("Cloth"));
            DestroyObj(descriptor.transform.Find("cover_arm"));
            DestroyObj(descriptor.transform.Find("Outer"));
            DestroyObj(descriptor.transform.Find("Over knee socks"));
            DestroyObj(descriptor.transform.Find("Tail"));

            if (descriptor.transform.Find("body_b") is Transform body_b)
            {
                body_b.gameObject.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(34, 0);
                body_b.gameObject.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(39, 0);
                body_b.gameObject.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(40, 0);
                body_b.gameObject.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(108, 0);
                body_b.gameObject.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(109, 0);
            }

            foreach (var layer in animator.layers.Where(layer => layer.name == "MainCtrlTree"))
            {
                foreach (var state in layer.stateMachine.states)
                {
                    if (state.state.motion is BlendTree blendTree)
                    {
                        blendTree.children = blendTree
                            .children.Where(c => !(c.motion.name == "Grounded"))
                            .ToArray();
                    }
                }
            }
            DestroyObj(descriptor.transform.Find("Armature/Hips/tail"));
            DestroyObj(descriptor.transform.Find("Armature/Hips/Spine/Z_Skirt_root"));
            DestroyObj(descriptor.transform.Find("Armature/Hips/Spine/Chest/cloth1_chestribbon"));
            DestroyObj(descriptor.transform.Find("Armature/Hips/Spine/Chest/Z_Bag_root"));
            DestroyObj(descriptor.transform.Find("Armature/Hips/Spine/Chest/sholder_L/Z_frills_L"));
            DestroyObj(descriptor.transform.Find("Armature/Hips/Spine/Chest/sholder_R/Z_frills_R"));
            // DestroyObj(descriptor.transform.Find("Armature/tail/tail.001/tail.002/tail.003"));
            DestroyObj(descriptor.transform.Find("Advanced/Ground"));
            DestroyObj(descriptor.transform.Find("Tail"));
            if (clothFlg9)
                DestroyObj(descriptor.transform.Find("Bra"));
            return this;
        }

        internal void ChangeObj(
            bool clothFlg1,
            bool clothFlg2,
            bool clothFlg3,
            bool clothFlg4,
            bool clothFlg5,
            bool clothFlg6,
            bool clothFlg7,
            bool clothFlg8
        )
        {
            if (clothFlg1)
                DestroyObj(descriptor.transform.Find("Outer"));
            if (clothFlg2)
            {
                DestroyObj(descriptor.transform.Find("Armature/Hips/Spine/Chest/Z_Bag_root"));
                DestroyObj(descriptor.transform.Find("Bag"));
            }
            if (clothFlg3)
                if (descriptor.transform.Find("Cloth") is Transform Cloth)
                {
                    Cloth
                        .gameObject.GetComponent<SkinnedMeshRenderer>()
                        .SetBlendShapeWeight(2, 100);
                }
            if (clothFlg4)
            {
                DestroyObj(descriptor.transform.Find("Tail"));
                DestroyObj(descriptor.transform.Find("Advanced/Ground"));
                DestroyObj(
                    descriptor.transform.Find("Armature/Hips/tail/tail.001/tail.002/tail.003")
                );
                if (descriptor.transform.Find("Armature/tail/tail.001") is Transform Tail)
                {
                    Tail.gameObject.GetComponent<VRCPhysBoneBase>().enabled = false;
                }
            }
            if (clothFlg8)
                DestroyObj(descriptor.transform.Find("Boots"));
            if (clothFlg6)
            {
                DestroyObj(descriptor.transform.Find("Cloth"));
                DestroyObj(descriptor.transform.Find("Armature/Hips/Spine/Z_Skirt_root"));
                DestroyObj(
                    descriptor.transform.Find("Armature/Hips/Spine/Chest/cloth1_chestribbon")
                );
                DestroyObj(
                    descriptor.transform.Find("Armature/Hips/Spine/Chest/sholder_L/Z_frills_L")
                );
                DestroyObj(
                    descriptor.transform.Find("Armature/Hips/Spine/Chest/sholder_R/Z_frills_R")
                );
                if (descriptor.transform.Find("body_b") is Transform body_b)
                {
                    body_b
                        .gameObject.GetComponent<SkinnedMeshRenderer>()
                        .SetBlendShapeWeight(34, 0);
                }
            }
            if (clothFlg5)
                DestroyObj(descriptor.transform.Find("cover_arm"));
            if (clothFlg7)
            {
                DestroyObj(descriptor.transform.Find("Over knee socks"));
                if (descriptor.transform.Find("body_b") is Transform body_b)
                {
                    body_b
                        .gameObject.GetComponent<SkinnedMeshRenderer>()
                        .SetBlendShapeWeight(39, 0);
                    body_b
                        .gameObject.GetComponent<SkinnedMeshRenderer>()
                        .SetBlendShapeWeight(40, 0);
                    body_b
                        .gameObject.GetComponent<SkinnedMeshRenderer>()
                        .SetBlendShapeWeight(108, 0);
                    body_b
                        .gameObject.GetComponent<SkinnedMeshRenderer>()
                        .SetBlendShapeWeight(109, 0);
                }
            }

            foreach (var layer in animator.layers.Where(layer => layer.name == "MainCtrlTree"))
            {
                foreach (var state in layer.stateMachine.states)
                {
                    if (state.state.motion is BlendTree blendTree)
                    {
                        blendTree.children = blendTree
                            .children.Where(c => !(c.motion.name == "Grounded"))
                            .ToArray();
                    }
                }
            }
        }
    }
}
#endif
