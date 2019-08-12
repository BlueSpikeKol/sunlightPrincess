namespace SXP.ProjectVersion
{
    using UnityEngine;
    using UnityEditor;

    public class ProjectVersionEditor : EditorWindow
    {
        private const string EDITOR_NAME = "Version du Projet";
        private const string ASSET_NAME = "ProjectVersionAsset";
        private const float WIDTH = 200f;
        private const float HEIGHT = 100f;
        
        private static ProjectVersionEditor editor;
        private static ProjectVersionAsset asset;

        [MenuItem("Help/" + EDITOR_NAME)]
        public static void Open()
        {
            if (editor != null)
            {
                editor.Close();
            }

            editor = GetWindow<ProjectVersionEditor>(true);
            editor.titleContent = new GUIContent(EDITOR_NAME);
            editor.minSize = editor.maxSize = new Vector2(WIDTH, HEIGHT);
            
            CacheAsset();
        }

        static void CacheAsset()
        {
            asset = Resources.Load<ProjectVersionAsset>(ASSET_NAME);
        }

        void OnGUI()
        {
            if (asset != null)
            {
                GUILayout.Label("Version du Projet:");
                GUILayout.Label(asset.version);
            }
            else
            {
                GUILayout.Label("Aucune version trouvée");
            }
            
            GUILayout.Space(15f);
            GUILayout.Label("Studio XP");
        }
    }
}