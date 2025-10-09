using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using VRC.SDK3.Avatars.Components;
using VRC.SDK3.Avatars.ScriptableObjects;
#if UNITY_EDITOR
using UnityEditor.Animations;

namespace jp.illusive_isc.RirikaOptimizer
{
    internal class IllRirikaParamHair : IllRirikaParam
    {
        VRCAvatarDescriptor descriptor;
        AnimatorController animator;
        bool AccessoryFlg1;
        bool HairFlg1;
        bool HairFlg2;
        bool HairFlg3;
        bool HairFlg4;
        bool HairFlg5;
        bool HairFlg6;
        bool HairFlg7;
        bool HairFlg8;
        bool HairFlg;
        bool colorFlg0;
        private static readonly List<string> MenuParameters = new()
        {
            "Object1",
            "Object8",
            "Object3",
            "Object2",
            "Object4",
            "Object5",
            "Object7",
        };
        private static readonly List<string> Layers = new() { "hair ctrl" };

        public IllRirikaParamHair Initialize(
            VRCAvatarDescriptor descriptor,
            AnimatorController animator,
            IllRirikaOptimizer optimizer
        )
        {
            this.descriptor = descriptor;
            this.animator = animator;
            AccessoryFlg1 = optimizer.AccessoryFlg1;
            HairFlg1 = optimizer.HairFlg1;
            HairFlg2 = optimizer.HairFlg2;
            HairFlg3 = optimizer.HairFlg3;
            HairFlg4 = optimizer.HairFlg4;
            HairFlg5 = optimizer.HairFlg5;
            HairFlg6 = optimizer.HairFlg6;
            HairFlg7 = optimizer.HairFlg7;
            HairFlg8 = optimizer.HairFlg8;
            HairFlg = optimizer.HairFlg;
            colorFlg0 = optimizer.colorFlg0;
            return this;
        }

        public IllRirikaParamHair DeleteFx()
        {
            animator.layers = animator
                .layers.Where(layer => !Layers.Contains(layer.name))
                .ToArray();

            return this;
        }

        public IllRirikaParamHair DeleteFxBT()
        {
            foreach (var layer in animator.layers.Where(layer => layer.name == "MainCtrlTree"))
            {
                foreach (var state in layer.stateMachine.states)
                {
                    if (state.state.motion is BlendTree blendTree)
                    {
                        blendTree.children = blendTree
                            .children.Where(c => c.motion.name != "Object")
                            .ToArray();
                    }
                }
            }

            return this;
        }

        public IllRirikaParamHair DeleteParam()
        {
            animator.parameters = animator
                .parameters.Where(parameter => !MenuParameters.Contains(parameter.name))
                .ToArray();
            return this;
        }

        public IllRirikaParamHair DeleteVRCExpressions(
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
                        if (control2.name == "Hair")
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

        public void ChangeObj()
        {
            if (descriptor.transform.Find("hair_main") is Transform hair_main)
            {
                hair_main
                    .gameObject.GetComponent<SkinnedMeshRenderer>()
                    .SetBlendShapeWeight(4, HairFlg1 ? 100 : 0);
                hair_main
                    .gameObject.GetComponent<SkinnedMeshRenderer>()
                    .SetBlendShapeWeight(6, HairFlg2 ? 0 : 100);
                hair_main
                    .gameObject.GetComponent<SkinnedMeshRenderer>()
                    .SetBlendShapeWeight(15, HairFlg7 ? 100 : 0);
            }
            if (descriptor.transform.Find("hair_bob") is Transform hair_bob)
            {
                hair_bob.gameObject.SetActive(HairFlg5);
                hair_bob
                    .gameObject.GetComponent<SkinnedMeshRenderer>()
                    .SetBlendShapeWeight(0, HairFlg3 ? 0 : 100);
                hair_bob
                    .gameObject.GetComponent<SkinnedMeshRenderer>()
                    .SetBlendShapeWeight(1, HairFlg6 ? 100 : 0);
            }
            if (descriptor.transform.Find("hair_back_long") is Transform hair_back_long)
            {
                hair_back_long.gameObject.SetActive(HairFlg4);
                hair_back_long
                    .gameObject.GetComponent<SkinnedMeshRenderer>()
                    .SetBlendShapeWeight(0, HairFlg3 ? 0 : 100);
            }
            if (descriptor.transform.Find("cloth_Accessories") is Transform cloth_Accessories)
            {
                if (AccessoryFlg1)
                {
                    cloth_Accessories
                        .gameObject.GetComponent<SkinnedMeshRenderer>()
                        .SetBlendShapeWeight(3, HairFlg2 ? 0 : 100);
                    cloth_Accessories
                        .gameObject.GetComponent<SkinnedMeshRenderer>()
                        .SetBlendShapeWeight(4, HairFlg2 ? 0 : 100);
                }
                cloth_Accessories
                    .gameObject.GetComponent<SkinnedMeshRenderer>()
                    .SetBlendShapeWeight(2, HairFlg7 || HairFlg8 || HairFlg ? 100 : 0);
            }

            if (!HairFlg && HairFlg8)
            {
                var prefab = AssetDatabase.LoadAssetAtPath<Object>(
                    AssetDatabase.GUIDToAssetPath("b0fb802c479d39448bd81534f28ae96c")
                );
                bool alreadyExists = false;
                GameObject instance = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
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
                var twin = instance.transform.Find("Backhair_twin.003");
                var renderer = twin.GetComponent<SkinnedMeshRenderer>();

                var materials = renderer.sharedMaterials;

                materials[0] = colorFlg0
                    ? AssetDatabase.LoadAssetAtPath<Material>(
                        AssetDatabase.GUIDToAssetPath("98b8fd92f51ca3643bf67a12ed2c7333")
                    )
                    : AssetDatabase.LoadAssetAtPath<Material>(
                        AssetDatabase.GUIDToAssetPath("47af70148badc2b4bb17d0632669cd67")
                    );

                renderer.sharedMaterials = materials;
            }
            if (!HairFlg)
                return;
            DestroyObj(descriptor.transform.Find("hair_back_long"));
            DestroyObj(descriptor.transform.Find("hair_bob"));
            DestroyObj(descriptor.transform.Find("hair_main"));
            DestroyObj(descriptor.transform.Find("Advanced/Hair rotation"));
            if (AccessoryFlg1)
                DestroyObj(
                    descriptor.transform.Find("Armature/Hips/Spine/Chest/Neck/Head/Hair_root")
                );
            else
            {
                DestroyObj(
                    descriptor.transform.Find(
                        "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/bob_root"
                    )
                );
                DestroyObj(
                    descriptor.transform.Find(
                        "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/front_root"
                    )
                );
                DestroyObj(
                    descriptor.transform.Find(
                        "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/long_root"
                    )
                );
                DestroyObj(
                    descriptor.transform.Find(
                        "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/twintail_root"
                    )
                );
                DestroyObj(
                    descriptor.transform.Find(
                        "Armature/Hips/Spine/Chest/Neck/Head/Hair_root/side_hair_R/"
                    )
                );
            }
        }
    }
}
#endif
