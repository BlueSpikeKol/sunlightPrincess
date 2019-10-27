using System;
using System.Collections;
using System.Collections.Generic;
using PlatformerPro;
using UnityEngine;

namespace Assets.Scripts
{
    public class RespawnPointPerso : RespawnPoint
    {
        public string direction;

        private string respawnPointName = String.Empty;

        public Rigidbody2D rb2d;

        private bool isMoving = false;

        readonly Dictionary<string, Vector2> directions = new Dictionary<string, Vector2>
        {
            {"left", new Vector2(-5,0)},
            {"right", new Vector2(5,0)},
            {"null", new Vector2(0,0)},
        };
        

        private float interval = 1.0f ;

        void Awake()
        {
            LevelManager.Instance.Respawned +=
                (sender, args) => { respawnPointName = args.RespawnPoint; };

            RespawnPointPerso[] respawnPoints = GameObject.FindObjectsOfType<RespawnPointPerso>();
            foreach (RespawnPointPerso r in respawnPoints)
            {
                if (r.identifier == respawnPointName)
                {
                    Debug.Log("Respawn Perso.respawnPointName: " + respawnPointName);
                    this.SetActive();
                }
            }
        }

        void Start()
        {
            Debug.Log("In Respawn Perso.Start...");

            StartCoroutine(DoMove());
        }

        private IEnumerator DoMove()
        {
            Debug.Log("In Respawn Perso.DoMove...");

            try
            {
                Debug.Log("In Respawn Perso.direction is:  " + direction);

                rb2d.velocity = directions[direction];
            }
            catch (KeyNotFoundException e)
            {
                Console.WriteLine("Direction not found was: " + direction);
            }

            while (true)
            {
                if (!isMoving)
                {
                    isMoving = true;
                    yield return new WaitForSeconds(interval);
                }
                else
                {
                    rb2d.velocity = new Vector2(0,0);
                    break;
                }
            }
        }
    }
}


