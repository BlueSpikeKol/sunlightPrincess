using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class SXP_CreateNewScene : MonoBehaviour
{
    const string SINGLEPLAYER_TEMPLATE_PATH = "Assets/SXP Assets/Scenes/singleplayer_scene_template.unity";
    const string MULTIPLAYER_TEMPLATE_PATH = "Assets/SXP Assets/Scenes/multiplayer_scene_template.unity";
    const string SCENES_PATH = "Assets/Etudiant/Scenes/";
    const string SCENE_NAME_SINGLEPLAYER = "Scene 1.unity";
    const string SCENE_NAME_MULTIPLAYER = "Scene Multijoueur 1.unity";

    [MenuItem("Studio XP/Créer une scène étudiante", false, 0)]
    public static void CreateNewSingleplayerScene()
    {
        SXP_TransferTools.InitializeFolders();
        CreateNewScene(SINGLEPLAYER_TEMPLATE_PATH, SCENE_NAME_SINGLEPLAYER);
    }

    //[MenuItem("Studio XP/Créer une scène multijoueur", false, 0)]
    public static void CreateNewMultiplayerScene()
    {
        SXP_TransferTools.InitializeFolders();
        CreateNewScene(MULTIPLAYER_TEMPLATE_PATH, SCENE_NAME_MULTIPLAYER);
    }

    private static void CreateNewScene(string templatePath, string fileName)
    {
        if(!CloseCurrent()) return;

        Scene scene = EditorSceneManager.OpenScene(templatePath);
        string path = AssetDatabase.GenerateUniqueAssetPath(SCENES_PATH + fileName);
        EditorSceneManager.SaveScene(scene, path, false);

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    static bool CloseCurrent()
    {
        EditorApplication.isPaused = false;
        EditorApplication.isPlaying = false;

        return EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
    }
}
