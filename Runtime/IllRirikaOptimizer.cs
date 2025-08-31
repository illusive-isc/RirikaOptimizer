using System.Collections.Generic;
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
        // 保存先のパス設定
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

        [SerializeField]
        private bool ClothFlg = false;

        [SerializeField]
        private bool ClothFlg0 = false;

        public bool ClothFlg1 = false;

        [SerializeField]
        private bool ClothFlg2 = false;

        [SerializeField]
        private bool ClothFlg3 = false;

        [SerializeField]
        private bool ClothFlg4 = false;

        [SerializeField]
        private bool ClothFlg5 = false;

        [SerializeField]
        private bool ClothFlg6 = false;

        [SerializeField]
        private bool ClothFlg7 = false;

        [SerializeField]
        private bool ClothFlg8 = false;

        [SerializeField]
        private bool ClothFlg9 = false;

        [SerializeField]
        private bool ClothFlg10 = false;

        [SerializeField]
        private bool AccessoryFlg0 = false;

        [SerializeField]
        private bool AccessoryFlg1 = false;

        [SerializeField]
        private bool AccessoryFlg2 = false;

        [SerializeField]
        private bool AccessoryFlg3 = false;

        [SerializeField]
        private bool AccessoryFlg4 = false;

        [SerializeField]
        private bool HairFlg0 = false;

        [SerializeField]
        private bool HairFlg1 = false;

        [SerializeField]
        private bool HairFlg2 = false;

        [SerializeField]
        private bool HairFlg3 = false;

        public bool HairFlg4 = false;

        public bool HairFlg5 = false;

        [SerializeField]
        private bool HairFlg6 = false;

        public bool HairFlg7 = false;

        public bool HairFlg8 = false;

        [SerializeField]
        private bool HairFlg = false;

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
        private bool BreastSizeFlg = false;

        public bool BreastSizeFlg2 = false;

        [SerializeField]
        private bool WhiteBreathFlg = false;

        [SerializeField]
        private bool eightBitFlg = false;

        [SerializeField]
        private bool PenCtrlFlg = false;

        [SerializeField]
        private bool HeartGunFlg = false;

        [SerializeField]
        private bool FaceGestureFlg = false;

        [SerializeField]
        private bool FaceLockFlg = false;

        [SerializeField]
        private bool FaceValFlg = false;

        [SerializeField]
        private bool blinkFlg = false;

        [SerializeField]
        private bool kamitukiFlg = false;

        [SerializeField]
        private bool nadeFlg = false;

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

        private bool FaceFlg = false;
        private bool cameraFlg = false;
        private bool foodFlg = false;
        private bool loliFlg = false;
        private string pathDir;

        public void Execute(VRCAvatarDescriptor descriptor)
        {
            FaceFlg = false;
            cameraFlg = false;
            foodFlg = false;
            loliFlg = false;
            // 保存先ディレクトリの作成
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

            // 基本コントローラの参照取得（なければ baseAnimationLayers[4] から取得）
            if (!controllerDef)
            {
                controllerDef =
                    descriptor.baseAnimationLayers[4].animatorController as AnimatorController;
            }
            AssetDatabase.CopyAsset(AssetDatabase.GetAssetPath(controllerDef), pathDir + pathName);

            controller = AssetDatabase.LoadAssetAtPath<AnimatorController>(pathDir + pathName);

            // ExpressionMenu の複製
            if (!menuDef)
            {
                menuDef = descriptor.expressionsMenu;
            }
            menu = DuplicateExpressionMenu(menuDef, pathDir);

            // ExpressionParameters の複製
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
            IllRirikaParamDef illRirikaParamDef =
                ScriptableObject.CreateInstance<IllRirikaParamDef>();
            illRirikaParamDef
                .Initialize(descriptor, controller)
                .DeleteFx()
                .DeleteFxBT()
                .DeleteParam()
                .DeleteVRCExpressions(menu, param)
                .ParticleOptimize()
                .DestroyObj();
            List<string> hairList = new() { "hair_main", "hair_bob", "hair_back_long" };
            List<string> clothList = new() { "Cloth", "cover_arm", "Over knee socks" };
            List<string> cloth1List = new()
            {
                "Bag",
                "Boots",
                "cloth_Accessories",
                "Outer",
                "Tail",
            };
            if (colorFlg0)
            {
                if (descriptor.transform.Find("body_b") is Transform body)
                {
                    var renderer = body.GetComponent<SkinnedMeshRenderer>();

                    var materials = renderer.sharedMaterials;

                    materials[0] = AssetDatabase.LoadAssetAtPath<Material>(
                        AssetDatabase.GUIDToAssetPath("c0252ca034162eb46990a23810a1e07d")
                    );

                    renderer.sharedMaterials = materials;
                }
                if (descriptor.transform.Find("Body") is Transform head)
                {
                    var renderer = head.GetComponent<SkinnedMeshRenderer>();

                    var materials = renderer.sharedMaterials;

                    materials[0] = AssetDatabase.LoadAssetAtPath<Material>(
                        AssetDatabase.GUIDToAssetPath("5611fc4b398eecc4f886073d221f23e2")
                    );

                    renderer.sharedMaterials = materials;
                }
                if (descriptor.transform.Find("Bra") is Transform Bra)
                {
                    var renderer = Bra.GetComponent<SkinnedMeshRenderer>();

                    var materials = renderer.sharedMaterials;

                    materials[0] = AssetDatabase.LoadAssetAtPath<Material>(
                        AssetDatabase.GUIDToAssetPath("fac45238542421549bef06f342a5a28b")
                    );

                    renderer.sharedMaterials = materials;
                }
                foreach (var hairNm in hairList)
                {
                    if (descriptor.transform.Find(hairNm) is Transform hair)
                    {
                        var renderer = hair.GetComponent<SkinnedMeshRenderer>();

                        var materials = renderer.sharedMaterials;

                        materials[0] = AssetDatabase.LoadAssetAtPath<Material>(
                            AssetDatabase.GUIDToAssetPath("9ad6719639344ff4c8c7c0d9e29cc8a0")
                        );

                        renderer.sharedMaterials = materials;
                    }
                }
                foreach (var clothNm in clothList)
                {
                    if (descriptor.transform.Find(clothNm) is Transform cloth)
                    {
                        var renderer = cloth.GetComponent<SkinnedMeshRenderer>();

                        var materials = renderer.sharedMaterials;

                        materials[0] = AssetDatabase.LoadAssetAtPath<Material>(
                            AssetDatabase.GUIDToAssetPath("53e35fe2714b3f543835f9876594a5e1")
                        );

                        renderer.sharedMaterials = materials;
                    }
                }
                foreach (var cloth1Nm in cloth1List)
                {
                    if (descriptor.transform.Find(cloth1Nm) is Transform cloth1)
                    {
                        var renderer = cloth1.GetComponent<SkinnedMeshRenderer>();

                        var materials = renderer.sharedMaterials;

                        materials[0] = AssetDatabase.LoadAssetAtPath<Material>(
                            AssetDatabase.GUIDToAssetPath("5bfb354df46565d479d5b3d70a86b0b6")
                        );

                        renderer.sharedMaterials = materials;
                    }
                }
            }
            if (colorFlg1)
                foreach (var hairNm in hairList)
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
                if (descriptor.transform.Find("body_b") is Transform body)
                {
                    var renderer = body.GetComponent<SkinnedMeshRenderer>();

                    var materials = renderer.sharedMaterials;

                    materials[0] = AssetDatabase.LoadAssetAtPath<Material>(
                        AssetDatabase.GUIDToAssetPath("367477648768ca84eb734d731414034b")
                    );

                    renderer.sharedMaterials = materials;
                }
                if (descriptor.transform.Find("Body") is Transform head)
                {
                    var renderer = head.GetComponent<SkinnedMeshRenderer>();

                    var materials = renderer.sharedMaterials;

                    materials[0] = AssetDatabase.LoadAssetAtPath<Material>(
                        AssetDatabase.GUIDToAssetPath("39fc30cc23d31ce449650231e1a8d813")
                    );

                    renderer.sharedMaterials = materials;
                }
                if (descriptor.transform.Find("Bra") is Transform Bra)
                {
                    var renderer = Bra.GetComponent<SkinnedMeshRenderer>();

                    var materials = renderer.sharedMaterials;

                    materials[0] = AssetDatabase.LoadAssetAtPath<Material>(
                        AssetDatabase.GUIDToAssetPath("0c1c9891338f7fd43a8f85667813500e")
                    );

                    renderer.sharedMaterials = materials;
                }
                foreach (var hairNm in hairList)
                {
                    if (descriptor.transform.Find(hairNm) is Transform hair)
                    {
                        var renderer = hair.GetComponent<SkinnedMeshRenderer>();

                        var materials = renderer.sharedMaterials;

                        materials[0] = AssetDatabase.LoadAssetAtPath<Material>(
                            AssetDatabase.GUIDToAssetPath("788bbbd12cd3e1642a5ceb4e3aedfbd9")
                        );

                        renderer.sharedMaterials = materials;
                    }
                }
                foreach (var clothNm in clothList)
                {
                    if (descriptor.transform.Find(clothNm) is Transform cloth)
                    {
                        var renderer = cloth.GetComponent<SkinnedMeshRenderer>();

                        var materials = renderer.sharedMaterials;

                        materials[0] = AssetDatabase.LoadAssetAtPath<Material>(
                            AssetDatabase.GUIDToAssetPath("3cd89e1491e3baa4b89f409d4d14cd62")
                        );

                        renderer.sharedMaterials = materials;
                    }
                }
                foreach (var cloth1Nm in cloth1List)
                {
                    if (descriptor.transform.Find(cloth1Nm) is Transform cloth1)
                    {
                        var renderer = cloth1.GetComponent<SkinnedMeshRenderer>();

                        var materials = renderer.sharedMaterials;

                        materials[0] = AssetDatabase.LoadAssetAtPath<Material>(
                            AssetDatabase.GUIDToAssetPath("2ef2d823c931e40458c36d3ce25dccdf")
                        );

                        renderer.sharedMaterials = materials;
                    }
                }
            }
            if (ClothFlg0 || ClothFlg)
            {
                IllRirikaParamCloth illRirikaParamCloth =
                    ScriptableObject.CreateInstance<IllRirikaParamCloth>();
                illRirikaParamCloth
                    .Initialize(descriptor, controller, ClothFlg9)
                    .DeleteFxBT()
                    .DeleteParam()
                    .DeleteVRCExpressions(menu, param)
                    .ChangeObj(
                        ClothFlg1,
                        ClothFlg2,
                        ClothFlg3,
                        ClothFlg4,
                        ClothFlg5,
                        ClothFlg6,
                        ClothFlg7,
                        ClothFlg8
                    );

                if (ClothFlg)
                    illRirikaParamCloth.DestroyObjects();
                if (ClothFlg10)
                {
                    var prefab = AssetDatabase.LoadAssetAtPath<Object>(
                        AssetDatabase.GUIDToAssetPath("416063b3d4900a3468a716c27a8f6dee")
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
                }
            }

            if (AccessoryFlg0)
            {
                IllRirikaParamAccessory illRirikaParamAccessory =
                    ScriptableObject.CreateInstance<IllRirikaParamAccessory>();
                illRirikaParamAccessory
                    .Initialize(descriptor, controller)
                    .DeleteFxBT()
                    .DeleteParam()
                    .DeleteVRCExpressions(menu, param)
                    .DestroyObj(AccessoryFlg1, AccessoryFlg2, AccessoryFlg3, AccessoryFlg4);
            }

            if (HairFlg || HairFlg0)
            {
                IllRirikaParamHair illRirikaParamHair =
                    ScriptableObject.CreateInstance<IllRirikaParamHair>();
                illRirikaParamHair
                    .Initialize(descriptor, controller, AccessoryFlg1)
                    .DeleteFx()
                    .DeleteFxBT()
                    .DeleteParam()
                    .DeleteVRCExpressions(menu, param);

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
                if (HairFlg)
                    illRirikaParamHair.DestroyObj();
            }
            if (petFlg)
            {
                IllRirikaParamPet illRirikaParamKIllRirikaParamPet =
                    ScriptableObject.CreateInstance<IllRirikaParamPet>();
                illRirikaParamKIllRirikaParamPet
                    .Initialize(descriptor, controller)
                    .DeleteFx()
                    .DeleteFxBT()
                    .DeleteParam()
                    .DeleteVRCExpressions(menu, param)
                    .DestroyObj();
            }
            else if (petScale != 1.0f)
            {
                if (
                    descriptor.transform.Find("Advanced/Pet/World/Model/ririka_pet/Root")
                    is Transform pet
                )
                {
                    pet.localScale = new Vector3(petScale, petScale, petScale);
                }
                if (descriptor.transform.Find("Advanced/Pet/World/Grab/Grab1") is Transform grab)
                {
                    grab.gameObject.GetComponent<VRCPhysBoneBase>().radius = 0.04f * petScale;
                }
            }
            if (TPSFlg)
            {
                IllRirikaParamTPS illRirikaParamTPS =
                    ScriptableObject.CreateInstance<IllRirikaParamTPS>();
                illRirikaParamTPS
                    .Initialize(descriptor, controller)
                    .DeleteFxBT()
                    .DeleteParam()
                    .DeleteVRCExpressions(menu, param)
                    .DestroyObj();
            }
            if (ClairvoyanceFlg)
            {
                IllRirikaParamClairvoyance illRirikaParamClairvoyance =
                    ScriptableObject.CreateInstance<IllRirikaParamClairvoyance>();
                illRirikaParamClairvoyance
                    .Initialize(descriptor, controller)
                    .DeleteFxBT()
                    .DeleteParam()
                    .DeleteVRCExpressions(menu, param)
                    .DestroyObj();
            }
            if (phoneFlg)
            {
                IllRirikaParamPhone illRirikaParamPhone =
                    ScriptableObject.CreateInstance<IllRirikaParamPhone>();
                illRirikaParamPhone
                    .Initialize(descriptor, controller)
                    .DeleteFx()
                    .DeleteFxBT()
                    .DeleteParam()
                    .DeleteVRCExpressions(menu, param)
                    .DestroyObj();
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

                if (!phoneFlg)
                    foreach (var control in menu.controls)
                        if (control.name == "Gimmick")
                        {
                            var expressionsSubMenu = control.subMenu;

                            var flg = false;
                            foreach (var control2 in expressionsSubMenu.controls)
                            {
                                if (control2.name == "camera")
                                {
                                    var expressionsSubMenu2 = control2.subMenu;
                                    foreach (var control3 in expressionsSubMenu2.controls)
                                        if (control3.name == "phone")
                                        {
                                            expressionsSubMenu.controls.Add(control3);
                                            flg = true;
                                            break;
                                        }
                                }
                                if (flg)
                                    break;
                            }
                            control.subMenu = expressionsSubMenu;
                            break;
                        }
                foreach (var control in menu.controls)
                    if (control.name == "Gimmick")
                    {
                        var expressionsSubMenu = control.subMenu;

                        foreach (var control2 in expressionsSubMenu.controls)
                        {
                            if (control2.name == "camera")
                            {
                                expressionsSubMenu.controls.Remove(control2);
                                break;
                            }
                        }
                        control.subMenu = expressionsSubMenu;
                        break;
                    }
                cameraFlg = true;
            }

            if (candyFlg)
            {
                IllRirikaParamCandy illRirikaParamCandy =
                    ScriptableObject.CreateInstance<IllRirikaParamCandy>();
                illRirikaParamCandy
                    .Initialize(descriptor, controller)
                    .DeleteFxBT()
                    .DeleteParam()
                    .DeleteVRCExpressions(menu, param)
                    .DestroyObj();
            }
            if (gamFlg)
            {
                IllRirikaParamGam illRirikaParamGam =
                    ScriptableObject.CreateInstance<IllRirikaParamGam>();
                illRirikaParamGam
                    .Initialize(descriptor, controller)
                    .DeleteFx()
                    .DeleteParam()
                    .DeleteVRCExpressions(menu, param);
            }

            if (doughnutFlg)
            {
                IllRirikaParamDoughnut illRirikaParamDoughnut =
                    ScriptableObject.CreateInstance<IllRirikaParamDoughnut>();
                illRirikaParamDoughnut
                    .Initialize(descriptor, controller)
                    .DeleteFx()
                    .DeleteFxBT()
                    .DeleteParam()
                    .DeleteVRCExpressions(menu, param)
                    .DestroyObj();
            }

            if (drinkFlg)
            {
                IllRirikaParamCanDrink illRirikaParamCanDrink =
                    ScriptableObject.CreateInstance<IllRirikaParamCanDrink>();
                illRirikaParamCanDrink
                    .Initialize(descriptor, controller)
                    .DeleteFxBT()
                    .DeleteParam()
                    .DeleteVRCExpressions(menu, param)
                    .DestroyObj();
            }
            if (candyFlg & gamFlg & doughnutFlg & drinkFlg)
            {
                foreach (var control in menu.controls)
                {
                    if (control.name == "Gimmick")
                    {
                        var expressionsSubMenu = control.subMenu;

                        foreach (var control2 in expressionsSubMenu.controls)
                        {
                            if (control2.name == "food")
                            {
                                expressionsSubMenu.controls.Remove(control2);
                                break;
                            }
                        }
                        control.subMenu = expressionsSubMenu;
                        break;
                    }
                }
                IllRirikaParam.DestroyObj(descriptor.transform.Find("Advanced/food"));
                foodFlg = true;
            }
            if (colliderJumpFlg)
            {
                IllRirikaParamCollider illRirikaParamCollider =
                    ScriptableObject.CreateInstance<IllRirikaParamCollider>();
                illRirikaParamCollider
                    .Initialize(descriptor, controller)
                    .DeleteFx()
                    .DeleteFxBT()
                    .DeleteParam()
                    .DeleteVRCExpressions(menu, param)
                    .DestroyObj();
            }
            if (teppekiFlg)
            {
                IllRirikaParamTeppeki illRirikaParamTeppeki =
                    ScriptableObject.CreateInstance<IllRirikaParamTeppeki>();
                illRirikaParamTeppeki
                    .Initialize(descriptor, controller)
                    .DeleteFxBT()
                    .DeleteParam()
                    .DeleteVRCExpressions(menu, param)
                    .DestroyObj();
            }
            if (handHeartFlg)
            {
                IllRirikaParamHandheart illRirikaParamHandheart =
                    ScriptableObject.CreateInstance<IllRirikaParamHandheart>();
                illRirikaParamHandheart
                    .Initialize(descriptor, controller)
                    .DeleteFxBT()
                    .DeleteParam()
                    .DeleteVRCExpressions(menu, param)
                    .DestroyObj();
            }
            if (noisepanelFlg)
            {
                IllRirikaParamNoisepanel illRirikaParamNoisepanel =
                    ScriptableObject.CreateInstance<IllRirikaParamNoisepanel>();
                illRirikaParamNoisepanel
                    .Initialize(descriptor, controller)
                    .DeleteFxBT()
                    .DeleteParam()
                    .DeleteVRCExpressions(menu, param)
                    .DestroyObj();
            }
            if (neonFlg)
            {
                IllRirikaParamNeon illRirikaParamNeon =
                    ScriptableObject.CreateInstance<IllRirikaParamNeon>();
                illRirikaParamNeon
                    .Initialize(descriptor, controller)
                    .DeleteFxBT()
                    .DeleteParam()
                    .DeleteVRCExpressions(menu, param)
                    .DestroyObj();
            }

            if (BreastSizeFlg)
            {
                IllRirikaParamBreastSize illRirikaParamBreastSize =
                    ScriptableObject.CreateInstance<IllRirikaParamBreastSize>();
                illRirikaParamBreastSize
                    .Initialize(descriptor, controller)
                    .DeleteFxBT()
                    .DeleteParam()
                    .DeleteVRCExpressions(menu, param)
                    .ChangeObj(BreastSizeFlg2);
            }
            if (backlightFlg)
            {
                IllRirikaParamBacklight illRirikaParamBacklight =
                    ScriptableObject.CreateInstance<IllRirikaParamBacklight>();
                illRirikaParamBacklight
                    .Initialize(descriptor, controller)
                    .DeleteFxBT()
                    .DeleteParam()
                    .DeleteVRCExpressions(menu, param);
            }
            if (WhiteBreathFlg)
            {
                IllRirikaParamWhiteBreath illRirikaParamWhiteBreath =
                    ScriptableObject.CreateInstance<IllRirikaParamWhiteBreath>();
                illRirikaParamWhiteBreath
                    .Initialize(descriptor, controller)
                    .DeleteFxBT()
                    .DeleteParam()
                    .DeleteVRCExpressions(menu, param)
                    .DestroyObj();
            }

            if (eightBitFlg)
            {
                IllRirikaParam8bit illRirikaParam8bit =
                    ScriptableObject.CreateInstance<IllRirikaParam8bit>();
                illRirikaParam8bit
                    .Initialize(descriptor, controller)
                    .DeleteFxBT()
                    .DeleteParam()
                    .DeleteVRCExpressions(menu, param)
                    .DestroyObj();
            }
            if (HeartGunFlg)
            {
                IllRirikaParamHeartGun illRirikaParamHeartGun =
                    ScriptableObject.CreateInstance<IllRirikaParamHeartGun>();
                illRirikaParamHeartGun
                    .Initialize(descriptor, controller)
                    .DeleteFx()
                    .DeleteFxBT()
                    .DeleteParam()
                    .DeleteVRCExpressions(menu, param)
                    .DestroyObj();
            }
            if (PenCtrlFlg)
            {
                IllRirikaParamPenCtrl illRirikaParamPenCtrl =
                    ScriptableObject.CreateInstance<IllRirikaParamPenCtrl>();
                illRirikaParamPenCtrl
                    .Initialize(descriptor, controller)
                    .DeleteFx(HeartGunFlg)
                    .DeleteFxBT()
                    .DeleteParam()
                    .DeleteVRCExpressions(menu, param)
                    .DestroyObj();
            }

            if (FaceGestureFlg || FaceLockFlg || FaceValFlg)
            {
                IllRirikaParamFaceGesture illRirikaParamFaceGesture =
                    ScriptableObject.CreateInstance<IllRirikaParamFaceGesture>();
                illRirikaParamFaceGesture
                    .Initialize(descriptor, controller, FaceGestureFlg, FaceLockFlg, FaceValFlg)
                    .DeleteFx()
                    .DeleteParam()
                    .DeleteVRCExpressions(menu, param);
            }
            if (mesugakiFaceFlg)
            {
                IllRirikaParamMesugakiFace illRirikaParamMesugakiFace =
                    ScriptableObject.CreateInstance<IllRirikaParamMesugakiFace>();
                illRirikaParamMesugakiFace
                    .Initialize(descriptor, controller)
                    .DeleteFx()
                    .DeleteFxBT()
                    .DeleteParam()
                    .DeleteVRCExpressions(menu, param)
                    .DestroyObj();
            }
            if (kamitukiFlg || nadeFlg || blinkFlg)
            {
                IllRirikaParamFaceContact illRirikaParamFaceGesture =
                    ScriptableObject.CreateInstance<IllRirikaParamFaceContact>();
                illRirikaParamFaceGesture
                    .Initialize(descriptor, controller, kamitukiFlg, nadeFlg, blinkFlg)
                    .DeleteParam()
                    .DeleteFxBT()
                    .DeleteVRCExpressions(menu, param);
            }
            if (
                (FaceGestureFlg || (FaceLockFlg && FaceValFlg))
                && kamitukiFlg
                && nadeFlg
                && blinkFlg
            )
            {
                foreach (var control in menu.controls)
                {
                    if (control.name == "Gimmick")
                    {
                        var expressionsSubMenu = control.subMenu;

                        foreach (var control2 in expressionsSubMenu.controls)
                        {
                            if (control2.name == "Face")
                            {
                                expressionsSubMenu.controls.Remove(control2);
                                break;
                            }
                        }
                        control.subMenu = expressionsSubMenu;
                        break;
                    }
                }
                FaceFlg = true;
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
                foreach (var control in menu.controls)
                {
                    if (control.name == "closet")
                    {
                        menu.controls.Remove(control);
                        break;
                    }
                }
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
                if (HeartGunFlg)
                    foreach (var control in menu.controls)
                    {
                        if (control.name == "Particle")
                        {
                            menu.controls.Remove(control);
                            break;
                        }
                    }
            }
            if (teppekiFlg & handHeartFlg & noisepanelFlg & neonFlg & mesugakiFaceFlg)
            {
                foreach (var control in menu.controls)
                {
                    if (control.name == "Gimmick")
                    {
                        var expressionsSubMenu = control.subMenu;

                        foreach (var control2 in expressionsSubMenu.controls)
                        {
                            if (control2.name == "loli gimmik")
                            {
                                expressionsSubMenu.controls.Remove(control2);
                                break;
                            }
                        }
                        control.subMenu = expressionsSubMenu;
                        break;
                    }
                }
                loliFlg = true;
            }
            if (
                backlightFlg
                & FaceFlg
                & BreastSizeFlg
                & cameraFlg
                & phoneFlg
                & foodFlg
                & loliFlg
                & petFlg
            )
            {
                var toRemove = menu.controls.FirstOrDefault(c => c.name == "Gimmick");
                if (toRemove != null)
                {
                    menu.controls.Remove(toRemove);
                }
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
            var assetGuids = AssetDatabase.FindAssets(
                "t:VRCExpressionsMenu",
                new[] { pathDir + "Menu" }
            );

            Dictionary<string, VRCExpressionsMenu> menus = new();
            foreach (var guid in assetGuids)
            {
                menus.Add(
                    guid,
                    AssetDatabase.LoadAssetAtPath<VRCExpressionsMenu>(
                        AssetDatabase.GUIDToAssetPath(guid)
                    )
                );
            }
            foreach (var menuItem in menus)
            {
                var delFlg = true;
                if (menuItem.Value.controls.Any(p => p.parameter.name == ""))
                    continue;
                foreach (var control in menuItem.Value.controls)
                    if (!string.IsNullOrEmpty(control.parameter.name))
                        if (param.parameters.Any(p => p.name == control.parameter.name))
                        {
                            delFlg = false;
                            break;
                        }
                if (delFlg)
                    AssetDatabase.DeleteAsset(AssetDatabase.GUIDToAssetPath(menuItem.Key));
            }
            EditorUtility.SetDirty(controller);
            MarkAllMenusDirty(menu);
            EditorUtility.SetDirty(param);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            // AvatarDescriptor への適用と変更登録
            descriptor.baseAnimationLayers[4].animatorController = controller;
            descriptor.expressionsMenu = menu;
            descriptor.expressionParameters = param;
            EditorUtility.SetDirty(descriptor);

            Debug.Log("最適化を実行しました！");
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

        /// <summary>
        /// Expression Menu の複製（サブメニューも再帰的に複製）
        /// </summary>
        public static VRCExpressionsMenu DuplicateExpressionMenu(
            VRCExpressionsMenu originalMenu,
            string parentPath
        )
        {
            if (originalMenu == null)
            {
                Debug.LogError("元のExpression Menuがありません");
                return null;
            }
            // このメニュー用のフォルダを作成
            string menuFolderPath = Path.Combine(parentPath, originalMenu.name);
            if (!Directory.Exists(menuFolderPath))
            {
                Directory.CreateDirectory(menuFolderPath);
                AssetDatabase.Refresh();
            }
            // メニューの新規保存パス
            string menuAssetPath = Path.Combine(menuFolderPath, originalMenu.name + ".asset");

            VRCExpressionsMenu newMenu = AssetDatabase.LoadAssetAtPath<VRCExpressionsMenu>(
                menuAssetPath
            );
            if (newMenu != null)
            {
                return newMenu;
            }
            newMenu = Instantiate(originalMenu);
            // サブメニューの複製
            for (int i = 0; i < newMenu.controls.Count; i++)
            {
                var control = newMenu.controls[i];
                if (control.subMenu != null)
                {
                    string subMenuFolderPath = Path.Combine(menuFolderPath, control.subMenu.name);
                    VRCExpressionsMenu existingSubMenu =
                        AssetDatabase.LoadAssetAtPath<VRCExpressionsMenu>(
                            Path.Combine(subMenuFolderPath, control.subMenu.name + ".asset")
                        );
                    if (existingSubMenu == null)
                    {
                        control.subMenu = DuplicateExpressionMenu(control.subMenu, menuFolderPath);
                    }
                    else
                    {
                        control.subMenu = existingSubMenu;
                    }
                }
            }
            EditorUtility.SetDirty(newMenu);
            AssetDatabase.CreateAsset(newMenu, menuAssetPath);
            return newMenu;
        }
    }
}
#endif
