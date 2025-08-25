using UnityEditor;
using UnityEngine;
using VRC.SDK3.Avatars.Components;

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

        SerializedProperty controller;
        SerializedProperty menu;
        SerializedProperty param;
        SerializedProperty controllerDef;
        SerializedProperty menuDef;
        SerializedProperty paramDef;
        SerializedProperty IKUSIA_emote;

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

            controller = serializedObject.FindProperty("controller");
            menu = serializedObject.FindProperty("menu");
            param = serializedObject.FindProperty("param");
            controllerDef = serializedObject.FindProperty("controllerDef");
            menuDef = serializedObject.FindProperty("menuDef");
            paramDef = serializedObject.FindProperty("paramDef");
            IKUSIA_emote = serializedObject.FindProperty("IKUSIA_emote");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

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
            EditorGUILayout.PropertyField(candyFlg, new GUIContent("飴削除"));
            EditorGUILayout.PropertyField(drinkFlg, new GUIContent("ジュース削除"));
            EditorGUILayout.PropertyField(doughnutFlg, new GUIContent("ドーナツ削除"));
            EditorGUILayout.PropertyField(gamFlg, new GUIContent("ガム削除"));
            EditorGUILayout.PropertyField(teppekiFlg, new GUIContent("鉄壁削除"));
            EditorGUILayout.PropertyField(handHeartFlg, new GUIContent("ハンドハート削除"));
            EditorGUILayout.PropertyField(noisepanelFlg, new GUIContent("容疑者風削除"));
            EditorGUILayout.PropertyField(neonFlg, new GUIContent("neon削除"));
            EditorGUILayout.PropertyField(mesugakiFaceFlg, new GUIContent("メスガキフェイス削除"));

            EditorGUILayout.PropertyField(petFlg, new GUIContent("Petギミック削除"));

            EditorGUILayout.PropertyField(TPSFlg, new GUIContent("TPS削除"));
            EditorGUILayout.PropertyField(ClairvoyanceFlg, new GUIContent("透視削除"));
            EditorGUILayout.PropertyField(phoneFlg, new GUIContent("スマホギミック削除"));
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
