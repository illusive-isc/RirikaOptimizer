using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using VRC.Dynamics;
using VRC.SDK3.Avatars.Components;
using VRC.SDK3.Avatars.ScriptableObjects;
using VRC.SDKBase;
#if UNITY_EDITOR
using UnityEditor.Animations;

namespace jp.illusive_isc.RirikaOptimizer
{
    [AddComponentMenu("RirikaOptimizer")]
    public class IllRirikaOptimizer : MonoBehaviour, IEditorOnly
    {
        string pathDirPrefix = "Assets/RirikaOptimizer/";
        string pathDirSuffix = "/FX/";
        string pathName = "paryi_FX.controller";

        public bool colorFlg0 = false;

        public bool colorFlg1 = false;

        public bool colorFlg2 = false;

        [SerializeField]
        private bool heelFlg1 = false;

        [SerializeField]
        private bool heelFlg2 = false;

        public bool ClothFlg = false;

        public bool ClothFlg0 = false;

        public bool ClothFlg1 = false;

        public bool ClothFlg2 = false;

        public bool ClothFlg3 = false;

        public bool ClothFlg4 = false;

        public bool ClothFlg5 = false;

        public bool ClothFlg6 = false;

        public bool ClothFlg7 = false;

        public bool ClothFlg8 = false;

        public bool ClothFlg9 = false;

        public bool ClothFlg10 = false;

        [SerializeField]
        private bool AccessoryFlg0 = false;

        public bool AccessoryFlg1 = false;

        public bool AccessoryFlg2 = false;

        public bool AccessoryFlg3 = false;

        public bool AccessoryFlg4 = false;

        [SerializeField]
        private bool HairFlg0 = false;

        public bool HairFlg1 = false;

        public bool HairFlg2 = false;

        public bool HairFlg3 = false;

        public bool HairFlg4 = false;

        public bool HairFlg5 = false;

        public bool HairFlg6 = false;

        public bool HairFlg7 = false;

        public bool HairFlg8 = false;

        public bool HairFlg = false;

        [SerializeField]
        private float petScale = 1.0f;

        [SerializeField]
        private bool petFlg = false;

        [SerializeField]
        private bool TPSFlg = false;

        [SerializeField]
        private bool ClairvoyanceFlg = false;

        [SerializeField]
        private bool phoneFlg = false;

        [SerializeField]
        private bool phoneFlg1 = false;

        [SerializeField]
        private bool colliderJumpFlg = false;

        [SerializeField]
        private bool teppekiFlg = false;

        [SerializeField]
        private bool handHeartFlg = false;

        [SerializeField]
        private bool noisepanelFlg = false;

        [SerializeField]
        private bool neonFlg = false;

        [SerializeField]
        private bool mesugakiFaceFlg = false;

        [SerializeField]
        private bool mesugakiFaceFlg1 = false;

        [SerializeField]
        private bool BreastSizeFlg = false;

        public bool BreastSizeFlg2 = false;

        [SerializeField]
        private bool WhiteBreathFlg = false;

        [SerializeField]
        private bool eightBitFlg = false;

        [SerializeField]
        private bool PenCtrlFlg = false;

        public bool HeartGunFlg = false;

        public bool FaceGestureFlg = false;

        public bool FaceLockFlg = false;

        public bool FaceValFlg = false;

        public bool blinkFlg = false;

        public bool kamitukiFlg = false;

        public bool nadeFlg = false;

        [SerializeField]
        private bool candyFlg = false;

        [SerializeField]
        private bool doughnutFlg = false;

        [SerializeField]
        private bool drinkFlg = false;

        [SerializeField]
        private bool gamFlg = false;

        [SerializeField]
        private bool IKUSIA_emote = false;

        [SerializeField]
        private bool backlightFlg;
        public AnimatorController controllerDef;
        public VRCExpressionsMenu menuDef;
        public VRCExpressionParameters paramDef;

        public AnimatorController controller;
        public VRCExpressionsMenu menu;
        public VRCExpressionParameters param;

        public bool questFlg1 = false;
        private string pathDir;
        public bool Butt;
        public bool Breast;
        public bool acce_wing;
        public bool earring;
        public bool Leg_acce;
        public bool bob;
        public bool bobtwin;
        public bool front_root;
        public bool twintail;
        public bool stomach;
        public bool side_root;
        public bool ribbon;
        public bool frill;
        public bool bag;
        public bool nuigurumi;
        public bool long_hair;
        public bool tail;
        public bool bag_wing;
        public bool bag_ribbon;
        public bool Cloth;

        public bool upperArm_collider1;
        public bool upperArm_collider2;
        public bool upperArm_collider3;
        public bool upperArm_collider4;
        public bool upperArm_collider5;
        public bool upperArm_collider6;
        public bool upperArm_collider7;

        public bool chest_collider1;
        public bool chest_collider2;

        public bool hip_collider1;
        public bool hip_collider2;
        public bool hip_collider3;

        public bool upperleg_collider1;
        public bool upperleg_collider2;
        public bool upperleg_collider3;
        public bool plane_collider;

        public bool AAORemoveFlg;

        public enum TextureResizeOption
        {
            LowerResolution,
            Delete,
        }

        public TextureResizeOption textureResize = TextureResizeOption.LowerResolution;

        private static readonly Dictionary<
            System.Type,
            System.Reflection.MethodInfo[]
        > methodCache = new();

        private struct ParamProcessConfig
        {
            public System.Func<bool> condition;
            public System.Action processAction;
            public System.Action afterAction;
        }

        private ParamProcessConfig[] GetParamConfigs(VRCAvatarDescriptor descriptor)
        {
            return new ParamProcessConfig[]
            {
                new()
                {
                    condition = () => true,
                    processAction = () => ProcessParam<IllRirikaParamDef>(descriptor),
                },
                new()
                {
                    condition = () => ClothFlg0 || ClothFlg,
                    processAction = () => ProcessParam<IllRirikaParamCloth>(descriptor),
                },
                new()
                {
                    condition = () => AccessoryFlg0,
                    processAction = () => ProcessParam<IllRirikaParamAccessory>(descriptor),
                },
                new()
                {
                    condition = () => HairFlg || HairFlg0,
                    processAction = () => ProcessParam<IllRirikaParamHair>(descriptor),
                },
                new()
                {
                    condition = () => petFlg,
                    processAction = () => ProcessParam<IllRirikaParamPet>(descriptor),
                    afterAction = () =>
                    {
                        if (!petFlg && petScale != 1.0f)
                        {
                            if (
                                descriptor.transform.Find(
                                    "Advanced/Pet/World/Model/ririka_pet/Root"
                                )
                                is Transform pet
                            )
                            {
                                pet.localScale = new Vector3(petScale, petScale, petScale);
                            }
                            if (
                                descriptor.transform.Find("Advanced/Pet/World/Grab/Grab1")
                                is Transform grab
                            )
                            {
                                grab.gameObject.GetComponent<VRCPhysBoneBase>().radius =
                                    0.04f * petScale;
                            }
                        }
                    },
                },
                new()
                {
                    condition = () => TPSFlg,
                    processAction = () => ProcessParam<IllRirikaParamTPS>(descriptor),
                },
                new()
                {
                    condition = () => ClairvoyanceFlg,
                    processAction = () => ProcessParam<IllRirikaParamClairvoyance>(descriptor),
                },
                new()
                {
                    condition = () => phoneFlg,
                    processAction = () => ProcessParam<IllRirikaParamPhone>(descriptor),
                    afterAction = () =>
                    {
                        if (!phoneFlg && phoneFlg1)
                        {
                            IllRirikaParam.DestroyObj(
                                descriptor.transform.Find("Advanced/phone/InCamera")
                            );
                            IllRirikaParam.DestroyObj(
                                descriptor.transform.Find("Advanced/phone/Photo_camera")
                            );
                            IllRirikaParam.DestroyObj(
                                descriptor.transform.Find("Advanced/phone/Spot Light")
                            );
                        }
                    },
                },
                new()
                {
                    condition = () => candyFlg,
                    processAction = () => ProcessParam<IllRirikaParamCandy>(descriptor),
                },
                new()
                {
                    condition = () => gamFlg,
                    processAction = () => ProcessParam<IllRirikaParamGam>(descriptor),
                },
                new()
                {
                    condition = () => doughnutFlg,
                    processAction = () => ProcessParam<IllRirikaParamDoughnut>(descriptor),
                },
                new()
                {
                    condition = () => drinkFlg,
                    processAction = () => ProcessParam<IllRirikaParamCanDrink>(descriptor),
                },
                new()
                {
                    condition = () => colliderJumpFlg,
                    processAction = () => ProcessParam<IllRirikaParamCollider>(descriptor),
                },
                new()
                {
                    condition = () => teppekiFlg,
                    processAction = () => ProcessParam<IllRirikaParamTeppeki>(descriptor),
                },
                new()
                {
                    condition = () => handHeartFlg,
                    processAction = () => ProcessParam<IllRirikaParamHandheart>(descriptor),
                },
                new()
                {
                    condition = () => noisepanelFlg,
                    processAction = () => ProcessParam<IllRirikaParamNoisepanel>(descriptor),
                },
                new()
                {
                    condition = () => neonFlg,
                    processAction = () => ProcessParam<IllRirikaParamNeon>(descriptor),
                },
                new()
                {
                    condition = () => BreastSizeFlg,
                    processAction = () => ProcessParam<IllRirikaParamBreastSize>(descriptor),
                },
                new()
                {
                    condition = () => backlightFlg,
                    processAction = () => ProcessParam<IllRirikaParamBacklight>(descriptor),
                },
                new()
                {
                    condition = () => WhiteBreathFlg,
                    processAction = () => ProcessParam<IllRirikaParamWhiteBreath>(descriptor),
                },
                new()
                {
                    condition = () => eightBitFlg,
                    processAction = () => ProcessParam<IllRirikaParam8bit>(descriptor),
                },
                new()
                {
                    condition = () => HeartGunFlg,
                    processAction = () => ProcessParam<IllRirikaParamHeartGun>(descriptor),
                },
                new()
                {
                    condition = () => PenCtrlFlg,
                    processAction = () => ProcessParam<IllRirikaParamHeartGun>(descriptor),
                },
                new()
                {
                    condition = () => FaceGestureFlg || FaceLockFlg || FaceValFlg,
                    processAction = () => ProcessParam<IllRirikaParamFaceGesture>(descriptor),
                },
                new()
                {
                    condition = () => mesugakiFaceFlg,
                    processAction = () => ProcessParam<IllRirikaParamMesugakiFace>(descriptor),
                    afterAction = () =>
                    {
                        if (!mesugakiFaceFlg && mesugakiFaceFlg1)
                        {
                            IllRirikaParam.DestroyObj(
                                descriptor.transform.Find("Advanced/mesugakiparticle")
                            );
                        }
                    },
                },
                new()
                {
                    condition = () => kamitukiFlg || nadeFlg || blinkFlg,
                    processAction = () => ProcessParam<IllRirikaParamFaceContact>(descriptor),
                },
            };
        }

        public void Execute(VRCAvatarDescriptor descriptor)
        {
            var stopwatch = Stopwatch.StartNew();
            var stepTimes = new Dictionary<string, long>();
            var step1 = Stopwatch.StartNew();
            InitializeAssets(descriptor);
            step1.Stop();
            stepTimes["InitializeAssets"] = step1.ElapsedMilliseconds;

            var step2 = Stopwatch.StartNew();
            SetUpMaterial(descriptor);

            foreach (var config in GetParamConfigs(descriptor))
            {
                if (config.condition())
                {
                    config.processAction();
                }
                config.afterAction?.Invoke();
            }

            if (TPSFlg & ClairvoyanceFlg)
            {
                var targetLayer = controller.layers.FirstOrDefault(l => l.name == "MainCtrlTree");
                foreach (var state in targetLayer.stateMachine.states)
                {
                    if (state.state.motion is not BlendTree rootTree)
                        continue;
                    rootTree.children = rootTree
                        .children.Where(c => !(c.motion.name == "VRMode"))
                        .ToArray();
                }
            }
            if (candyFlg & gamFlg & doughnutFlg & drinkFlg)
            {
                IllRirikaParam.DestroyObj(descriptor.transform.Find("Advanced/food"));
            }

            if (IKUSIA_emote)
            {
                foreach (var control in menu.controls)
                {
                    if (control.name == "IKUSIA_emote")
                    {
                        menu.controls.Remove(control);
                        break;
                    }
                }
            }

            if (ClothFlg0 && HairFlg0 && AccessoryFlg0)
            {
                var targetLayer = controller.layers.FirstOrDefault(l => l.name == "MainCtrlTree");
                foreach (var state in targetLayer.stateMachine.states)
                {
                    if (state.state.motion is not BlendTree rootTree)
                        continue;
                    rootTree.children = rootTree
                        .children.Where(c => !(c.motion.name == "Ririka closet"))
                        .ToArray();
                }
            }
            if (eightBitFlg && PenCtrlFlg && WhiteBreathFlg)
            {
                IllRirikaParam.DestroyObj(descriptor.transform.Find("Advanced/Particle"));
            }

            if (descriptor.transform.Find("body_b") is Transform body_b)
            {
                body_b
                    .gameObject.GetComponent<SkinnedMeshRenderer>()
                    .SetBlendShapeWeight(4, heelFlg1 ? 0 : 100);
                body_b
                    .gameObject.GetComponent<SkinnedMeshRenderer>()
                    .SetBlendShapeWeight(5, heelFlg1 ? 0 : 100);
                body_b
                    .gameObject.GetComponent<SkinnedMeshRenderer>()
                    .SetBlendShapeWeight(6, heelFlg2 ? 100 : 0);
                body_b
                    .gameObject.GetComponent<SkinnedMeshRenderer>()
                    .SetBlendShapeWeight(7, heelFlg2 ? 100 : 0);
            }
            if (questFlg1)
            {
                IllRirikaParam.DestroyObj(descriptor.transform.Find("Advanced/NadeCamera"));
            }

            IllRirikaDel4Quest.ProcessPhysicsAndColliders(
                descriptor,
                IllRirikaDel4Quest.GetPhysicsSettings(this)
            );
            step2.Stop();
            stepTimes["editProcessing"] = step2.ElapsedMilliseconds;
            var step4 = Stopwatch.StartNew();

            FinalizeAssets(descriptor);
            step4.Stop();
            stepTimes["FinalizeAssets"] = step4.ElapsedMilliseconds;

            stopwatch.Stop();
            UnityEngine.Debug.Log(
                $"最適化を実行しました！総処理時間: {stopwatch.ElapsedMilliseconds}ms ({stopwatch.Elapsed.TotalSeconds:F2}秒)"
            );

            foreach (var kvp in stepTimes)
            {
                UnityEngine.Debug.Log($"[Performance] {kvp.Key}: {kvp.Value}ms");
            }
        }

        private void ProcessParam<T>(VRCAvatarDescriptor descriptor)
            where T : ScriptableObject
        {
            var instance = ScriptableObject.CreateInstance<T>();
            var type = typeof(T);

            if (!methodCache.TryGetValue(type, out var methods))
            {
                methods = new[]
                {
                    type.GetMethod("Initialize"),
                    type.GetMethod("DeleteFx"),
                    type.GetMethod("DeleteFxBT"),
                    type.GetMethod("DeleteParam"),
                    type.GetMethod("DeleteVRCExpressions"),
                    type.GetMethod("ParticleOptimize"),
                    type.GetMethod("ChangeObj"),
                };
                methodCache[type] = methods;
            }

            var initializeMethod = methods[0];
            var deleteFxMethod = methods[1];
            var deleteFxBTMethod = methods[2];
            var deleteParamMethod = methods[3];
            var deleteVRCExpressionsMethod = methods[4];
            var ParticleOptimizeMethod = methods[5];
            var changeObjMethod = methods[6];

            if (initializeMethod != null)
            {
                try
                {
                    int count = initializeMethod.GetParameters().Length;
                    object result = initializeMethod.Invoke(
                        instance,
                        count == 3
                            ? new object[] { descriptor, controller, this }
                            : new object[] { descriptor, controller }
                    );

                    deleteFxMethod?.Invoke(result, null);
                    deleteFxBTMethod?.Invoke(result, null);
                    deleteParamMethod?.Invoke(result, null);
                    deleteVRCExpressionsMethod?.Invoke(result, new object[] { menu, param });
                    ParticleOptimizeMethod?.Invoke(result, null);
                    changeObjMethod?.Invoke(result, null);
                }
                catch (Exception ex)
                {
                    UnityEngine.Debug.LogError(
                        $"[ProcessParam] Error processing {type.Name}: {ex.Message}"
                    );
                    UnityEngine.Debug.LogError($"[ProcessParam] Stack trace: {ex.StackTrace}");
                }
            }
        }

        private void SetUpMaterial(VRCAvatarDescriptor descriptor)
        {
            string[] materialGUIDList1 = new[]
            {
                "c0252ca034162eb46990a23810a1e07d",
                "5611fc4b398eecc4f886073d221f23e2",
                "fac45238542421549bef06f342a5a28b",
                "9ad6719639344ff4c8c7c0d9e29cc8a0",
                "53e35fe2714b3f543835f9876594a5e1",
                "5bfb354df46565d479d5b3d70a86b0b6",
            };
            string[] materialGUIDList2 = new[]
            {
                "367477648768ca84eb734d731414034b",
                "39fc30cc23d31ce449650231e1a8d813",
                "0c1c9891338f7fd43a8f85667813500e",
                "788bbbd12cd3e1642a5ceb4e3aedfbd9",
                "3cd89e1491e3baa4b89f409d4d14cd62",
                "2ef2d823c931e40458c36d3ce25dccdf",
            };
            string[][] transformLists = new[]
            {
                new[] { "body_b" },
                new[] { "Body" },
                new[] { "Bra" },
                new[] { "hair_main", "hair_bob", "hair_back_long" },
                new[] { "Cloth", "cover_arm", "Over knee socks" },
                new[] { "Bag", "Boots", "cloth_Accessories", "Outer", "Tail" },
            };

            if (colorFlg0)
            {
                foreach (var materialGUID in materialGUIDList1)
                {
                    var i = Array.IndexOf(materialGUIDList1, materialGUID);
                    for (int j = 0; j < transformLists[i].Length; j++)
                    {
                        if (descriptor.transform.Find(transformLists[i][j]) is Transform hair)
                        {
                            var renderer = hair.GetComponent<SkinnedMeshRenderer>();

                            var materials = renderer.sharedMaterials;

                            materials[0] = AssetDatabase.LoadAssetAtPath<Material>(
                                AssetDatabase.GUIDToAssetPath(materialGUID)
                            );

                            renderer.sharedMaterials = materials;
                        }
                    }
                }
            }
            if (colorFlg1)
                foreach (var hairNm in transformLists[3])
                {
                    if (descriptor.transform.Find(hairNm) is Transform hair)
                    {
                        var renderer = hair.GetComponent<SkinnedMeshRenderer>();

                        var materials = renderer.sharedMaterials;

                        materials[0] = AssetDatabase.LoadAssetAtPath<Material>(
                            AssetDatabase.GUIDToAssetPath("f8a05f950d33acd41b3c5835fb973a7c")
                        );

                        renderer.sharedMaterials = materials;
                    }
                }
            if (colorFlg2)
            {
                foreach (var materialGUID in materialGUIDList2)
                {
                    var i = Array.IndexOf(materialGUIDList2, materialGUID);
                    for (int j = 0; j < transformLists[i].Length; j++)
                    {
                        if (descriptor.transform.Find(transformLists[i][j]) is Transform hair)
                        {
                            var renderer = hair.GetComponent<SkinnedMeshRenderer>();
                            var materials = renderer.sharedMaterials;
                            materials[0] = AssetDatabase.LoadAssetAtPath<Material>(
                                AssetDatabase.GUIDToAssetPath(materialGUID)
                            );
                            renderer.sharedMaterials = materials;
                        }
                    }
                }
            }
        }

        private void InitializeAssets(VRCAvatarDescriptor descriptor)
        {
            pathDir = pathDirPrefix + descriptor.gameObject.name + pathDirSuffix;
            if (AssetDatabase.LoadAssetAtPath<AnimatorController>(pathDir + pathName) != null)
            {
                AssetDatabase.DeleteAsset(pathDir + pathName);
                AssetDatabase.DeleteAsset(pathDir + "Menu");
                AssetDatabase.DeleteAsset(pathDir + "paryi_paraments.asset");
            }
            if (!Directory.Exists(pathDir))
            {
                Directory.CreateDirectory(pathDir);
            }

            if (!controllerDef)
            {
                controllerDef =
                    descriptor.baseAnimationLayers[4].animatorController as AnimatorController;
            }
            AssetDatabase.CopyAsset(AssetDatabase.GetAssetPath(controllerDef), pathDir + pathName);

            controller = AssetDatabase.LoadAssetAtPath<AnimatorController>(pathDir + pathName);

            if (!menuDef)
            {
                menuDef = descriptor.expressionsMenu;
            }
            var iconPath = pathDir + "/icon";
            if (!Directory.Exists(iconPath))
            {
                Directory.CreateDirectory(iconPath);
            }
            menu = DuplicateExpressionMenu(menuDef, pathDir, iconPath, questFlg1, textureResize);

            if (!paramDef)
            {
                paramDef = descriptor.expressionParameters;
                paramDef.name = descriptor.expressionParameters.name;
            }
            param = ScriptableObject.CreateInstance<VRCExpressionParameters>();
            EditorUtility.CopySerialized(paramDef, param);
            param.name = paramDef.name;
            EditorUtility.SetDirty(param);
            AssetDatabase.CreateAsset(param, pathDir + param.name + ".asset");
        }

        private void FinalizeAssets(VRCAvatarDescriptor descriptor)
        {
            RemoveUnusedMenuControls(menu, param);
            EditorUtility.SetDirty(controller);
            MarkAllMenusDirty(menu);
            EditorUtility.SetDirty(param);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            descriptor.baseAnimationLayers[4].animatorController = controller;
            descriptor.expressionsMenu = menu;
            descriptor.expressionParameters = param;
            EditorUtility.SetDirty(descriptor);
        }

        private static void RemoveUnusedMenuControls(
            VRCExpressionsMenu menu,
            VRCExpressionParameters param
        )
        {
            if (menu == null)
                return;

            for (int i = menu.controls.Count - 1; i >= 0; i--)
            {
                var control = menu.controls[i];
                bool shouldRemove = true;

                if (string.IsNullOrEmpty(control.parameter.name))
                {
                    shouldRemove = false;
                }
                else
                {
                    if (param.parameters.Any(p => p.name == control.parameter.name))
                    {
                        shouldRemove = false;
                    }
                }

                if (control.subMenu != null)
                {
                    RemoveUnusedMenuControls(control.subMenu, param);
                    if (control.subMenu.controls.Count > 0)
                    {
                        shouldRemove = false;
                    }
                }

                if (shouldRemove)
                {
                    menu.controls.RemoveAt(i);
                }
            }
        }

        private static void MarkAllMenusDirty(VRCExpressionsMenu menu)
        {
            if (menu == null)
                return;

            EditorUtility.SetDirty(menu);

            foreach (var control in menu.controls)
            {
                if (control.subMenu != null)
                {
                    MarkAllMenusDirty(control.subMenu);
                }
            }
        }

        public static VRCExpressionsMenu DuplicateExpressionMenu(
            VRCExpressionsMenu originalMenu,
            string parentPath,
            string iconPath,
            bool questFlg1,
            TextureResizeOption textureResize
        )
        {
            return DuplicateExpressionMenu(
                originalMenu,
                parentPath,
                iconPath,
                questFlg1,
                textureResize,
                null,
                null,
                null
            );
        }

        private static VRCExpressionsMenu DuplicateExpressionMenu(
            VRCExpressionsMenu originalMenu,
            string parentPath,
            string iconPath,
            bool questFlg1,
            TextureResizeOption textureResize,
            VRCExpressionsMenu rootMenuAsset = null,
            Dictionary<VRCExpressionsMenu, VRCExpressionsMenu> processedMenus = null,
            Dictionary<string, Texture2D> processedIcons = null
        )
        {
            if (originalMenu == null)
            {
                UnityEngine.Debug.LogError("元のExpression Menuがありません");
                return null;
            }

            bool isRootCall = processedMenus == null;
            if (isRootCall)
            {
                processedMenus = new Dictionary<VRCExpressionsMenu, VRCExpressionsMenu>();
                processedIcons = new Dictionary<string, Texture2D>();
            }

            if (processedMenus.ContainsKey(originalMenu))
            {
                return processedMenus[originalMenu];
            }

            VRCExpressionsMenu newMenu = Instantiate(originalMenu);
            newMenu.name = originalMenu.name;

            processedMenus[originalMenu] = newMenu;

            if (isRootCall)
            {
                string menuAssetPath = Path.Combine(parentPath, originalMenu.name + ".asset");
                AssetDatabase.CreateAsset(newMenu, menuAssetPath);
                rootMenuAsset = newMenu;
            }
            else if (rootMenuAsset != null)
            {
                AssetDatabase.AddObjectToAsset(newMenu, rootMenuAsset);
            }

            for (int i = 0; i < newMenu.controls.Count; i++)
            {
                var control = newMenu.controls[i];
                if (questFlg1)
                {
                    if (textureResize == TextureResizeOption.LowerResolution)
                    {
                        var originalControl = originalMenu.controls[i];

                        if (originalControl.icon != null)
                        {
                            string iconAssetPath = AssetDatabase.GetAssetPath(originalControl.icon);
                            if (!string.IsNullOrEmpty(iconAssetPath))
                            {
                                string iconFileName = Path.GetFileName(iconAssetPath);
                                string destPath = Path.Combine(iconPath, iconFileName);

                                if (processedIcons.ContainsKey(iconAssetPath))
                                {
                                    control.icon = processedIcons[iconAssetPath];
                                }
                                else
                                {
                                    if (!File.Exists(destPath))
                                    {
                                        File.Copy(iconAssetPath, destPath, true);
                                        AssetDatabase.ImportAsset(destPath);
                                    }

                                    var copiedIcon = AssetDatabase.LoadAssetAtPath<Texture2D>(
                                        destPath
                                    );
                                    if (copiedIcon != null)
                                    {
                                        var importer =
                                            AssetImporter.GetAtPath(destPath) as TextureImporter;
                                        if (importer != null)
                                        {
                                            importer.maxTextureSize = 32;
                                            importer.SaveAndReimport();
                                        }

                                        processedIcons[iconAssetPath] = copiedIcon;
                                        control.icon = copiedIcon;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        control.icon = null;
                    }
                }
                if (control.subMenu != null)
                {
                    control.subMenu = DuplicateExpressionMenu(
                        control.subMenu,
                        parentPath,
                        iconPath,
                        questFlg1,
                        textureResize,
                        rootMenuAsset,
                        processedMenus,
                        processedIcons
                    );
                }
            }
            EditorUtility.SetDirty(newMenu);
            if (isRootCall)
            {
                AssetDatabase.SaveAssets();
            }
            return newMenu;
        }
    }
}
#endif
