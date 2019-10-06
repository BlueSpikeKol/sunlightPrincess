using System;
using System.Collections;
using System.Collections.Generic;
using PlatformerPro;
using UnityEngine;

public class CharacterWalkOut : MonoBehaviour {

    private string respawnPointName = String.Empty;

    // Use this for initialization
    void Awake ()
    {
        LevelManager.Instance.Respawned += 
            (sender, args) =>
            {
                Debug.Log("event respawned occurred!" + args.RespawnPoint);
                respawnPointName = args.RespawnPoint;
            };
    }

    void Start() {
        RespawnPointPerso[] respawnPoints = GameObject.FindObjectsOfType<RespawnPointPerso>();

        foreach (RespawnPointPerso r in respawnPoints)
        {
            if (r.identifier == respawnPointName)
            {
                var direction = r.direction;
                Debug.Log("direction: " + direction);

                var pinky = GameObject.FindGameObjectWithTag("Player");

                pinky.transform.right = pinky.transform.right - (Vector3.right * 20); 
            }
        }

    }
}
