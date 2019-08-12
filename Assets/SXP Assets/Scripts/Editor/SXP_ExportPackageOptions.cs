using UnityEngine;
using UnityEditor;

public class SXP_ExportPackageOptions : Editor
{
    [MenuItem("Assets/Export Whole Project")]
    public static void ExportProject()
    {
        var flags = 
            ExportPackageOptions.Default 
            | ExportPackageOptions.Recurse  
            | ExportPackageOptions.Interactive;

        string savePath = EditorUtility.SaveFilePanel("Save Project Package", "", "", "unitypackage");
        string[] projectContent = new string[]
        {
            "Assets",
            "ProjectSettings/AudioManager.asset",
            "ProjectSettings/EditorBuildSettings.asset",
            "ProjectSettings/EditorSettings.asset",
            "ProjectSettings/GraphicsSettings.asset",
            "ProjectSettings/InputManager.asset",
            "ProjectSettings/Physics2DSettings.asset",
            "ProjectSettings/ProjectSettings.asset",
            "ProjectSettings/QualitySettings.asset",
            "ProjectSettings/TagManager.asset",
            "ProjectSettings/TimeManager.asset"
        };

        if (!string.IsNullOrEmpty(savePath))
        {
            AssetDatabase.ExportPackage(projectContent, savePath, flags);
            Debug.Log("Export");
        }
    }
}
