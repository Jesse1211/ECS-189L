using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    /// <summary>
    /// Items collected by player
    /// </summary>
    public class PlayerControllerData : MonoBehaviour
    {
        public int score;
        public float health;
        public bool collected;
        public GameObject bag;
        
        [NonSerialized] public bool onGround = true;
        [NonSerialized] public bool isTouchingWall;
        private BagManager bagManager;
        public bool isTouchingDeathSwamp;

        void Awake()
        {
            isTouchingDeathSwamp = false;
            health = 100f;
            collected = false;
            bagManager = bag.GetComponent<BagManager>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.tag == "ground" || collision.collider.tag == "paltform" || collision.collider.tag == "DieSlowly")
            {
                onGround = true;
            }

            if (collision.collider.tag == "ClimbableWall")
            {
                isTouchingWall = true;
            }

            if (collision.gameObject.tag == "PickUp")
            {
                bagManager.AddItem(new Item() { Id = 1, prefab = collision.gameObject });
            }


            // In battle scene:
            if (collision.gameObject.tag == "DieImmediately") 
            {
                health = 0;
            }
            if (collision.gameObject.tag == "DieSlowly")
            {
                isTouchingDeathSwamp = true;
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.collider.tag == "ground" || collision.collider.tag == "paltform")
            {
                onGround = false;
            }

            if (collision.collider.tag == "Wall")
            {
                isTouchingWall = false;
            }

            if (collision.gameObject.tag == "DieSlowly")
            {
                isTouchingDeathSwamp = false;
            }
        }

        public GameObject LaunchWeapon(int index)
            => bagManager.LaunchWeapon(index);
    }
}