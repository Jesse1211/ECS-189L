using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Collections.AllocatorManager;
using UnityEngine.UIElements;
using System;

namespace Project
{
    /// <summary>
    /// keep in tack of player's movement & animator
    /// </summary>
    public class PlayerControllerAnimator : PlayerControllerData
    {
        private Rigidbody2D PlayerRigid;
        private Animator animator;
        public bool facingRight;
        // run animation
        public float currentSpeed = 5.0f;
        private float acceleration = 0.03f;
        private float maxSpeed = 6.5f;

        // climbing
        private bool isWallSliding;
        public Transform frontcheck;
        private float wallSlidingSpeed = 5;
        private bool wallJumping;
        private float xWallForce = 15f;
        private float yWallForce = 30f;
        private float wallJumpTime = 0.05f;

        // attack
        public GameObject bolt;
        public int damage;
        public float timeBetweenAttacks;
        public Transform shotPoint;
        private float nextAttackTime;
        public float attackRange;
        public Transform attackPoint;
        public LayerMask enemyLayer;
        public GameObject blood;

        void Start()
        {
            PlayerRigid = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            facingRight = true;
        }

        void Update()
        {
            //var animator = this.gameObject.GetComponent<Animator>();
            //animator.SetFloat("Velocity", Mathf.Abs(this.gameObject.GetComponent<Rigidbody2D>().velocity.x / 5.0f));
            if (isTouchingDeathSwamp)
            {
                health -= 0.001f;
            }


            if (health > 0)
            {
                moving();
                jump();
                Attack();
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

            if (horizontal > 0 && facingRight == false)
            {
                Flip();
            }
            else if (horizontal < 0 && facingRight == true)
            {
                Flip();
            }

            //movementDirection = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);
            PlayerRigid.velocity = new Vector2(horizontal * currentSpeed, PlayerRigid.velocity.y);
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, 0, 1000), transform.position.y, transform.position.z);
        }

        void Flip()
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            facingRight = !facingRight;
        }


        void updateAnimator()
        {
            // set Fall
            if (PlayerRigid.velocity.y < 0 && !onGround)
            {
                animator.SetBool("isFall", true);
            }
            else if (PlayerRigid.velocity.y == 0 || onGround)
            {
                animator.SetBool("isFall", false);
            }

            //animator.SetFloat("Health", currentSpeed);

            // set Run
            animator.SetFloat("Speed", currentSpeed);
        }

        void setWallJumpingToFalse()
        {
            wallJumping = false;
        }



        void Attack()
        {
            if (Time.time > nextAttackTime)
            {
                if (Input.GetKeyDown(KeyCode.J))
                {
                    Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
                    // foreach (Collider2D col in enemiesToDamage)
                    // {
                    //     col.GetComponent<Enemy>().TakeDamage(damage);
                    // }

                    animator.SetTrigger("stab");
                }
                else if (Input.GetKeyDown(KeyCode.K))
                {
                    animator.SetTrigger("gunAttack");
                    Quaternion boltRotation = Quaternion.identity;
                    if (!facingRight)
                    {
                        boltRotation = Quaternion.Euler(0f, 180f, 0f);
                    }
                    Instantiate(bolt, shotPoint.position, boltRotation);
                }
                else if (Input.GetKeyDown(KeyCode.L))
                {
                    animator.SetTrigger("dodge");
                    if (facingRight)
                    {
                        transform.position = new Vector3(transform.position.x + 2f, transform.position.y, transform.position.z);
                    }
                    else
                    {
                        transform.position = new Vector3(transform.position.x - 2f, transform.position.y, transform.position.z);
                    }
                }
                else if (Input.GetKeyDown(KeyCode.U))
                {
                    animator.SetTrigger("transition");
                }
                else if (Input.GetKeyDown(KeyCode.I))
                {
                    animator.SetTrigger("spectialJump");
                    PlayerRigid.velocity = Vector2.up * 1.15f * 11;
                }
                else if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    shootWeapon(0);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    shootWeapon(1);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    shootWeapon(2);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha4))
                {
                    shootWeapon(3);
                }

                nextAttackTime = Time.time + timeBetweenAttacks;
            }
        }


        public void TakeDamage(int damage)
        {
            if (health > 0)
            {
                health -= damage;
                if (health <= 0.1f)
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

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }

        private void shootWeapon(int index)
        {
            animator.SetTrigger("gunAttack");
            Quaternion boltRotation = Quaternion.identity;
            var weapon = bagManager.LaunchWeapon(index);
            if (!facingRight)
            {
                boltRotation = Quaternion.Euler(0f, 180f, 0f);
            }

            if (weapon != null)
            {
                var weaponObject = Instantiate(weapon, shotPoint.position, boltRotation);
                weaponObject.transform.localScale = new Vector2(0.5f, 0.5f);

                weaponObject.AddComponent<Test>();
                weaponObject.GetComponent<Test>().transform = weaponObject.transform;
                weaponObject.GetComponent<Test>().degrees = -45;
                weaponObject.AddComponent<Bolt>();
                weaponObject.GetComponent<Bolt>().speed = 8;
                weaponObject.GetComponent<Bolt>().damage = 1;
                weaponObject.GetComponent<Bolt>().lifeTime = 3;
                weaponObject.GetComponent<Bolt>().boltHit = GameObject.Find("BoltHit");

                if (!facingRight)
                {
                    weaponObject.GetComponent<Test>().degrees = 135;
                }
                weaponObject.AddComponent<Rigidbody2D>();
                weaponObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            }
        }

       
    }
}