using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Collections.AllocatorManager;
using UnityEngine.UI;
using System;

namespace Project
{
    /// <summary>
    /// keep in tack of player's movement & animator
    /// </summary>
    public class PlayerControllerAnimator : PlayerControllerData
    {
        // run animation
        public float currentSpeed = 5.0f;
        private float acceleration = 0.03f;
        private float maxSpeed = 6.5f;

        // climbing
        private bool isWallSliding;
        public Transform frontcheck;
        private float wallSlidingSpeed = 5;
        private bool wallJumping;
        private float xWallForce = 10f;
        private float yWallForce = 10f;
        private float wallJumpTime = 0.05f;

        // attack
        public GameObject bolt;
        public int damage;
        public float timeBetweenAttacks = 1f;
        public Transform shotPoint;
        private float nextAttackTime = 0;
        public float attackRange;
        public Transform attackPoint;
        public LayerMask enemyLayer;
        
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
                TakeDamage(0.1f);
            }


            if (HP.GetComponent<Slider>().value > 0)
            {
                moving();
                jump();
                Attack();
                updateAnimator();

                isWallSliding = ((isTouchingWall is true) && (!onGround) && (!onRoof) && Input.GetAxisRaw("Horizontal") != 0) ? true : false;

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
                PlayerRigid.velocity = Vector2.up * 9f;
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
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -1000, 1000), transform.position.y, transform.position.z);
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
                    foreach (Collider2D col in enemiesToDamage)
                    {
                        col.GetComponent<Enemy>().TakeDamage(25);
                    }
                    animator.SetTrigger("stab");
                    nextAttackTime = Time.time + timeBetweenAttacks;
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
                    nextAttackTime = Time.time + timeBetweenAttacks;
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
                    nextAttackTime = Time.time + timeBetweenAttacks;
                }
                else if (Input.GetKeyDown(KeyCode.U))
                {
                    animator.SetTrigger("transition");
                    nextAttackTime = Time.time + timeBetweenAttacks;
                }
                else if (Input.GetKeyDown(KeyCode.I))
                {
                    animator.SetTrigger("spectialJump");
                    PlayerRigid.velocity = Vector2.up * 1.15f * 11;
                    nextAttackTime = Time.time + timeBetweenAttacks;
                }
                else if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    shootWeapon(0);
                    nextAttackTime = Time.time + timeBetweenAttacks;
                }
                else if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    shootWeapon(1);
                    nextAttackTime = Time.time + timeBetweenAttacks;
                }
                else if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    shootWeapon(2);
                    nextAttackTime = Time.time + timeBetweenAttacks;
                }
                else if (Input.GetKeyDown(KeyCode.Alpha4))
                {
                    shootWeapon(3);
                    nextAttackTime = Time.time + timeBetweenAttacks;
                }
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

                if(weaponObject.tag == "MasterWeapon"){
                    weaponObject.GetComponent<Bolt>().damage = 50;
                    weaponObject.GetComponent<Bolt>().speed = 3;
                }

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



/*
 * 
 * 
 * 小女孩: Player
 * 
 * Fanboy: Default
 *          area: Default
 *          atttactArea : Player
 *          follow: Default
 */