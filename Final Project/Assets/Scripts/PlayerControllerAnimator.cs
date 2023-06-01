using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    /// <summary>
    /// keep in tack of player's movement & animator
    /// </summary>
    public class PlayerControllerAnimator : MonoBehaviour
    {
        Rigidbody2D PlayerRigid;

        // run animation
        public float currentSpeed = 5.0f;
        private float acceleration = 0.03f;
        private float maxSpeed = 6.5f;
        public Vector3 movementDirection;

        // fall animation
        private bool onGround = true;
        public Vector2 minPosition;
        public Vector2 maxPosition;
        private Animator animator;

        void Start()
        {
            PlayerRigid = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            moving();
            jump();

            if (PlayerRigid.velocity.y < 0 && !onGround)
            {
                UnityEngine.Debug.Log(1);
                animator.SetBool("isFall", true);
            }
            else if (PlayerRigid.velocity.y == 0 || onGround)
            {
                UnityEngine.Debug.Log(2);
                animator.SetBool("isFall", false);
            }

        }
        void jump()
        {
            PlayerRigid.velocity = (Input.GetKey(KeyCode.Space) && onGround) ? Vector2.up * 15f : PlayerRigid.velocity;
        }

        /// <summary>
        /// speed changing 
        /// </summary>
        void moving()
        {
            float horizontal = Input.GetAxis("Horizontal");

            if (onGround)
            {
                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) // moving right straight
                {
                    currentSpeed = (currentSpeed <= maxSpeed) ? currentSpeed + acceleration : maxSpeed;
                }
                else if (!Input.GetKey(KeyCode.A) || !Input.GetKey(KeyCode.D)) // moving left straight
                {
                    currentSpeed = (currentSpeed <= maxSpeed) ? maxSpeed : currentSpeed - acceleration;
                }
            }

            movementDirection = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);
            PlayerRigid.velocity = new Vector2(horizontal * currentSpeed, PlayerRigid.velocity.y);
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, minPosition.x, maxPosition.x), transform.position.y, transform.position.z);
        }

        /// <summary>
        /// check if on ground
        /// </summary>
        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.tag == "ground" || collision.collider.tag == "paltform")
            {
                onGround = true;
            }
        }

        void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.collider.tag == "ground" || collision.collider.tag == "paltform")
            {
                onGround = false;
            }
        }
    }
}