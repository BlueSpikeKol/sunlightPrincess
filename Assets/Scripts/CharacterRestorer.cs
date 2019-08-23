using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using PlatformerPro;

public class CharacterRestorer : MonoBehaviour {
    private string _playerInfoFilePath;
    // Use this for initialization
    void Start () {

        StartCoroutine(RestorePosition());
    
    }
    public IEnumerator RestorePosition()
    {
        _playerInfoFilePath = Application.persistentDataPath + "/playerInfo.dat";

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = new FileStream(_playerInfoFilePath, FileMode.Open);

        PlayerData data = (PlayerData)bf.Deserialize(file);
        file.Close();
        var player = gameObject;
        player.transform.position = data.position;
        ItemManager itemManager = player.GetComponent<ItemManager>();
        data.coinsSaveData = itemManager;
        yield return null;
    }
}
