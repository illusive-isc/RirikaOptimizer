#if SPLIT_MESH_FOUND
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace jp.illusive_isc
{
    public static class SplitMesh4BoneGroupsHelper
    {
        [MenuItem("GameObject/illusive_tools/ぬいぐるみ召喚 -IllRirikaOptimizer-", false, 0)]
        private static void SplitSelectedSMR(MenuCommand command)
        {
            GameObject selected = Selection.activeGameObject;

            GameObject prefab = LoadGameObjectFromGuid("ad44ac2e3d055034da3e6760294c8bea");
            if (prefab == null)
                return;
            SkinnedMeshRenderer smr = prefab
                .transform.Find("Bag")
                .GetComponent<SkinnedMeshRenderer>();

            SplitMesh4BoneGroups splitter = ScriptableObject.CreateInstance<SplitMesh4BoneGroups>();

            string nuigurumiPath =
                "Armature/Hips/Spine/Chest/Z_Bag_root/bag/bag.001/bag.002/bag_nuigurumi";
            var nuigurumiTransform = prefab.transform.Find(nuigurumiPath).transform;
            var nuigurumiTransform1 = nuigurumiTransform.Find("bag_nuigurumi.001").transform;
            var nuigurumiTransform2 = nuigurumiTransform1.Find("bag_nuigurumi.002").transform;
            var nuigurumiTransform3 = nuigurumiTransform2.Find("bag_nuigurumi.003").transform;
            var groups = new List<SplitMesh4BoneGroups.BoneGroup>
            {
                new()
                {
                    name = "金具",
                    bones = new() { nuigurumiTransform },
                },
                new()
                {
                    name = "ぬいぐるみ",
                    bones = new() { nuigurumiTransform1, nuigurumiTransform2, nuigurumiTransform3 },
                },
            };
            GameObject container = splitter.SplitAllGroups("ぬいぐるみ", smr, groups);

            if (container != null)
            {
                container.transform.GetChild(0).rotation = Quaternion.Euler(0, 0, 174);
                container.transform.SetParent(selected.transform, false);
                Undo.RegisterCreatedObjectUndo(container, "Split Mesh 4 Bone Groups");
                Selection.activeGameObject = container;

            }
        }

        public static GameObject LoadGameObjectFromGuid(string guid)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            if (string.IsNullOrEmpty(assetPath))
            {
                Debug.LogError("GUIDが無効です: " + guid);
                return null;
            }
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(assetPath);
            return prefab;
        }
    }
}
#endif
#endif
