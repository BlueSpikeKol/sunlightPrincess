using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Assets.Scripts.Helpers;
using PlatformerPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Scripts;

public class Introduction : MonoBehaviour {

    public GameObject player;

	// Use this for initialization
	void Start ()
    {
       if (NewGame.newGame == true)
       {

          player.transform.position = new Vector3(-13.45312f, 10.5f);
          NewGame.newGame = false;
       }

    }
}
