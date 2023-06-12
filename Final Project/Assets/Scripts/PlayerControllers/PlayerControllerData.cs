using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Unity.Collections.AllocatorManager;

namespace Project
{
    /// <summary>
    /// Items collected by player
    /// </summary>
    public class PlayerControllerData : MonoBehaviour
    {
        public int score;
        public GameObject bag;
        public GameObject HP;
        public GameObject blood;
        [NonSerialized] public Rigidbody2D PlayerRigid;
        [NonSerialized] public Animator animator;
        [NonSerialized] public bool onGround = false;
        [NonSerialized] public bool onRoof = false;
        [NonSerialized] public bool isTouchingWall;
        [NonSerialized] public BagManager bagManager;
        [NonSerialized] public bool facingRight;
        private CharacterHP HPscript;
        public bool isTouchingDeathSwamp;
        

        void Awake()
        {
            isTouchingDeathSwamp = false;
            bagManager = bag.GetComponent<BagManager>();
            HPscript = HP.GetComponent<CharacterHP>();
           
        }


        public void TakeDamage(int damage)
        {
            if (HP.GetComponent<Slider>().value > 0)
            {
                HP.GetComponent<Slider>().value -= damage;
                if (HP.GetComponent<Slider>().value <= 0.1f)
                {
                    animator.SetTrigger("die");
                    PlayerRigid.velocity = Vector2.zero;
                }
                else
                {
                    animator.SetTrigger("isHit");
                    if (facingRight)
                    {
                        PlayerRigid.velocity = new Vector3(-2f, 2f, 0f);
                    }
                    else
                    {
                        PlayerRigid.velocity = new Vector3(2f, 2f, 0f);
                    }
                }
                Instantiate(blood, transform.position, Quaternion.identity);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.tag == "ground" || collision.collider.tag == "paltform" || collision.collider.tag == "DieSlowly")
            {
                onGround = true;
            }

            if (collision.collider.tag == "Roof")
            {
                onRoof = true;
            }

            if (collision.collider.tag == "PickUp")
            {
                Destroy(collision.gameObject);
                score++;
            }

            if (collision.collider.tag == "Secret")
            {
                Destroy(collision.gameObject);
                score += 5;
            }

            if (collision.collider.tag == "RedOrbs")
            {
                Destroy(collision.gameObject);
                TakeDamage(5);
            }

            if (collision.collider.tag == "WhiteOrbs")
            {
                Destroy(collision.gameObject);
                HP.GetComponent<Slider>().value += 5;
            }


            if (collision.collider.tag == "ClimbableWall")
            {
                isTouchingWall = true;
            }

            if ((collision.gameObject.tag == "Weapon") || (collision.gameObject.tag == "Food")  || (collision.gameObject.tag == "MasterWeapon"))
            {
                bagManager.AddItem(new Item() { Id = 1, prefab = collision.gameObject });
            }


            // In battle scene:
            if (collision.gameObject.tag == "DieImmediately")
            {
                HP.GetComponent<Slider>().value = 0;
            }
            if (collision.gameObject.tag == "DieSlowly")
            {
                isTouchingDeathSwamp = true;
            }

            if (collision.gameObject.tag == "NPC")
            {

                HPscript.CollideThunder();
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.collider.tag == "ground" || collision.collider.tag == "paltform")
            {
                onGround = false;
            }

            if (collision.collider.tag == "Roof")
            {
                onRoof = false;
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