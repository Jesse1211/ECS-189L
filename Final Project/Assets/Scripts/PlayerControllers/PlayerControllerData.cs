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
        
        void Awake()
        {
            collected = false;
            bagManager = bag.GetComponent<BagManager>();
        }

        void Update()
        {
            //var animator = this.gameObject.GetComponent<Animator>();
            //animator.SetFloat("Velocity", Mathf.Abs(this.gameObject.GetComponent<Rigidbody2D>().velocity.x / 5.0f));
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.tag == "ground" || collision.collider.tag == "paltform")
            {
                onGround = true;
            }

            if (collision.collider.tag == "Wall")
            {
                isTouchingWall = true;
            }

            if (collision.gameObject.tag == "PickUp")
            {
                bagManager.AddItem(new Item() { Id = 1, prefab = collision.gameObject });
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
        }


        public GameObject LaunchWeapon(int index)
            => bagManager.LaunchWeapon(index);
    }
}