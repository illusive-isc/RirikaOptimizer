using System.Collections.Generic;
using System.Linq;
using VRC.Dynamics;
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
        bool accessoryFlg;
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
            bool accessoryFlg
        )
        {
            this.descriptor = descriptor;
            this.animator = animator;
            this.accessoryFlg = accessoryFlg;
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

        public IllRirikaParamHair DestroyObj()
        {
            DestroyObj(descriptor.transform.Find("hair_back_long"));
            DestroyObj(descriptor.transform.Find("hair_bob"));
            DestroyObj(descriptor.transform.Find("hair_main"));
            DestroyObj(descriptor.transform.Find("Advanced/Hair rotation"));
            if (accessoryFlg)
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
            return this;
        }
    }
}
#endif
