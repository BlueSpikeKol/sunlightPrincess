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

        private Vector2 movement;

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
                    var direction = r.direction;
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
            while (true)
            {
                movement = new Vector2 {x = 0};

                if (direction == left)
                {
                    movement.x = -5;
                }
                else if (direction == right)
                {
                    movement.x = 5;
                }

                if (!isMoving)
                {
                    rb2d.velocity = movement;
                    isMoving = true;
                    yield return new WaitForSeconds(interval);
                }
                else
                {
                    movement.x = 0.0f;
                    rb2d.velocity = movement;
                    break;
                }
            }
        }
    }
}


