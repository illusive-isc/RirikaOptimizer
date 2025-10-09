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
    internal class IllRirikaParamPet : IllRirikaParam
    {
        VRCAvatarDescriptor descriptor;
        AnimatorController animator;
        private static readonly List<string> MenuParameters = new()
        {
            "pet",
            "pet position X",
            "pet position Y",
            "pet position custom",
            "pet to pet contact",
            "pet_stand_position_look",
            "Pet Grab_IsGrabbed",
            "pet stand head",
            "pet stand sholder_L",
            "pet stand sholder_R",
            "Head_search",
            "Head_search_X+",
            "Head_search_X-",
            "Head_search_Y+",
            "Head_search_Y-",
            "Head_search_Z+",
            "Head_search_Z-",
            "pet frend on",
            "pet player dis",
        };

        private static readonly List<string> Layers = new() { "Pet", "Pet Animation" };

        public IllRirikaParamPet Initialize(
            VRCAvatarDescriptor descriptor,
            AnimatorController animator
        )
        {
            this.descriptor = descriptor;
            this.animator = animator;
            return this;
        }

        public IllRirikaParamPet DeleteFx()
        {
            animator.layers = animator
                .layers.Where(layer => !Layers.Contains(layer.name))
                .ToArray();

            return this;
        }

        public IllRirikaParamPet DeleteFxBT()
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

                rootTree.children = rootTree
                    .children.Where(c => !(c.motion.name == "pet position custom"))
                    .ToArray();
            }

            return this;
        }

        public IllRirikaParamPet DeleteParam()
        {
            animator.parameters = animator
                .parameters.Where(parameter => !MenuParameters.Contains(parameter.name))
                .ToArray();
            return this;
        }

        public IllRirikaParamPet DeleteVRCExpressions(
            VRCExpressionsMenu menu,
            VRCExpressionParameters param
        )
        {
            param.parameters = param
                .parameters.Where(parameter => !MenuParameters.Contains(parameter.name))
                .ToArray();
            foreach (var control1 in menu.controls)
            {
                if (control1.name == "Gimmick")
                {
                    var expressionsSubMenu1 = control1.subMenu;

                    foreach (var control2 in expressionsSubMenu1.controls)
                    {
                        if (control2.name == "Pet")
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

        public IllRirikaParamPet ChangeObj()
        {
            DestroyObj(descriptor.transform.Find("Advanced/Pet"));
            DestroyObj(descriptor.transform.Find("Advanced/pet position Particle"));

            return this;
        }
    }
}
#endif
