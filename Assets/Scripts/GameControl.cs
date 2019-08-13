using System;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using PlatformerPro;

public class GameControl : MonoBehaviour
{
    public  static GameControl control;
    private CharacterHealth _charHealth;

    private string _playerInfoFilePath;

    void Awake () {
        _playerInfoFilePath = Application.persistentDataPath + "/playerInfo.dat";

        if (control == null)
        {
            DontDestroyOnLoad(gameObject);
            control = this;
        }
        else if (control != this)
        {
            Destroy(gameObject);
        }

        _charHealth = gameObject.GetComponentInParent<CharacterHealth>();
        if (_charHealth != null)
        {
            _charHealth.Damaged += CharHealthOnDamaged;
        }


    }

    private void CharHealthOnDamaged(object sender, DamageInfoEventArgs e)
    {
        var currentHealth = ((CharacterHealth)sender).CurrentHealth;

        Save(currentHealth);                                    
    }

    public void Save(int health)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = new FileStream(_playerInfoFilePath, FileMode.Create);
        var data = new PlayerData();

        data.health = health;
        
        bf.Serialize(file, data);

        file.Close();
    }

    public void Load()
    {
        if (File.Exists(_playerInfoFilePath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = new FileStream(_playerInfoFilePath, FileMode.Open);

            PlayerData data = (PlayerData) bf.Deserialize(file);
            file.Close();

            if (data.health == 0)
            {
                _charHealth.ResetSaveData();
            }
            else
            {
                _charHealth.ApplySaveData(data.AsSaveData());
            }        
        }
    }
}

[Serializable]
public class PlayerData
{
    public int health;
    public int lives;

    public object AsSaveData()
    {
            return new int[] { lives, health };
    }
}
