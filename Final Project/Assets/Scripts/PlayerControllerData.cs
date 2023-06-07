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
        [SerializeField] private PlayerControllerAnimator PlayerControllerAnimator;
        public bool collected;
        public GameObject bag;
        private BagManager bagManager;

        void Awake()
        {
            collected = false;
            health = 100;
            bagManager = bag.GetComponent<BagManager>();
        }

        void Update()
        {
            //var animator = this.gameObject.GetComponent<Animator>();
            //animator.SetFloat("Velocity", Mathf.Abs(this.gameObject.GetComponent<Rigidbody2D>().velocity.x / 5.0f));
        }

        // picking up the green balls and increments scores.
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "PickUp")
            {
                //collision.gameObject.SetActive(false);
                //BagDataLoader.AddBagItems(new Item() { Id = collision.gameObject.tag, prefab = collision.gameObject });
                bagManager.GetData(new Item() {Id = 1, prefab = collision.gameObject});
            }

            // if (collision.gameObject.tag == "PickUp1")
            // {
            //     Destroy(collision.gameObject);
                
            // }
        }

        public Vector3 GetMovementDirection()
            => PlayerControllerAnimator.movementDirection;

        public float GetCurrentSpeed()
            => PlayerControllerAnimator.currentSpeed;
    }
}