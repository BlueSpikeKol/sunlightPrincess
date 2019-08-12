using Com.LuisPedroFonseca.ProCamera2D;
using PlatformerPro;
using PlatformerPro.Extras;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    public enum CharacterEnum { PINKY, YELLO, BOB_THE_BOT, BIOCOP, STARLAW, MARIO }

    // 1- Pinky
    // 2- Yello
    // 3- RobotBoy
    // 4- Biocop
    // 5- CommandBro
    // 6- Captain Xp
    // 7- Starlaw
    // 8- Mario

    static int currentCharacter = -1;

    //[MenuItem("Studio XP/Personnages/Pinky", false, 0)]
    public static void SelectPinky()
    {
        ChangeCharacter(0);
    }

    //[MenuItem("Studio XP/Personnages/Yello", false, 0)]
    public static void SelectYello()
    {
        ChangeCharacter(1);
    }

    //[MenuItem("Character Selection/RobotBoy")]
    public static void SelectRobotBoy()
    {
        ChangeCharacter(2);
    }

    /*
    [MenuItem("Character Selection/Biocop")]
    public static void SelectBiocop()
    {
        ChangeCharacter(3);
    }
    */

    //[MenuItem("Character Selection/CommandBro")]
    public static void SelectCommandBro()
    {
        ChangeCharacter(4);
    }


    static void ChangeCharacter(int _next)
    {
        if (EditorApplication.isPlaying) return;

        if (!IsAlreadyCurrent(_next))
        {
            DestroyCurrent();
            CreateNext(_next);
        }
    }

    static bool IsAlreadyCurrent(int _next)
    {
        Character player = FindObjectOfType<Character>();

        if (player != null)
        {
            switch (player.name)
            {
                case "Pinky":
                    currentCharacter = 0;
                    break;
                case "Yello":
                    currentCharacter = 1;
                    break;
                case "RobotBoy":
                    currentCharacter = 2;
                    break;
                case "Biocop":
                    currentCharacter = 3;
                    break;
                case "CommandBro":
                    currentCharacter = 4;
                    break;
            }

            return _next == currentCharacter;
        }

        return false;
    }

    static void DestroyCurrent()
    {
        Character player = FindObjectOfType<Character>();

        if (player != null)
        {
            DestroyImmediate(player.gameObject);
        }
    }

    static void CreateNext(int _next)
    {
        GameObject prefab = GetCharacterPrefab(_next);
        Transform sp = GetSpawnpoint();

        GameObject newCharacterObj = Instantiate(prefab, sp.position, Quaternion.identity);
        newCharacterObj.name = prefab.name;
        ProCamera2D cam2D = FindObjectOfType<ProCamera2D>();

        if (cam2D != null)
        {
            cam2D.RemoveAllCameraTargets();
            cam2D.AddCameraTarget(newCharacterObj.transform);
            EditorUtility.SetDirty(cam2D);
        }

        CharacterLoader charLoader = FindObjectOfType<CharacterLoader>();

        if (charLoader != null)
        {
            charLoader.character = newCharacterObj.GetComponent<Character>();
            EditorUtility.SetDirty(charLoader);
        }

        UIHealth_Icons uiHealth = FindObjectOfType<UIHealth_Icons>();

        if (uiHealth != null)
        {
            uiHealth.characterHealth = newCharacterObj.GetComponent<CharacterHealth>();
            EditorUtility.SetDirty(uiHealth);
        }

        EditorUtility.SetDirty(newCharacterObj);
        AssetDatabase.Refresh();
        AssetDatabase.SaveAssets();

#if UNITY_EDITOR
        EditorSceneManager.SaveOpenScenes();
#endif
    }

    static GameObject GetCharacterPrefab(int _next)
    {
        string prefabName = "";
        switch (_next)
        {
            case 0:
                prefabName = "Pinky";
                break;
            case 1:
                prefabName = "Yello";
                break;
            case 2:
                prefabName = "RobotBoy";
                break;
            case 3:
                prefabName = "Biocop";
                break;
            case 4:
                prefabName = "CommandBro";
                break;
        }

        Character newCharacterPrefab = Resources.Load<Character>("Characters/" + prefabName);
        return newCharacterPrefab.gameObject;
    }

    static Transform GetSpawnpoint()
    {
        return FindObjectOfType<RespawnPoint>().transform;
    }
}
#endif