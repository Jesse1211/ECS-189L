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
        [SerializeField] private PlayerControllerAnimator PlayerControllerAnimator;

        void Start()
        {

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
                Destroy(collision.gameObject);
                this.score++;
            }
        }

        public Vector3 GetMovementDirection()
            => PlayerControllerAnimator.movementDirection;

        public float GetCurrentSpeed()
            => PlayerControllerAnimator.currentSpeed;
    }
}