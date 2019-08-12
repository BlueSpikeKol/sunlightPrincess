#if UNITY_EDITOR
namespace SXP_EDITOR
{
    using UnityEngine;
    using UnityEditor;
    using System.IO;

    public static class ScriptableObjectUtility
    {
        public static void CreateAsset<T>() where T : ScriptableObject
        {
            var asset = ScriptableObject.CreateInstance<T>();
            ProjectWindowUtil.CreateAsset(asset, "New " + typeof (T).Name + ".asset");
        }

        public static T CreateAsset<T>(string path, string name) where T : ScriptableObject
        {
            T asset = ScriptableObject.CreateInstance<T>();
            string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "/" + name + ".asset");

            if (asset == null)
            {
                Debug.Log("Could not create instance of '" + typeof(T).ToString() + "'");
                return null;
            }

            AssetDatabase.CreateAsset(asset, assetPathAndName);
            AssetDatabase.SaveAssets();
            return asset;
        }
    }
}
#endif