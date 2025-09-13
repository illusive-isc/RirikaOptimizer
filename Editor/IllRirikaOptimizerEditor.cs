using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using VRC.SDK3.Avatars.Components;
#if AVATAR_OPTIMIZER_FOUND
using Anatawa12.AvatarOptimizer;
#endif
namespace jp.illusive_isc.RirikaOptimizer
{
    [CustomEditor(typeof(IllRirikaOptimizer))]
    [AddComponentMenu("")]
    internal class IllRirikaOptimizerEditor : Editor
    {
        SerializedProperty colorFlg0;
        SerializedProperty colorFlg1;
        SerializedProperty colorFlg2;
        SerializedProperty ClothFlg0;
        SerializedProperty ClothFlg;
        SerializedProperty ClothFlg1;
        SerializedProperty ClothFlg2;
        SerializedProperty ClothFlg3;
        SerializedProperty ClothFlg4;
        SerializedProperty ClothFlg5;
        SerializedProperty ClothFlg6;
        SerializedProperty ClothFlg7;
        SerializedProperty ClothFlg8;
        SerializedProperty ClothFlg9;
        SerializedProperty ClothFlg10;
        SerializedProperty heelFlg1;
        SerializedProperty heelFlg2;
        SerializedProperty AccessoryFlg0;
        SerializedProperty AccessoryFlg1;
        SerializedProperty AccessoryFlg2;
        SerializedProperty AccessoryFlg3;
        SerializedProperty AccessoryFlg4;
        SerializedProperty HairFlg0;
        SerializedProperty HairFlg1;
        SerializedProperty HairFlg2;
        SerializedProperty HairFlg3;
        SerializedProperty HairFlg4;
        SerializedProperty HairFlg5;
        SerializedProperty HairFlg6;
        SerializedProperty HairFlg7;
        SerializedProperty HairFlg8;
        SerializedProperty HairFlg;
        SerializedProperty petScale;
        SerializedProperty petFlg;
        SerializedProperty TPSFlg;
        SerializedProperty ClairvoyanceFlg;
        SerializedProperty phoneFlg;
        SerializedProperty phoneFlg1;
        SerializedProperty colliderJumpFlg;
        SerializedProperty BreastSizeFlg;
        SerializedProperty BreastSizeFlg2;
        SerializedProperty backlightFlg;
        SerializedProperty WhiteBreathFlg;
        SerializedProperty eightBitFlg;
        SerializedProperty PenCtrlFlg;
        SerializedProperty HeartGunFlg;

        // SerializedProperty FaceGestureFlg;
        SerializedProperty FaceLockFlg;
        SerializedProperty FaceValFlg;
        SerializedProperty blinkFlg;
        SerializedProperty kamitukiFlg;
        SerializedProperty nadeFlg;
        SerializedProperty candyFlg;
        SerializedProperty drinkFlg;
        SerializedProperty doughnutFlg;
        SerializedProperty gamFlg;
        SerializedProperty teppekiFlg;
        SerializedProperty handHeartFlg;
        SerializedProperty noisepanelFlg;
        SerializedProperty neonFlg;
        SerializedProperty mesugakiFaceFlg;
        SerializedProperty mesugakiFaceFlg1;

        SerializedProperty controller;
        SerializedProperty menu;
        SerializedProperty param;
        SerializedProperty controllerDef;
        SerializedProperty menuDef;
        SerializedProperty paramDef;
        SerializedProperty IKUSIA_emote;
        SerializedProperty questFlg1;
        bool questArea;
        SerializedProperty Butt;
        SerializedProperty Breast;
        SerializedProperty acce_wing;
        SerializedProperty earring;
        SerializedProperty Leg_acce;
        SerializedProperty bob;
        SerializedProperty bobtwin;
        SerializedProperty front_root;
        SerializedProperty twintail;
        SerializedProperty stomach;
        SerializedProperty side_root;
        SerializedProperty frill;
        SerializedProperty ribbon;
        SerializedProperty bag;
        SerializedProperty nuigurumi;
        SerializedProperty long_hair;
        SerializedProperty Cloth;
        SerializedProperty tail;
        SerializedProperty bag_wing;
        SerializedProperty bag_ribbon;

        SerializedProperty upperArm_collider1;
        SerializedProperty upperArm_collider2;
        SerializedProperty upperArm_collider3;
        SerializedProperty upperArm_collider4;
        SerializedProperty upperArm_collider5;
        SerializedProperty upperArm_collider6;
        SerializedProperty upperArm_collider7;

        SerializedProperty chest_collider1;
        SerializedProperty chest_collider2;

        SerializedProperty hip_collider1;
        SerializedProperty hip_collider2;
        SerializedProperty hip_collider3;

        SerializedProperty plane_collider;
        SerializedProperty upperleg_collider1;
        SerializedProperty upperleg_collider2;
        SerializedProperty upperleg_collider3;
        SerializedProperty textureResize;
        SerializedProperty AAORemoveFlg;

        // PB情報とコライダー情報のクラス定義（namespace内、Editorクラス外に移動）
        public class PhysBoneInfo
        {
            public int AffectedCount; //:Transform数
            public int Count; //:Transform数
            public int ColliderCount; //:Collider数
            public int[] ColliderCounts; //:Collider数
        }

        public static readonly Dictionary<string, PhysBoneInfo> physBoneList = new()
        {
            {
                "Butt",
                new PhysBoneInfo { AffectedCount = 4 }
            },
            {
                "stomach",
                new PhysBoneInfo { AffectedCount = 2 }
            },
            {
                "Breast",
                new PhysBoneInfo { AffectedCount = 6, ColliderCount = 8 }
            },
            {
                "acce_wing",
                new PhysBoneInfo { AffectedCount = 11 }
            },
            {
                "earring",
                new PhysBoneInfo { AffectedCount = 11 }
            },
            {
                "Leg_acce",
                new PhysBoneInfo { AffectedCount = 2 }
            },
            {
                "bob",
                new PhysBoneInfo { AffectedCount = 27, ColliderCount = 34 }
            },
            {
                "bobtwin",
                new PhysBoneInfo { AffectedCount = 8, ColliderCount = 6 }
            },
            {
                "front_root",
                new PhysBoneInfo { AffectedCount = 13 }
            },
            {
                "twintail",
                new PhysBoneInfo { AffectedCount = 9, ColliderCounts = new int[] { 12, 6 } }
            },
            {
                "side_root",
                new PhysBoneInfo { AffectedCount = 11, ColliderCount = 16 }
            },
            {
                "ribbon",
                new PhysBoneInfo { AffectedCount = 9 }
            },
            {
                "frill",
                new PhysBoneInfo { AffectedCount = 4, ColliderCount = 2 }
            },
            {
                "bag",
                new PhysBoneInfo { AffectedCount = 3 }
            },
            {
                "nuigurumi",
                new PhysBoneInfo { AffectedCount = 4 }
            },
            {
                "long_hair",
                new PhysBoneInfo
                {
                    AffectedCount = 39,
                    ColliderCounts = new int[] { 54, 23, 46, 23 },
                }
            },
            {
                "tail",
                new PhysBoneInfo { AffectedCount = 15, ColliderCounts = new int[] { 14, 28, 14 } }
            },
            {
                "bag_wing",
                new PhysBoneInfo { AffectedCount = 14, ColliderCount = 14 }
            },
            {
                "bag_ribbon",
                new PhysBoneInfo { AffectedCount = 6, ColliderCount = 14 }
            },
            {
                "Cloth",
                new PhysBoneInfo { AffectedCount = 49, ColliderCounts = new int[] { 72, 36 } }
            },
        };

        private void OnEnable()
        {
            colorFlg0 = serializedObject.FindProperty("colorFlg0");
            colorFlg1 = serializedObject.FindProperty("colorFlg1");
            colorFlg2 = serializedObject.FindProperty("colorFlg2");
            heelFlg1 = serializedObject.FindProperty("heelFlg1");
            heelFlg2 = serializedObject.FindProperty("heelFlg2");
            ClothFlg0 = serializedObject.FindProperty("ClothFlg0");
            ClothFlg = serializedObject.FindProperty("ClothFlg");
            ClothFlg1 = serializedObject.FindProperty("ClothFlg1");
            ClothFlg2 = serializedObject.FindProperty("ClothFlg2");
            ClothFlg3 = serializedObject.FindProperty("ClothFlg3");
            ClothFlg4 = serializedObject.FindProperty("ClothFlg4");
            ClothFlg5 = serializedObject.FindProperty("ClothFlg5");
            ClothFlg6 = serializedObject.FindProperty("ClothFlg6");
            ClothFlg7 = serializedObject.FindProperty("ClothFlg7");
            ClothFlg8 = serializedObject.FindProperty("ClothFlg8");
            ClothFlg9 = serializedObject.FindProperty("ClothFlg9");
            ClothFlg10 = serializedObject.FindProperty("ClothFlg10");
            AccessoryFlg0 = serializedObject.FindProperty("AccessoryFlg0");
            AccessoryFlg1 = serializedObject.FindProperty("AccessoryFlg1");
            AccessoryFlg2 = serializedObject.FindProperty("AccessoryFlg2");
            AccessoryFlg3 = serializedObject.FindProperty("AccessoryFlg3");
            AccessoryFlg4 = serializedObject.FindProperty("AccessoryFlg4");
            HairFlg0 = serializedObject.FindProperty("HairFlg0");
            HairFlg1 = serializedObject.FindProperty("HairFlg1");
            HairFlg2 = serializedObject.FindProperty("HairFlg2");
            HairFlg3 = serializedObject.FindProperty("HairFlg3");
            HairFlg4 = serializedObject.FindProperty("HairFlg4");
            HairFlg5 = serializedObject.FindProperty("HairFlg5");
            HairFlg6 = serializedObject.FindProperty("HairFlg6");
            HairFlg7 = serializedObject.FindProperty("HairFlg7");
            HairFlg8 = serializedObject.FindProperty("HairFlg8");
            HairFlg = serializedObject.FindProperty("HairFlg");
            petScale = serializedObject.FindProperty("petScale");
            petFlg = serializedObject.FindProperty("petFlg");
            TPSFlg = serializedObject.FindProperty("TPSFlg");
            ClairvoyanceFlg = serializedObject.FindProperty("ClairvoyanceFlg");
            phoneFlg = serializedObject.FindProperty("phoneFlg");
            phoneFlg1 = serializedObject.FindProperty("phoneFlg1");
            colliderJumpFlg = serializedObject.FindProperty("colliderJumpFlg");
            BreastSizeFlg = serializedObject.FindProperty("BreastSizeFlg");
            BreastSizeFlg2 = serializedObject.FindProperty("BreastSizeFlg2");
            backlightFlg = serializedObject.FindProperty("backlightFlg");
            WhiteBreathFlg = serializedObject.FindProperty("WhiteBreathFlg");
            eightBitFlg = serializedObject.FindProperty("eightBitFlg");
            PenCtrlFlg = serializedObject.FindProperty("PenCtrlFlg");
            HeartGunFlg = serializedObject.FindProperty("HeartGunFlg");
            // FaceGestureFlg = serializedObject.FindProperty("FaceGestureFlg");
            FaceLockFlg = serializedObject.FindProperty("FaceLockFlg");
            FaceValFlg = serializedObject.FindProperty("FaceValFlg");
            blinkFlg = serializedObject.FindProperty("blinkFlg");
            kamitukiFlg = serializedObject.FindProperty("kamitukiFlg");
            nadeFlg = serializedObject.FindProperty("nadeFlg");
            candyFlg = serializedObject.FindProperty("candyFlg");
            drinkFlg = serializedObject.FindProperty("drinkFlg");
            doughnutFlg = serializedObject.FindProperty("doughnutFlg");
            gamFlg = serializedObject.FindProperty("gamFlg");
            teppekiFlg = serializedObject.FindProperty("teppekiFlg");
            handHeartFlg = serializedObject.FindProperty("handHeartFlg");
            noisepanelFlg = serializedObject.FindProperty("noisepanelFlg");
            neonFlg = serializedObject.FindProperty("neonFlg");
            mesugakiFaceFlg = serializedObject.FindProperty("mesugakiFaceFlg");
            mesugakiFaceFlg1 = serializedObject.FindProperty("mesugakiFaceFlg1");

            controller = serializedObject.FindProperty("controller");
            menu = serializedObject.FindProperty("menu");
            param = serializedObject.FindProperty("param");
            controllerDef = serializedObject.FindProperty("controllerDef");
            menuDef = serializedObject.FindProperty("menuDef");
            paramDef = serializedObject.FindProperty("paramDef");
            IKUSIA_emote = serializedObject.FindProperty("IKUSIA_emote");

            questFlg1 = serializedObject.FindProperty("questFlg1");
            Butt = serializedObject.FindProperty("Butt");
            Breast = serializedObject.FindProperty("Breast");
            acce_wing = serializedObject.FindProperty("acce_wing");
            earring = serializedObject.FindProperty("earring");
            Leg_acce = serializedObject.FindProperty("Leg_acce");
            bob = serializedObject.FindProperty("bob");
            bobtwin = serializedObject.FindProperty("bobtwin");
            front_root = serializedObject.FindProperty("front_root");
            twintail = serializedObject.FindProperty("twintail");
            stomach = serializedObject.FindProperty("stomach");
            side_root = serializedObject.FindProperty("side_root");
            ribbon = serializedObject.FindProperty("ribbon");
            frill = serializedObject.FindProperty("frill");
            bag = serializedObject.FindProperty("bag");
            nuigurumi = serializedObject.FindProperty("nuigurumi");
            long_hair = serializedObject.FindProperty("long_hair");
            tail = serializedObject.FindProperty("tail");
            bag_wing = serializedObject.FindProperty("bag_wing");
            bag_ribbon = serializedObject.FindProperty("bag_ribbon");
            Cloth = serializedObject.FindProperty("Cloth");

            upperArm_collider1 = serializedObject.FindProperty("upperArm_collider1");
            upperArm_collider2 = serializedObject.FindProperty("upperArm_collider2");
            upperArm_collider3 = serializedObject.FindProperty("upperArm_collider3");
            upperArm_collider4 = serializedObject.FindProperty("upperArm_collider4");
            upperArm_collider5 = serializedObject.FindProperty("upperArm_collider5");
            upperArm_collider6 = serializedObject.FindProperty("upperArm_collider6");
            upperArm_collider7 = serializedObject.FindProperty("upperArm_collider7");

            chest_collider1 = serializedObject.FindProperty("chest_collider1");
            chest_collider2 = serializedObject.FindProperty("chest_collider2");

            hip_collider1 = serializedObject.FindProperty("hip_collider1");
            hip_collider2 = serializedObject.FindProperty("hip_collider2");
            hip_collider3 = serializedObject.FindProperty("hip_collider3");

            upperleg_collider1 = serializedObject.FindProperty("upperleg_collider1");
            upperleg_collider2 = serializedObject.FindProperty("upperleg_collider2");
            upperleg_collider3 = serializedObject.FindProperty("upperleg_collider3");

            plane_collider = serializedObject.FindProperty("plane_collider");

            textureResize = serializedObject.FindProperty("textureResize");
            AAORemoveFlg = serializedObject.FindProperty("AAORemoveFlg");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical();
            EditorGUILayout.PropertyField(heelFlg1, new GUIContent("ヒールON"));
            EditorGUILayout.PropertyField(heelFlg2, new GUIContent("ハイヒールON"));
            if (
                AssetDatabase.LoadAssetAtPath<Object>(
                    AssetDatabase.GUIDToAssetPath("1270af4956044a14db0b58aa4fd2832b")
                ) == null
            )
                GUI.enabled = false;
            EditorGUILayout.PropertyField(
                colorFlg0,
                new GUIContent("(追加アセット) 2Pカラー置き換え")
            );
            EditorGUILayout.PropertyField(
                colorFlg1,
                new GUIContent("(追加アセット) 黒髪カラー置き換え")
            );
            EditorGUILayout.PropertyField(colorFlg2, new GUIContent("デフォルトカラー置き換え"));
            {
                var RirikaOptimizer = (IllRirikaOptimizer)target;
                if (
                    colorFlg0.boolValue != RirikaOptimizer.colorFlg0
                    || colorFlg1.boolValue != RirikaOptimizer.colorFlg1
                )
                {
                    colorFlg2.boolValue = false;
                }
                else if (colorFlg2.boolValue != RirikaOptimizer.colorFlg2)
                {
                    colorFlg0.boolValue = false;
                    colorFlg1.boolValue = false;
                }
            }
            GUI.enabled = true;
            EditorGUILayout.PropertyField(ClothFlg0, new GUIContent("衣装メニューのみ削除"));
            if (!ClothFlg0.boolValue)
            {
                GUI.enabled = false;
                ClothFlg.boolValue = false;
                ClothFlg1.boolValue = false;
                ClothFlg2.boolValue = false;
                ClothFlg3.boolValue = false;
                ClothFlg4.boolValue = false;
                ClothFlg5.boolValue = false;
                ClothFlg6.boolValue = false;
                ClothFlg7.boolValue = false;
                ClothFlg8.boolValue = false;
            }

            EditorGUILayout.PropertyField(ClothFlg1, new GUIContent("  ├ アウター削除"));
            EditorGUILayout.PropertyField(ClothFlg2, new GUIContent("  ├ バッグ削除"));
            EditorGUILayout.PropertyField(ClothFlg3, new GUIContent("  ├ スリーブ削除"));
            EditorGUILayout.PropertyField(ClothFlg4, new GUIContent("  ├ 尻尾削除"));
            EditorGUILayout.PropertyField(ClothFlg5, new GUIContent("  ├ アームカバー削除"));
            EditorGUILayout.PropertyField(ClothFlg6, new GUIContent("  ├ 服削除"));
            EditorGUILayout.PropertyField(ClothFlg7, new GUIContent("  ├ ニーソックス削除"));
            EditorGUILayout.PropertyField(ClothFlg8, new GUIContent("  ├ 靴削除"));
            EditorGUILayout.PropertyField(ClothFlg10, new GUIContent("  ├ 差分衣装追加"));
            EditorGUILayout.PropertyField(ClothFlg, new GUIContent("  └ デフォ衣装すべて削除"));
            if (!ClothFlg.boolValue)
            {
                GUI.enabled = false;
                ClothFlg9.boolValue = false;
            }
            else
            {
                ClothFlg1.boolValue = true;
                ClothFlg2.boolValue = true;
                ClothFlg3.boolValue = true;
                ClothFlg4.boolValue = true;
                ClothFlg5.boolValue = true;
                ClothFlg6.boolValue = true;
                ClothFlg7.boolValue = true;
                ClothFlg8.boolValue = true;
            }

            EditorGUILayout.PropertyField(ClothFlg9, new GUIContent("      └ 下着も削除"));
            GUI.enabled = true;

            EditorGUILayout.PropertyField(AccessoryFlg0, new GUIContent("アクセメニューのみ削除"));
            if (!AccessoryFlg0.boolValue)
            {
                GUI.enabled = false;
                AccessoryFlg1.boolValue = false;
                AccessoryFlg2.boolValue = false;
                AccessoryFlg3.boolValue = false;
                AccessoryFlg4.boolValue = false;
            }
            EditorGUILayout.PropertyField(AccessoryFlg1, new GUIContent("  ├ 髪留めON"));
            EditorGUILayout.PropertyField(AccessoryFlg2, new GUIContent("  ├ 頭羽ON"));
            EditorGUILayout.PropertyField(AccessoryFlg3, new GUIContent("  ├ チョーカーON"));
            EditorGUILayout.PropertyField(AccessoryFlg4, new GUIContent("  └ レッグベルトON"));
            GUI.enabled = true;

            EditorGUILayout.PropertyField(HairFlg0, new GUIContent("髪毛メニューのみ削除"));
            if (!HairFlg0.boolValue)
            {
                GUI.enabled = false;
                HairFlg1.boolValue = false;
                HairFlg2.boolValue = false;
                HairFlg.boolValue = false;
            }
            EditorGUILayout.PropertyField(HairFlg1, new GUIContent("  │   ├ 前髪ショートON"));
            EditorGUILayout.PropertyField(HairFlg2, new GUIContent("  │   ├ 前髪サイドON"));
            EditorGUILayout.PropertyField(HairFlg3, new GUIContent("  │   └ ボブツインON"));
            EditorGUILayout.PropertyField(HairFlg4, new GUIContent("  ├ ロングON"));
            EditorGUILayout.PropertyField(HairFlg7, new GUIContent("  ├ ツインテON"));

            if (
                AssetDatabase.LoadAssetAtPath<Object>(
                    AssetDatabase.GUIDToAssetPath("b0fb802c479d39448bd81534f28ae96c")
                ) == null
            )
                GUI.enabled = false;
            EditorGUILayout.PropertyField(
                HairFlg8,
                new GUIContent("  ├ (追加アセット) ロングツインテON")
            );
            if (HairFlg0.boolValue)
                GUI.enabled = true;
            EditorGUILayout.PropertyField(HairFlg5, new GUIContent("  ├ ボブON"));
            if (!HairFlg5.boolValue)
            {
                GUI.enabled = false;
                HairFlg6.boolValue = false;
            }
            EditorGUILayout.PropertyField(HairFlg6, new GUIContent("  │   └ ショートON"));
            if (HairFlg0.boolValue)
                GUI.enabled = true;
            {
                var RirikaOptimizer = (IllRirikaOptimizer)target;
                if (HairFlg4.boolValue != RirikaOptimizer.HairFlg4)
                {
                    HairFlg5.boolValue = false;
                    HairFlg7.boolValue = false;
                    HairFlg8.boolValue = false;
                }
                else if (HairFlg5.boolValue != RirikaOptimizer.HairFlg5)
                {
                    HairFlg4.boolValue = false;
                    HairFlg7.boolValue = false;
                    HairFlg8.boolValue = false;
                }
                else if (HairFlg7.boolValue != RirikaOptimizer.HairFlg7)
                {
                    HairFlg4.boolValue = false;
                    HairFlg5.boolValue = false;
                    HairFlg8.boolValue = false;
                }
                else if (HairFlg8.boolValue != RirikaOptimizer.HairFlg8)
                {
                    HairFlg4.boolValue = false;
                    HairFlg5.boolValue = false;
                    HairFlg7.boolValue = false;
                }
            }
            EditorGUILayout.PropertyField(HairFlg, new GUIContent("  └ 髪毛削除"));
            GUI.enabled = true;
            EditorGUILayout.PropertyField(BreastSizeFlg, new GUIContent("バストサイズ変更削除"));
            if (!BreastSizeFlg.boolValue)
            {
                GUI.enabled = false;
                BreastSizeFlg2.boolValue = false;
            }
            EditorGUILayout.PropertyField(
                BreastSizeFlg2,
                new GUIContent("  └ BreastSize100にする")
            );
            GUI.enabled = true;
            GUILayout.EndVertical();
            GUILayout.BeginVertical();
            EditorGUILayout.PropertyField(candyFlg, new GUIContent("飴削除"));
            EditorGUILayout.PropertyField(drinkFlg, new GUIContent("ジュース削除"));
            EditorGUILayout.PropertyField(doughnutFlg, new GUIContent("ドーナツ削除"));
            EditorGUILayout.PropertyField(gamFlg, new GUIContent("ガム削除"));
            EditorGUILayout.PropertyField(teppekiFlg, new GUIContent("鉄壁削除"));
            EditorGUILayout.PropertyField(handHeartFlg, new GUIContent("ハンドハート削除"));
            EditorGUILayout.PropertyField(noisepanelFlg, new GUIContent("容疑者風削除"));
            EditorGUILayout.PropertyField(neonFlg, new GUIContent("neon削除"));
            EditorGUILayout.PropertyField(mesugakiFaceFlg, new GUIContent("メスガキフェイス削除"));
            EditorGUILayout.PropertyField(
                mesugakiFaceFlg1,
                new GUIContent("  └ パーティクルのみ削除")
            );
            if (mesugakiFaceFlg.boolValue)
            {
                mesugakiFaceFlg1.boolValue = true;
            }
            EditorGUILayout.PropertyField(petFlg, new GUIContent("Petギミック削除"));

            EditorGUILayout.PropertyField(TPSFlg, new GUIContent("TPS削除"));
            EditorGUILayout.PropertyField(ClairvoyanceFlg, new GUIContent("透視削除"));
            EditorGUILayout.PropertyField(phoneFlg, new GUIContent("スマホギミック削除"));
            EditorGUILayout.PropertyField(
                phoneFlg1,
                new GUIContent("  └ ライトと撮影ギミック削除")
            );
            if (phoneFlg.boolValue)
            {
                phoneFlg1.boolValue = true;
            }
            EditorGUILayout.PropertyField(
                colliderJumpFlg,
                new GUIContent("コライダー・ジャンプ削除")
            );

            EditorGUILayout.PropertyField(backlightFlg, new GUIContent("backlight削除"));

            EditorGUILayout.PropertyField(WhiteBreathFlg, new GUIContent("ホワイトブレス削除"));
            EditorGUILayout.PropertyField(eightBitFlg, new GUIContent("8bit削除"));
            EditorGUILayout.PropertyField(PenCtrlFlg, new GUIContent("ペン操作削除"));
            EditorGUILayout.PropertyField(HeartGunFlg, new GUIContent("ハートガン削除"));
            // EditorGUILayout.PropertyField(
            //     FaceGestureFlg,
            //     new GUIContent("デフォルトの表情プリセット削除(faceEmoなど使う場合)")
            // );
            EditorGUILayout.PropertyField(FaceLockFlg, new GUIContent("FaceLock削除"));
            EditorGUILayout.PropertyField(FaceValFlg, new GUIContent("顔差分変更機能削除"));
            EditorGUILayout.PropertyField(
                blinkFlg,
                new GUIContent("まばたきをメニューから削除して常にON")
            );
            EditorGUILayout.PropertyField(
                nadeFlg,
                new GUIContent("なでギミックをメニューから削除して常にON")
            );
            EditorGUILayout.PropertyField(
                kamitukiFlg,
                new GUIContent("噛みつきをメニューから削除して常にON")
            );
            EditorGUILayout.PropertyField(
                IKUSIA_emote,
                new GUIContent("IKUSIA_emoteをメニューのみ削除")
            );
            if (!petFlg.boolValue)
                EditorGUILayout.PropertyField(petScale, new GUIContent("Pet大きさ変更(試作機能)"));

            GUILayout.EndVertical();
            GUILayout.EndHorizontal();

            questArea = EditorGUILayout.Foldout(questArea, "Quest用調整項目(素体のみ)", true);
            if (questArea)
            {
                var ririkaOptimizer = (IllRirikaOptimizer)target;
#if AVATAR_OPTIMIZER_FOUND
                if (ririkaOptimizer.transform.root.GetComponent<TraceAndOptimize>() == null)
                    EditorGUILayout.HelpBox(
                        "アバターにTraceAndOptimizeを追加してください",
                        MessageType.Error
                    );
#else
                EditorGUILayout.HelpBox(
                    "AvatarOptimizerが見つかりませんVCCに追加して有効化してください",
                    MessageType.Error
                );
#endif
                EditorGUILayout.HelpBox(
                    "Quest化に対応してないコンポーネントやシェーダーを使っているためペット、TPS、透視、コライダー・ジャンプ、ホワイトブレス、8bit、ペン操作、ハートガンのparticle、AFKの演出の一部を削除します。\n"
                        + "",
                    MessageType.Info
                );
                EditorGUILayout.PropertyField(questFlg1, new GUIContent("quest用にギミックを削除"));

                if (questFlg1.boolValue)
                {
                    serializedObject.ApplyModifiedProperties();
                    serializedObject.Update();
                    TPSFlg.boolValue = true;
                    teppekiFlg.boolValue = true;
                    mesugakiFaceFlg1.boolValue = true;
                    ClairvoyanceFlg.boolValue = true;
                    colliderJumpFlg.boolValue = true;
                    backlightFlg.boolValue = true;
                    WhiteBreathFlg.boolValue = true;
                    eightBitFlg.boolValue = true;
                    PenCtrlFlg.boolValue = true;
                    HeartGunFlg.boolValue = true;
                    candyFlg.boolValue = true;
                    neonFlg.boolValue = true;
                    HairFlg0.boolValue = true;
                    handHeartFlg.boolValue = true;
                    noisepanelFlg.boolValue = true;
                    petFlg.boolValue = true;
                    serializedObject.ApplyModifiedProperties();
                }
                if (GUILayout.Button("おすすめ設定にする"))
                {
                    serializedObject.ApplyModifiedProperties();
                    serializedObject.Update();
                    Butt.boolValue = true;
                    Breast.boolValue = false;
                    stomach.boolValue = true;

                    acce_wing.boolValue = true;
                    earring.boolValue = true;
                    Leg_acce.boolValue = true;
                    bob.boolValue = HairFlg.boolValue || !HairFlg5.boolValue;
                    bobtwin.boolValue =
                        HairFlg.boolValue || !(HairFlg3.boolValue && HairFlg5.boolValue);
                    front_root.boolValue = true;
                    side_root.boolValue = HairFlg.boolValue || HairFlg2.boolValue;
                    twintail.boolValue = HairFlg.boolValue || !HairFlg7.boolValue;
                    long_hair.boolValue = HairFlg.boolValue || !HairFlg4.boolValue;
                    Cloth.boolValue = true;
                    ribbon.boolValue = true;
                    frill.boolValue = true;
                    tail.boolValue = false;

                    bag.boolValue = true;
                    nuigurumi.boolValue = true;
                    bag_wing.boolValue = true;
                    bag_ribbon.boolValue = true;

                    upperArm_collider1.boolValue = true;
                    upperArm_collider2.boolValue = true;
                    upperArm_collider3.boolValue = true;
                    upperArm_collider4.boolValue = true;
                    upperArm_collider5.boolValue = true;
                    upperArm_collider6.boolValue = true;
                    upperArm_collider7.boolValue = true;

                    chest_collider1.boolValue = false;
                    chest_collider2.boolValue = false;

                    hip_collider1.boolValue = false;
                    hip_collider2.boolValue = false;
                    hip_collider3.boolValue = false;

                    upperleg_collider1.boolValue = true;
                    upperleg_collider2.boolValue = true;
                    upperleg_collider3.boolValue = true;

                    plane_collider.boolValue = true;

                    serializedObject.ApplyModifiedProperties();
                }

                if (questFlg1.boolValue)
                {
                    if (ClothFlg.boolValue)
                    {
                        long_hair.boolValue = true;
                        Cloth.boolValue = true;
                    }
                    if (HairFlg.boolValue)
                    {
                        Leg_acce.boolValue = true;
                        twintail.boolValue = true;
                        bob.boolValue = true;
                        bobtwin.boolValue = true;
                        stomach.boolValue = true;
                        front_root.boolValue = true;
                        side_root.boolValue = true;
                    }
                }
                PbTransform("お尻", "Butt", Butt);
                PbTransform("お腹", "stomach", stomach);
                PbTransform("胸", "Breast", Breast);
                DisplayColliderSettings(
                    Breast,
                    "Breast",
                    new Dictionary<string, SerializedProperty>()
                    {
                        { "腕干渉", upperArm_collider1 },
                    }
                );
                PbTransform("頭の羽", "acce_wing", acce_wing);
                PbTransform("イヤリング", "earring", earring);
                PbTransform("レッグベルト", "Leg_acce", Leg_acce);
                PbTransform("bob", "bob", bob);
                DisplayColliderSettings(
                    bob,
                    "bob",
                    new Dictionary<string, SerializedProperty>()
                    {
                        { "腕干渉", upperArm_collider2 },
                    }
                );
                PbTransform("bobtwin", "bobtwin", bobtwin);
                DisplayColliderSettings(
                    bobtwin,
                    "bobtwin",
                    new Dictionary<string, SerializedProperty>()
                    {
                        { "腕干渉", upperArm_collider3 },
                    }
                );

                PbTransform("前髪", "front_root", front_root);
                PbTransform("前髪サイド", "side_root", side_root);
                DisplayColliderSettings(
                    side_root,
                    "side_root",
                    new Dictionary<string, SerializedProperty>()
                    {
                        { "腕干渉", upperArm_collider4 },
                    }
                );
                PbTransform("ツインテール", "twintail", twintail);
                DisplayColliderSettings(
                    twintail,
                    "twintail",
                    new Dictionary<string, SerializedProperty>()
                    {
                        { "腕干渉", upperArm_collider5 },
                        { "胸部干渉", chest_collider1 },
                    }
                );

                PbTransform("長髪", "long_hair", long_hair);
                DisplayColliderSettings(
                    long_hair,
                    "long_hair",
                    new Dictionary<string, SerializedProperty>()
                    {
                        { "腕干渉", upperArm_collider6 },
                        { "胸部干渉", chest_collider2 },
                        { "脚干渉", upperleg_collider1 },
                        { "お尻干渉", hip_collider1 },
                    }
                );

                PbTransform("スカート", "Cloth", Cloth);
                DisplayColliderSettings(
                    Cloth,
                    "Cloth",
                    new Dictionary<string, SerializedProperty>()
                    {
                        { "脚干渉", upperleg_collider2 },
                        { "お尻干渉", hip_collider2 },
                    }
                );
                PbTransform("胸リボン", "ribbon", ribbon);
                PbTransform("肩フリル", "frill", frill);
                DisplayColliderSettings(
                    frill,
                    "frill",
                    new Dictionary<string, SerializedProperty>()
                    {
                        { "腕干渉", upperArm_collider7 },
                    }
                );
                PbTransform("尻尾", "tail", tail);
                DisplayColliderSettings(
                    tail,
                    "tail",
                    new Dictionary<string, SerializedProperty>()
                    {
                        { "地面干渉", plane_collider },
                        { "脚干渉", upperleg_collider3 },
                        { "お尻干渉", hip_collider3 },
                    }
                );
                PbTransform("バッグ", "bag", bag);
                PbTransform("バッグぬいぐるみ", "nuigurumi", nuigurumi);

                PbTransform("バッグ羽", "bag_wing", bag_wing);
                PbTransform("バッグリボン", "bag_ribbon", bag_ribbon);

                int count = 247;
                if (Butt.boolValue)
                    count -= physBoneList["Butt"].AffectedCount;
                if (Breast.boolValue)
                    count -= physBoneList["Breast"].AffectedCount;
                if (acce_wing.boolValue)
                    count -= physBoneList["acce_wing"].AffectedCount;
                if (earring.boolValue)
                    count -= physBoneList["earring"].AffectedCount;
                if (Leg_acce.boolValue)
                    count -= physBoneList["Leg_acce"].AffectedCount;
                if (bob.boolValue)
                    count -= physBoneList["bob"].AffectedCount;
                if (bobtwin.boolValue)
                    count -= physBoneList["bobtwin"].AffectedCount;
                if (front_root.boolValue)
                    count -= physBoneList["front_root"].AffectedCount;
                if (twintail.boolValue)
                    count -= physBoneList["twintail"].AffectedCount;
                if (stomach.boolValue)
                    count -= physBoneList["stomach"].AffectedCount;
                if (side_root.boolValue)
                    count -= physBoneList["side_root"].AffectedCount;
                if (ribbon.boolValue)
                    count -= physBoneList["ribbon"].AffectedCount;
                if (frill.boolValue)
                    count -= physBoneList["frill"].AffectedCount;
                if (bag.boolValue)
                    count -= physBoneList["bag"].AffectedCount;
                if (nuigurumi.boolValue)
                    count -= physBoneList["nuigurumi"].AffectedCount;
                if (long_hair.boolValue)
                    count -= physBoneList["long_hair"].AffectedCount;
                if (tail.boolValue)
                    count -= physBoneList["tail"].AffectedCount;
                if (bag_wing.boolValue)
                    count -= physBoneList["bag_wing"].AffectedCount;
                if (bag_ribbon.boolValue)
                    count -= physBoneList["bag_ribbon"].AffectedCount;
                if (Cloth.boolValue)
                    count -= physBoneList["Cloth"].AffectedCount;
                if (count > 64)
                    EditorGUILayout.HelpBox(
                        "影響transform数 :" + count + "/64 (64以下に調整してください)",
                        MessageType.Error
                    );
                else
                    EditorGUILayout.HelpBox("影響transform数 :" + count + "/64", MessageType.Info);

                int count2 = 394;
                if (upperArm_collider1.boolValue)
                    count2 -= physBoneList["Breast"].ColliderCount;
                if (upperArm_collider2.boolValue)
                    count2 -= physBoneList["bob"].ColliderCount;
                if (upperArm_collider3.boolValue)
                    count2 -= physBoneList["bobtwin"].ColliderCount;
                if (upperArm_collider4.boolValue)
                    count2 -= physBoneList["side_root"].ColliderCount;
                if (upperArm_collider5.boolValue)
                    count2 -= physBoneList["twintail"].ColliderCounts[0];
                if (upperArm_collider6.boolValue)
                    count2 -= physBoneList["long_hair"].ColliderCounts[0];
                if (chest_collider2.boolValue)
                    count2 -= physBoneList["long_hair"].ColliderCounts[1];
                if (upperleg_collider1.boolValue)
                    count2 -= physBoneList["long_hair"].ColliderCounts[2];
                if (hip_collider1.boolValue)
                    count2 -= physBoneList["long_hair"].ColliderCounts[3];
                if (upperArm_collider7.boolValue)
                    count2 -= physBoneList["frill"].ColliderCount;
                if (chest_collider1.boolValue)
                    count2 -= physBoneList["twintail"].ColliderCounts[1];
                if (upperleg_collider2.boolValue)
                    count2 -= physBoneList["Cloth"].ColliderCounts[0];
                if (hip_collider2.boolValue)
                    count2 -= physBoneList["Cloth"].ColliderCounts[1];
                if (plane_collider.boolValue)
                    count2 -= physBoneList["tail"].ColliderCounts[0];
                if (upperleg_collider3.boolValue)
                    count2 -= physBoneList["tail"].ColliderCounts[1];
                if (hip_collider3.boolValue)
                    count2 -= physBoneList["tail"].ColliderCounts[2];

                if (count2 > 64)
                    EditorGUILayout.HelpBox(
                        "コライダー干渉数 :" + count2 + "/64 (64以下に調整してください)",
                        MessageType.Error
                    );
                else
                    EditorGUILayout.HelpBox(
                        "コライダー干渉数 :" + count2 + "/64",
                        MessageType.Info
                    );
                int selected = textureResize.enumValueIndex;
                textureResize.enumValueIndex = EditorGUILayout.Popup(
                    "メニュー画像解像度設定",
                    selected,
                    new[] { "下げる", "削除" }
                );

#if !AVATAR_OPTIMIZER_FOUND
                GUI.enabled = false;
                EditorGUILayout.HelpBox(
                    "AAOがインストールされている場合のみ「頬染めを削除」が有効になります。",
                    MessageType.Info
                );
#endif
                EditorGUILayout.PropertyField(AAORemoveFlg, new GUIContent("頬染めを削除"));
                GUI.enabled = true;
            }
            // Execute ボタンの追加
            if (GUILayout.Button("Execute"))
            {
                IllRirikaOptimizer script = (IllRirikaOptimizer)target;
                VRCAvatarDescriptor descriptor =
                    script.transform.root.GetComponent<VRCAvatarDescriptor>();
                if (descriptor != null)
                {
                    try
                    {
                        script.Execute(descriptor);
                    }
                    catch (System.Exception)
                    {
                        Debug.LogWarning("変換に失敗しました。再実行します。");
                        script.Execute(descriptor);
                    }
                }
                else
                {
                    Debug.LogWarning("VRCAvatarDescriptor が見つかりません。");
                }
            }
            EditorGUILayout.Space();
            GUILayout.TextField(
                "生成する元Asset",
                new GUIStyle
                {
                    fontStyle = FontStyle.Bold,
                    fontSize = 24,
                    normal = new GUIStyleState { textColor = Color.white },
                }
            );
            GUI.enabled = false;
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(controllerDef, new GUIContent("Animator Controller"));
            EditorGUILayout.PropertyField(menuDef, new GUIContent("Expressions Menu"));
            EditorGUILayout.PropertyField(paramDef, new GUIContent("Expression Parameters"));
            GUI.enabled = true;
            EditorGUILayout.Space();
            GUILayout.TextField(
                "生成されたAsset",
                new GUIStyle
                {
                    fontStyle = FontStyle.Bold,
                    fontSize = 24,
                    normal = new GUIStyleState { textColor = Color.white },
                }
            );
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(controller, new GUIContent("Animator Controller"));
            EditorGUILayout.PropertyField(menu, new GUIContent("Expressions Menu"));
            EditorGUILayout.PropertyField(param, new GUIContent("Expression Parameters"));

            // 変更内容の適用
            serializedObject.ApplyModifiedProperties();
        }

        private void DisplayColliderSettings(
            SerializedProperty pbDelFlg,
            string pbname,
            Dictionary<string, SerializedProperty> ColliderList
        )
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(30);
            GUILayout.BeginVertical();
            if (
                physBoneList[pbname].ColliderCount != 0
                && physBoneList[pbname].ColliderCounts == null
            )
                foreach (var item in ColliderList)
                {
                    EditorGUILayout.PropertyField(
                        item.Value,
                        new GUIContent(item.Key + " : " + physBoneList[pbname].ColliderCount)
                    );
                }
            else if (physBoneList[pbname].ColliderCounts != null)
                for (int i = 0; i < ColliderList.Count; i++)
                {
                    var item = ColliderList.ElementAt(i);
                    EditorGUILayout.PropertyField(
                        item.Value,
                        new GUIContent(item.Key + " : " + physBoneList[pbname].ColliderCounts[i])
                    );
                }
            if (pbDelFlg.boolValue)
            {
                foreach (var item in ColliderList)
                {
                    item.Value.boolValue = true;
                }
            }
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
        }

        private void PbTransform(string name, string pbname, SerializedProperty property)
        {
            EditorGUILayout.PropertyField(
                property,
                new GUIContent(name + ":Transform : " + physBoneList[pbname].AffectedCount)
            );
        }

        // ▼ 右クリックメニューで作成できるようにするためのメニュー項目 ▼

        // Validate メソッドで対象が VRC アバターのルートであり、さらにアタッチされている Animator の avatar が "RirikaAvatar" であることをチェック
        [MenuItem("GameObject/illusive_tools/Create IllRirikaOptimizer Object", true)]
        private static bool ValidateCreateIllRirikaOptimizerObject(MenuCommand menuCommand)
        {
            GameObject contextGO = menuCommand.context as GameObject;
            if (contextGO == null)
            {
                contextGO = Selection.activeGameObject;
            }
            if (contextGO == null)
            {
                // どちらも null ならエラーを出すか、false を返す
                return false;
            }
            // 対象が VRCAvatarDescriptor を持っているか
            if (contextGO.GetComponent<VRCAvatarDescriptor>() == null)
                return false;
            // さらに、親に VRCAvatarDescriptor が存在しない（＝ルートである）かをチェック
            if (
                contextGO.transform.parent != null
                && contextGO.transform.parent.GetComponent<VRCAvatarDescriptor>() != null
            )
                return false;
            // Animator コンポーネントが存在し、その avatar プロパティの名前が "RirikaAvatar" であるかをチェック
            Animator animator = contextGO.GetComponent<Animator>();
            if (animator == null)
                return false;
            if (animator.avatar == null)
                return false;
            if (animator.avatar.name != "ririkaAvatar")
                return false;
            return true;
        }

        // 対象が条件を満たす場合のみ、メニュー項目が有効となる
        [MenuItem("GameObject/illusive_tools/Create IllRirikaOptimizer Object", false, 10)]
        private static void CreateIllRirikaOptimizerObject(MenuCommand menuCommand)
        {
            // 新しい GameObject を作成し、IllRirikaOptimizer コンポーネントを追加
            GameObject go = new GameObject("IllRirikaOptimizer");
            go.AddComponent<IllRirikaOptimizer>();

            // 右クリックで選択されたオブジェクト（VRCアバターのルート）の子として配置
            GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
            Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
            Selection.activeObject = go;
        }
    }
}
