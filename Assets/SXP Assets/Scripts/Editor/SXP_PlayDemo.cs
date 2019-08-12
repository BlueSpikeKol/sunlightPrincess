using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public class SXP_PlayDemo : Editor
{
    //[MenuItem("Studio XP/Demo/Charger Demo", false, 0)]
    public static void LoadDemo()
    {
        CloseCurrent();
        EditorSceneManager.OpenScene("Assets/Scene Demo.unity");
    }

    //[MenuItem("Studio XP/Demo/Jouer au Demo")]
    public static void PlayDemo()
    {
        CloseCurrent();
        EditorSceneManager.OpenScene("Assets/Scene Demo.unity");
        EditorApplication.isPlaying = true;
    }

    static void CloseCurrent()
    {
        EditorApplication.isPaused = false;
        EditorApplication.isPlaying = false;

        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
    }
}
