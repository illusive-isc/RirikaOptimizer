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
    [AddComponentMenu("")]
    internal class IllRirikaParamDef : IllRirikaParam
    {
        HashSet<string> paramList = new();
        VRCAvatarDescriptor descriptor;
        AnimatorController animator;

        private static readonly List<string> Layers = new() { };

        private static readonly List<string> NotSyncParameters = new()
        {
            "takasa",
            "takasa_Toggle",
            "Action_Mode_Reset",
            "Action_Mode",
            "Mirror",
            "paryi_change_Standing",
            "paryi_change_Crouching",
            "paryi_change_Prone",
            "paryi_floating",
            "paryi_change_all_reset",
            "paryi_change_Mirror_S",
            "paryi_change_Mirror_P",
            "paryi_change_Mirror_H",
            "paryi_change_Mirror_C",
            "paryi_chang_Loco",
            "paryi_Jump",
            "paryi_Jump_cancel",
            "paryi_change_Standing_M",
            "paryi_change_Crouching_M",
            "paryi_change_Prone_M",
            "paryi_floating_M",
            "leg fixed",
            "JumpCollider",
            "SpeedCollider",
            "clairvoyance",
            "TPS",
        };

        public IllRirikaParamDef Initialize(
            VRCAvatarDescriptor descriptor,
            AnimatorController animator
        )
        {
            this.descriptor = descriptor;
            this.animator = animator;
            return this;
        }

        public IllRirikaParamDef DeleteFx()
        {
            foreach (var layer in animator.layers)
            {
                if (layer.name == "MainCtrlTree")
                {
                    foreach (var state in layer.stateMachine.states)
                    {
                        if (state.state.name == "MainCtrlTree 0")
                        {
                            layer.stateMachine.RemoveState(state.state);
                            break;
                        }
                    }
                    foreach (var state in layer.stateMachine.states)
                    {
                        if (state.state.motion is BlendTree blendTree)
                        {
                            BlendTree newBlendTree = new()
                            {
                                name = "VRMode",
                                blendParameter = "VRMode",
                                blendParameterY = "Blend",
                                blendType = BlendTreeType.Simple1D,
                                useAutomaticThresholds = false,
                                maxThreshold = 1.0f,
                                minThreshold = 0.0f,
                            };
                            blendTree.AddChild(newBlendTree);
                            // BlendTreeの子モーションを取得
                            var children = blendTree.children;

                            // "LipSynk" のモーションがある場合、threshold を変更
                            for (int i = 0; i < children.Length; i++)
                            {
                                if (children[i].motion.name == "VRMode")
                                {
                                    children[i].threshold = 1;
                                }
                            }
                            // 修正した children 配列を再代入（これをしないと変更が反映されない）
                            blendTree.children = children;

                            newBlendTree.children = new ChildMotion[]
                            {
                                new()
                                {
                                    motion = AssetDatabase.LoadAssetAtPath<Motion>(
                                        AssetDatabase.GUIDToAssetPath(
                                            AssetDatabase.FindAssets("VRMode0")[0]
                                        )
                                    ),
                                    threshold = 0.0f,
                                    timeScale = 1,
                                },
                                new()
                                {
                                    motion = AssetDatabase.LoadAssetAtPath<Motion>(
                                        AssetDatabase.GUIDToAssetPath(
                                            AssetDatabase.FindAssets("VRMode1")[0]
                                        )
                                    ),
                                    threshold = 1.0f,
                                    timeScale = 1,
                                },
                            };
                            AssetDatabase.AddObjectToAsset(newBlendTree, animator);
                            AssetDatabase.SaveAssets();
                        }
                    }
                }
                if (layer.name == "MainCtrlTree")
                {
                    foreach (var state in layer.stateMachine.states)
                        if (
                            state.state.name == "MainCtrlTree"
                            && state.state.motion is BlendTree blendTree
                        )
                            foreach (var childBT1 in blendTree.children)
                                if (
                                    childBT1.motion is BlendTree nestedBlendTree1
                                    && childBT1.motion.name == "phone"
                                )
                                    foreach (var childBT2 in nestedBlendTree1.children)
                                        if (
                                            childBT2.motion is BlendTree nestedBlendTree2
                                            && childBT2.motion.name == "phone on"
                                        )
                                        {
                                            var child1 = nestedBlendTree2.children[0];

                                            if (child1.motion is AnimationClip originalClip1)
                                            {
                                                AnimationClip copiedClip1 = Instantiate(
                                                    originalClip1
                                                );
                                                copiedClip1.name = originalClip1.name;

                                                copiedClip1.SetCurve(
                                                    "Outer",
                                                    typeof(SkinnedMeshRenderer),
                                                    $"blendShape.camera on",
                                                    AnimationCurve.Constant(0, 0, 0)
                                                );
                                                AssetDatabase.AddObjectToAsset(
                                                    copiedClip1,
                                                    animator
                                                );
                                                child1.motion = copiedClip1;
                                            }
                                            var child2 = nestedBlendTree2.children[1];

                                            if (child2.motion is AnimationClip originalClip2)
                                            {
                                                AnimationClip copiedClip2 = Instantiate(
                                                    originalClip2
                                                );
                                                copiedClip2.name = originalClip2.name;

                                                copiedClip2.SetCurve(
                                                    "Outer",
                                                    typeof(SkinnedMeshRenderer),
                                                    $"blendShape.camera on",
                                                    AnimationCurve.Constant(0, 0, 100)
                                                );
                                                AssetDatabase.AddObjectToAsset(
                                                    copiedClip2,
                                                    animator
                                                );
                                                child2.motion = copiedClip2;
                                            }
                                            nestedBlendTree2.children = new ChildMotion[]
                                            {
                                                child1,
                                                child2,
                                            };
                                        }
                    AssetDatabase.SaveAssets();
                }
                if (layer.name == "RightHand")
                {
                    foreach (var sub in layer.stateMachine.stateMachines)
                    {
                        if (sub.stateMachine.name == "mesugaki face")
                            foreach (var state in sub.stateMachine.states)
                                if (state.state.name == "Fist")
                                    foreach (var t in state.state.transitions)
                                        if (t.destinationState.name == "idol")
                                        {
                                            var conditions = t.conditions.ToList();
                                            conditions.Add(
                                                new AnimatorCondition()
                                                {
                                                    parameter = "GestureRight",
                                                    mode = AnimatorConditionMode.NotEqual,
                                                    threshold = 1,
                                                }
                                            );
                                            conditions.Add(
                                                new AnimatorCondition()
                                                {
                                                    parameter = "FaceLock",
                                                    mode = AnimatorConditionMode.Less,
                                                    threshold = 0.5f,
                                                }
                                            );
                                            t.conditions = conditions.ToArray();
                                        }
                    }
                }
            }
            // "VRMode" パラメータが Float でない場合は再設定
            foreach (var p in animator.parameters.Where(p => p.name == "VRMode").ToList())
            {
                if (p.type != AnimatorControllerParameterType.Float)
                {
                    animator.RemoveParameter(p);
                    animator.AddParameter("VRMode", AnimatorControllerParameterType.Float);
                }
            }
            var removedLayers = animator
                .layers.Where(layer => Layers.Contains(layer.name))
                .ToList();

            animator.layers = animator
                .layers.Where(layer => !Layers.Contains(layer.name))
                .ToArray();

            foreach (var layer in removedLayers)
            {
                foreach (var state in layer.stateMachine.states)
                {
                    AddIfNotInParameters(
                        paramList,
                        exsistParams,
                        state.state.cycleOffsetParameter,
                        state.state.cycleOffsetParameterActive
                    );
                    AddIfNotInParameters(
                        paramList,
                        exsistParams,
                        state.state.timeParameter,
                        state.state.timeParameterActive
                    );
                    AddIfNotInParameters(
                        paramList,
                        exsistParams,
                        state.state.speedParameter,
                        state.state.speedParameterActive
                    );
                    AddIfNotInParameters(
                        paramList,
                        exsistParams,
                        state.state.mirrorParameter,
                        state.state.mirrorParameterActive
                    );

                    foreach (var transition in state.state.transitions)
                    {
                        foreach (var condition in transition.conditions)
                        {
                            AddIfNotInParameters(paramList, exsistParams, condition.parameter);
                        }
                    }
                }

                foreach (var transition in layer.stateMachine.anyStateTransitions)
                {
                    foreach (var condition in transition.conditions)
                    {
                        AddIfNotInParameters(paramList, exsistParams, condition.parameter);
                    }
                }
            }

            return this;
        }

        public IllRirikaParamDef DeleteFxBT()
        {
            foreach (var layer in animator.layers.Where(layer => layer.name == "MainCtrlTree"))
            {
                foreach (var state in layer.stateMachine.states)
                {
                    if (state.state.motion is BlendTree blendTree)
                    {

                        blendTree.children = blendTree
                            .children.Where(c => !(c.motion.name == "FaceLock"))
                            .ToArray();
                        blendTree.children = blendTree
                            .children.Where(c => !(c.motion.name == "Voice_bubbles"))
                            .ToArray();
                        blendTree.children = blendTree
                            .children.Where(c => !(c.motion.name == "Camera_eye_hide"))
                            .ToArray();
                    }
                }
            }
            return this;
        }

        public IllRirikaParamDef DeleteParam()
        {
            animator.parameters = animator
                .parameters.Where(parameter => !paramList.Contains(parameter.name))
                .ToArray();

            return this;
        }

        public IllRirikaParamDef DeleteVRCExpressions(
            VRCExpressionsMenu menu,
            VRCExpressionParameters param
        )
        {
            param.parameters = param
                .parameters.Where(parameter => !paramList.Contains(parameter.name))
                .ToArray();
            param.parameters = param
                .parameters.Where(parameter => !("Mirror Toggle" == parameter.name))
                .ToArray();
            foreach (var parameter in param.parameters)
            {
                if (NotSyncParameters.Contains(parameter.name))
                {
                    parameter.networkSynced = false;
                }
            }
            return this;
        }

        public IllRirikaParamDef DestroyObj()
        {
            DestroyObj(descriptor.transform.Find("Advanced/Particle/2"));
            DestroyObj(descriptor.transform.Find("Advanced/Particle/3"));
            DestroyObj(descriptor.transform.Find("Advanced/Particle/4"));
            DestroyObj(descriptor.transform.Find("Advanced/Particle/6"));
            DestroyObj(descriptor.transform.Find("Advanced/Particle/7/1"));
            DestroyObj(descriptor.transform.Find("Advanced/Particle/7/3"));
            DestroyObj(descriptor.transform.Find("Advanced/Particle/7/5"));
            DestroyObj(descriptor.transform.Find("Advanced/Gimmick1/8"));
            DestroyObj(descriptor.transform.Find("Advanced/Gimmick2/3"));
            DestroyObj(descriptor.transform.Find("Advanced/Gimmick2/5"));
            DestroyObj(descriptor.transform.Find("Advanced/Gimmick2/6"));
            DestroyObj(descriptor.transform.Find("Advanced/Gimmick2/7"));

            return this;
        }

        public IllRirikaParamDef ParticleOptimize()
        {
            SetMaxParticle("Advanced/Particle/1/breath", 100);
            SetMaxParticle("Advanced/Particle/5/8bitheart", 5);
            SetMaxParticle("Advanced/Particle/5/8bitheart/8bitheart flare", 15);

            SetMaxParticle("Advanced/HeartGunR/HeartGunFLY/HeartGunFLY2", 50);
            SetMaxParticle("Advanced/HeartGunR/HeartGunFLY/HeartGunFLY2/kira", 3200);
            SetMaxParticle("Advanced/HeartGunR/HeartGunFLY/HeartGunFLY2/Heart", 100);
            SetMaxParticle("Advanced/HeartGunR/HeartGunFLY/HeartGunFLY2/shot2", 400);
            SetMaxParticle("Advanced/HeartGunR/HeartGunFLY/HeartGunFLY2/shot2 (1)", 400);
            SetMaxParticle("Advanced/HeartGunR/HeartGunFLY/HeartGunChargeStay", 50);

            SetMaxParticle("Advanced/HeartGunR/HeartGunCollider/HeadHit/HeadHit", 50);
            SetMaxParticle("Advanced/HeartGunR/HeartGunCollider/HeadHit/Heart (1)", 50);

            SetMaxParticle("Advanced/HeartGunL/HeartGunFLY/HeartGunFLY2", 50);
            SetMaxParticle("Advanced/HeartGunL/HeartGunFLY/HeartGunFLY2/kira", 3200);
            SetMaxParticle("Advanced/HeartGunL/HeartGunFLY/HeartGunFLY2/Heart", 100);
            SetMaxParticle("Advanced/HeartGunL/HeartGunFLY/HeartGunFLY2/shot2", 400);
            SetMaxParticle("Advanced/HeartGunL/HeartGunFLY/HeartGunFLY2/shot2 (1)", 400);
            SetMaxParticle("Advanced/HeartGunL/HeartGunFLY/HeartGunChargeStay", 50);
            SetMaxParticle("Advanced/HeartGunL/HeartGunCollider/HeadHit/HeadHit", 50);
            SetMaxParticle("Advanced/HeartGunL/HeartGunCollider/HeadHit/Heart (1)", 50);
            SetMaxParticle("Advanced/HeartGunR2/HeartGunFLY/HeartGunFLY2", 50);
            SetMaxParticle("Advanced/HeartGunR2/HeartGunFLY/HeartGunFLY2/kira", 3200);
            SetMaxParticle("Advanced/HeartGunR2/HeartGunFLY/HeartGunFLY2/Heart", 100);
            SetMaxParticle("Advanced/HeartGunR2/HeartGunFLY/HeartGunFLY2/shot2", 400);
            SetMaxParticle("Advanced/HeartGunR2/HeartGunFLY/HeartGunFLY2/shot2 (1)", 400);
            SetMaxParticle("Advanced/HeartGunR2/HeartGunFLY/HeartGunChargeStay", 50);
            SetMaxParticle("Advanced/HeartGunL2/HeartGunFLY/HeartGunFLY2", 50);
            SetMaxParticle("Advanced/HeartGunL2/HeartGunFLY/HeartGunFLY2/kira", 3200);
            SetMaxParticle("Advanced/HeartGunL2/HeartGunFLY/HeartGunFLY2/Heart", 100);
            SetMaxParticle("Advanced/HeartGunL2/HeartGunFLY/HeartGunFLY2/shot2", 1200);
            SetMaxParticle("Advanced/HeartGunL2/HeartGunFLY/HeartGunFLY2/shot2 (1)", 1200);
            SetMaxParticle("Advanced/HeartGunL2/HeartGunFLY/HeartGunChargeStay", 50);

            SetMaxParticle("Advanced/Particle/7/2/pen1_R/PenParticle", 10000);
            SetMaxParticle("Advanced/Particle/7/2/pen1_R/PenParticle/SubEmitter0", 5000);
            SetMaxParticle("Advanced/Particle/7/4/pen1_L/PenParticle", 10000);
            SetMaxParticle("Advanced/Particle/7/4/pen1_L/PenParticle/SubEmitter0", 5000);

            return this;
        }

        private void SetMaxParticle(string path, int max)
        {
            var particleobj = descriptor.transform.Find(path);
            if (particleobj)
            {
                var mainModule = particleobj.GetComponent<ParticleSystem>().main;
                mainModule.maxParticles = max;
            }
        }
    }
}
#endif
