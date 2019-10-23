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

        private bool isMoving;
        private string left="left";
        private string right="right";
        private string down="down";
        private string up="up";
        private string nul="null";

        readonly Dictionary<string, Vector2> directions = new Dictionary<string, Vector2>
        {
            {"left", new Vector2(-5,0)},
            {"right", new Vector2(5,0)},
            {"null", new Vector2(0,0)},
        };

        

        private float interval;

        void Awake()
        {
            LevelManager.Instance.Respawned +=
                (sender, args) => { respawnPointName = args.RespawnPoint; };

            interval = 1.0f;
            isMoving = false;

            RespawnPointPerso[] respawnPoints = GameObject.FindObjectsOfType<RespawnPointPerso>();
            foreach (RespawnPointPerso r in respawnPoints)
            {
                if (r.identifier == respawnPointName)
                {
                    Debug.Log("direction: " + direction);
                    this.SetActive();
                }
            }
        }

        void Start()
        {
            StartCoroutine(DoMove());
        }

        private IEnumerator DoMove()
        {
            try
            {
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


