using UnityEngine;
using UnityEditor;

public class SXP_TransferTools
{
    private static readonly string[] etudiantAssetFolderNames = new string[]
    {
        "Sprites",
        "Tilesets",
        "Backgrounds",
        "Scripts",
        "Prefabs",
        "Scenes",
    };

    public static void InitializeFolders()
    {
        CreateFolder("Assets", "Etudiant");

        foreach(var name in etudiantAssetFolderNames)
        {
            CreateFolder("Assets/Etudiant", name);
        }
    }

    [MenuItem("Outils de transfert/Enregistrer le projet", false, 5)]
    public static void ExportProject()
    {
        InitializeFolders();

        var flags =
            ExportPackageOptions.Default
            | ExportPackageOptions.Recurse
            | ExportPackageOptions.Interactive;

        string savePath = EditorUtility.SaveFilePanel("Save Project Package", "", "", "unitypackage");

        string[] projectContent = new string[etudiantAssetFolderNames.Length + 1];

        projectContent[0] = "Assets/Etudiant";

        if(!string.IsNullOrEmpty(savePath))
        {
            AssetDatabase.ExportPackage(projectContent, savePath, flags);
            Debug.Log("Export");
        }
    }

    [MenuItem("Outils de transfert/Charger un projet", false, 6)]
    public static void ImportProject()
    {
        InitializeFolders();

        string path = EditorUtility.OpenFilePanel("Charger un projet", "", "unitypackage");

        if(!string.IsNullOrEmpty(path))
        {
            AssetDatabase.ImportPackage(path, true);
        }
    }

    private static void CreateFolder(string parent, string folderName)
    {
        if(!AssetDatabase.IsValidFolder(parent + "/" + folderName))
        {
            AssetDatabase.CreateFolder(parent, folderName);
        }
    }
}