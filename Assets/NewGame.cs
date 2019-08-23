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
    public class NewGame : CharacterSaver
    {
        private void DebuterJeu()
        {
            SceneManager.LoadScene("chateau tournesol");
            var player = gameObject;
            player.transform.position = new Vector3();


        }






    }
}
    
    






