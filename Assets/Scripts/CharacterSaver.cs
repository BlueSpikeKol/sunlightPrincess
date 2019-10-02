using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Assets.Scripts.Helpers;
using PlatformerPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    // 
    public class CharacterSaver : MonoBehaviour
    {
        public GameObject player;

        private string _playerInfoFilePath;

        public void Start()
        {
            _playerInfoFilePath = Application.persistentDataPath + "/playerInfo.dat";

            SavePlayerData();
        }

        protected void SavePlayerData()
        {
            PlayerData data = new PlayerData();

            SetHealth(data);
            SetCoins(data);
            SetScene(data);
            SetPosition(data);

            SaveToDisk(data);
        }

        private void SetPosition(PlayerData data)
        {
            data.position = player.transform.position;
        }

        private void SetScene(PlayerData data)
        {
            data.scene = SceneManager.GetActiveScene().name;
        }

        private void SetCoins(PlayerData data)
        {
            ItemManager itemManager = player.GetComponent<ItemManager>();
            data.coinsSaveData = itemManager.SaveData;
        }

        private void SetHealth(PlayerData data)
        {
            if (player == null)
            {
                Debug.Log("Player n'a pas été assigné dans le component CharacterSaver dans Checkpoint");
                return;
            }

            CharacterHealth charHealth = player.GetComponent<CharacterHealth>();
            data.healthSaveData = charHealth.SaveData;
        }

        private void SaveToDisk(PlayerData data)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = new FileStream(_playerInfoFilePath, FileMode.Create);

            bf.Serialize(file, data);

            file.Close();

            Debug.Log("Saved to disk: " + _playerInfoFilePath );
        }

        
            

         
    }

    [Serializable]
    public class PlayerData
    {
        public object healthSaveData;
        public object coinsSaveData;
        public string scene;
        public SerializableVector3 position;
    }
}