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

        // climbing
        bool isTouchingWall;
        bool isWallSliding;
        public Transform fontcheck;
        public float wallSlidingSpeed;
        public bool wallJumping;
        public float xWallForce;
        public float yWallForce;
        public float wallJumpTime;

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
            updateAnimator();

            isWallSliding = ((isTouchingWall is true) && (!onGround) && Input.GetAxisRaw("Horizontal") != 0) ? true : false;

            if (isWallSliding)
            {
                PlayerRigid.velocity = new Vector2(PlayerRigid.velocity.x, Mathf.Clamp(PlayerRigid.velocity.y, -wallSlidingSpeed, float.MaxValue));
            }

            if (Input.GetKeyDown(KeyCode.Space) && isWallSliding)
            {
                wallJumping = true;
                Invoke("setWallJumpingToFalse", wallJumpTime);
            }

            if (wallJumping)
            {
                PlayerRigid.velocity = new Vector2(xWallForce * -Input.GetAxisRaw("Horizontal"), yWallForce);
            }


        }
        void jump()
        {
            if (Input.GetKey(KeyCode.Space) && onGround)
            {
                PlayerRigid.velocity = Vector2.up * 15f;
            }

            if (PlayerRigid.velocity.y > 0 && !onGround)
            {
                animator.SetTrigger("JumpEnter");
                animator.ResetTrigger("JumpExit");
            }
            else if (PlayerRigid.velocity.y <= 0)
            {
                animator.ResetTrigger("JumpEnter");
                animator.SetTrigger("JumpExit");
            }
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

            if (collision.collider.tag == "Wall")
            {
                isTouchingWall = true;
            }
        }

        void OnCollisionExit2D(Collision2D collision)
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

        void updateAnimator()
        {

            // set Fall
            if (PlayerRigid.velocity.y < 0 && !onGround)
            {
                animator.SetBool("IsFall", true);
            }
            else if (PlayerRigid.velocity.y == 0 || onGround)
            {
                animator.SetBool("IsFall", false);
            }

            //animator.SetFloat("Health", currentSpeed);

            // set Run
            //animator.SetFloat("Speed", currentSpeed);

            // set Transition
            if (Input.GetKeyDown(KeyCode.M))
            {
                animator.SetTrigger("transitionEnter");
                animator.ResetTrigger("transitionExit");
            }
            else if (Input.GetKeyUp(KeyCode.M))
            {
                animator.ResetTrigger("transitionEnter");
                animator.SetTrigger("transitionExit");
            }

            // set Dodge
            if (Input.GetKeyDown(KeyCode.S))
            {
                animator.SetTrigger("DodgeEnter");
                animator.ResetTrigger("DodgeExit");
            }
            else if (Input.GetKeyUp(KeyCode.S))
            {
                animator.ResetTrigger("DodgeEnter");
                animator.SetTrigger("DodgeExit");
            }

            // set Stab
            if (Input.GetKeyDown(KeyCode.N))
            {
                animator.SetTrigger("StabEnter");
                animator.ResetTrigger("StabExit");
            }
            else if (Input.GetKeyUp(KeyCode.N))
            {
                animator.ResetTrigger("StabEnter");
                animator.SetTrigger("StabExit");
            }

            // set Gunattack
            if (Input.GetKeyDown(KeyCode.B))
            {
                animator.SetTrigger("GunattackEnter");
                animator.ResetTrigger("GunattackExit");
            }
            else if (Input.GetKeyUp(KeyCode.B))
            {
                animator.ResetTrigger("GunattackEnter");
                animator.SetTrigger("GunattackExit");
            }

        }

        void setWallJumpingToFalse()
        {
            wallJumping = false;
        }
    }
}