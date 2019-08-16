using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class restoreSave : MonoBehaviour
{
    private string _playerInfoFilePath;

    public void Load()
    {
        _playerInfoFilePath = Application.persistentDataPath + "/playerInfo.dat";


        if (File.Exists(_playerInfoFilePath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = new FileStream(_playerInfoFilePath, FileMode.Open);

            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();
            SceneManager.LoadScene(data.scene);
            //todo: play animation continue game


        }
    }
}
