using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace jp.illusive_isc.RirikaOptimizer
{
    [InitializeOnLoad]
    public static class RirikaPackageImportHandler
    {
        // チェックしたい対象の相対パス（部分一致でもOK）
        private const string TargetSubGuid1 = "ece7e10d3e8910e4da3119fd3954f108";
        private const string TargetSubGuid2 = "f6a24d4dc394e034b9b2173bfe669e7e";

        static RirikaPackageImportHandler()
        {
            EditorApplication.update += OnEditorUpdate;
        }

        private static EditorWindow importWindow;
        private static bool dialogShownForPea = false;
        private static bool dialogShownForPa = false;

        private static void OnEditorUpdate()
        {
            if (importWindow == null)
            {
                var tmp = Resources.FindObjectsOfTypeAll<EditorWindow>();
                importWindow = tmp.FirstOrDefault(w => w.GetType().Name == "PackageImport");
                dialogShownForPea = false;
                dialogShownForPa = false;
            }

            if (importWindow == null)
                return;

            var guids = GetImportGUIDList(importWindow);
            if (guids == null || guids.Length == 0)
                return;

            if (!dialogShownForPea)
            {
                var matchPea = guids.FirstOrDefault(guid => guid == TargetSubGuid1);
                if (matchPea != null)
                {
                    dialogShownForPea = true;

                    if (
                        EditorUtility.DisplayDialog(
                            "インポート確認 — pea.anim",
                            $"ピースのアニメーションを\nインポートから除外しますか？",
                            "はい、除外する",
                            "いいえ"
                        )
                    )
                        UncheckImportFile(importWindow, matchPea);
                }
            }

            if (!dialogShownForPa)
            {
                var matchPa = guids.FirstOrDefault(guid => guid == TargetSubGuid2);
                if (matchPa != null)
                {
                    dialogShownForPa = true;

                    if (
                        EditorUtility.DisplayDialog(
                            "インポート確認 — pa.anim",
                            $"パーのアニメーションを\nインポートから除外しますか？",
                            "はい、除外する",
                            "いいえ"
                        )
                    )
                        UncheckImportFile(importWindow, matchPa);
                }
            }
            GetImportGUIDList(importWindow);
        }

        private static string[] GetImportGUIDList(EditorWindow window)
        {
            var field = window
                .GetType()
                .GetField("m_ImportPackageItems", BindingFlags.NonPublic | BindingFlags.Instance);

            if (field == null)
                return null;

            if (field.GetValue(window) is not System.Collections.IEnumerable items)
                return null;

            var result = new List<string>();
            foreach (var item in items)
            {
                var guid =
                    item.GetType()
                        .GetField("guid", BindingFlags.Public | BindingFlags.Instance)
                        ?.GetValue(item) as string;
                if (!string.IsNullOrEmpty(guid))
                    result.Add(guid);
            }
            return result.ToArray();
        }

        private static void UncheckImportFile(EditorWindow window, string targetGuid)
        {
            var field = window
                .GetType()
                .GetField("m_ImportPackageItems", BindingFlags.NonPublic | BindingFlags.Instance);

            if (field == null)
                return;
            if (field.GetValue(window) is not System.Collections.IEnumerable items)
                return;

            foreach (var item in items)
            {
                if (
                    item.GetType()
                        .GetField("guid", BindingFlags.Public | BindingFlags.Instance)
                        ?.GetValue(item) as string
                    == targetGuid
                )
                {
                    item.GetType()
                        .GetField(
                            "assetChanged",
                            BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance
                        )
                        ?.SetValue(item, false);

                    window.Repaint();
                    break;
                }
            }
        }
    }
}
