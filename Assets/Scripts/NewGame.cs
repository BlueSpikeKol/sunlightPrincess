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
    public class NewGame : MonoBehaviour
    {
        public static bool newGame;
        
        public void DebuterJeu()
        {
            SceneManager.LoadScene("chateau tournesol");
            newGame = true ;
        }


    }

}
    
    






