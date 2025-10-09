using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VRC.Dynamics;
using VRC.SDK3.Avatars.Components;
using VRC.SDK3.Avatars.ScriptableObjects;
using UnityEditor;

#if UNITY_EDITOR
using UnityEditor.Animations;

namespace jp.illusive_isc.RirikaOptimizer
{
    [AddComponentMenu("")]
    internal class IllRirikaParamCloth : IllRirikaParam
    {
        VRCAvatarDescriptor descriptor;
        AnimatorController animator;
        bool ClothFlg;
        bool ClothFlg1;
        bool ClothFlg2;
        bool ClothFlg3;
        bool ClothFlg4;
        bool ClothFlg5;
        bool ClothFlg6;
        bool ClothFlg7;
        bool ClothFlg8;
        bool ClothFlg9;
        bool ClothFlg10;
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
            AnimatorController animator,
            IllRirikaOptimizer optimizer
        )
        {
            this.descriptor = descriptor;
            this.animator = animator;
            ClothFlg = optimizer.ClothFlg;
            ClothFlg1 = optimizer.ClothFlg1;
            ClothFlg2 = optimizer.ClothFlg2;
            ClothFlg3 = optimizer.ClothFlg3;
            ClothFlg4 = optimizer.ClothFlg4;
            ClothFlg5 = optimizer.ClothFlg5;
            ClothFlg6 = optimizer.ClothFlg6;
            ClothFlg7 = optimizer.ClothFlg7;
            ClothFlg8 = optimizer.ClothFlg8;
            ClothFlg9 = optimizer.ClothFlg9;
            ClothFlg10 = optimizer.ClothFlg10;
            return this;
        }

        public void DeleteFxBT()
        {
            var targetLayer = animator.layers.FirstOrDefault(l => l.name == "MainCtrlTree");
            if (targetLayer == null)
                return;
            foreach (var state in targetLayer.stateMachine.states)
            {
                if (state.state.motion is not BlendTree rootTree)
                    continue;

                rootTree.children = rootTree
                    .children.Where(c => c.motion.name != "cloth1custom")
                    .ToArray();
            }

            return;
        }

        public void DeleteParam()
        {
            animator.parameters = animator
                .parameters.Where(parameter => !MenuParameters.Contains(parameter.name))
                .ToArray();
            return;
        }

        public void DeleteVRCExpressions(VRCExpressionsMenu menu, VRCExpressionParameters param)
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
            return;
        }

        public void ChangeObj()
        {
            if (ClothFlg1)
                DestroyObj(descriptor.transform.Find("Outer"));
            if (ClothFlg2)
            {
                DestroyObj(descriptor.transform.Find("Armature/Hips/Spine/Chest/Z_Bag_root"));
                DestroyObj(descriptor.transform.Find("Bag"));
            }
            if (ClothFlg3)
                if (descriptor.transform.Find("Cloth") is Transform Cloth)
                {
                    Cloth
                        .gameObject.GetComponent<SkinnedMeshRenderer>()
                        .SetBlendShapeWeight(2, 100);
                }
            if (ClothFlg4)
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
            if (ClothFlg8)
                DestroyObj(descriptor.transform.Find("Boots"));
            if (ClothFlg6)
            {
                DestroyObj(descriptor.transform.Find("Cloth"));
                if (ClothFlg1)
                    DestroyObj(descriptor.transform.Find("Armature/Hips/Spine/Z_Skirt_root"));
                DestroyObj(
                    descriptor.transform.Find("Armature/Hips/Spine/Chest/cloth1_chestribbon")
                );
                if (ClothFlg2)
                {
                    DestroyObj(
                        descriptor.transform.Find("Armature/Hips/Spine/Chest/sholder_L/Z_frills_L")
                    );
                    DestroyObj(
                        descriptor.transform.Find("Armature/Hips/Spine/Chest/sholder_R/Z_frills_R")
                    );
                }
                if (descriptor.transform.Find("body_b") is Transform body_b)
                {
                    body_b
                        .gameObject.GetComponent<SkinnedMeshRenderer>()
                        .SetBlendShapeWeight(34, 0);
                }
            }
            if (ClothFlg5)
                DestroyObj(descriptor.transform.Find("cover_arm"));
            if (ClothFlg7)
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
            if (ClothFlg10)
            {
                var prefab = AssetDatabase.LoadAssetAtPath<Object>(
                    AssetDatabase.GUIDToAssetPath("416063b3d4900a3468a716c27a8f6dee")
                );
                bool alreadyExists = false;
                GameObject instance = (GameObject)
                    PrefabUtility.InstantiatePrefab(prefab);
                foreach (Transform child in descriptor.transform)
                    if (child.name == prefab.name)
                    {
                        alreadyExists = true;
                        DestroyImmediate(instance);
                        instance = child.gameObject;
                        break;
                    }
                if (!alreadyExists)
                {
                    if (instance != null)
                    {
                        instance.transform.SetParent(descriptor.transform, false);

                        // Undo.RegisterCreatedObjectUndo(instance, "Instantiate Asset");
                    }
                }
            }
            if (!ClothFlg)
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
                    body_b
                        .gameObject.GetComponent<SkinnedMeshRenderer>()
                        .SetBlendShapeWeight(34, 0);
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
                DestroyObj(
                    descriptor.transform.Find("Armature/Hips/Spine/Chest/cloth1_chestribbon")
                );
                DestroyObj(descriptor.transform.Find("Armature/Hips/Spine/Chest/Z_Bag_root"));
                DestroyObj(
                    descriptor.transform.Find("Armature/Hips/Spine/Chest/sholder_L/Z_frills_L")
                );
                DestroyObj(
                    descriptor.transform.Find("Armature/Hips/Spine/Chest/sholder_R/Z_frills_R")
                );
                DestroyObj(descriptor.transform.Find("Advanced/Ground"));
                DestroyObj(descriptor.transform.Find("Tail"));
                if (ClothFlg9)
                    DestroyObj(descriptor.transform.Find("Bra"));
            }
        }
    }
}
#endif
