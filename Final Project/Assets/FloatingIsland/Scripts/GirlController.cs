using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GirlController : MonoBehaviour
{
    public float speed = 6f;
    private Rigidbody2D rb;
    private Animator anim;
    bool facingRight = true;

    bool isGrounded;
    public Transform groundCheck;
    public float checkRaduis;
    public LayerMask whatIsGround;
    public float jumpForce;

    public int health;
    public int damage;

    // Vector3 movement;
    // private int direction = 1;
    // bool isJumping = false;
    private bool alive = true;

    public GameObject bolt;
    public float timeBetweenAttacks;
    private float nextAttackTime;
    public Transform shotPoint;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if(alive)
        {
            Run();
            Jump();
            Attack();
        }
    }

    void Run()
    {
        float input = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(input * speed, rb.velocity.y);

        if (input > 0 && facingRight == false)
        {
            Flip();
        }
        else if (input < 0 && facingRight == true)
        {
            Flip();
        }

        if (input != 0)
        {
            anim.SetBool("isRun", true);
        }
        else
        {
            anim.SetBool("isRun", false);
        }
    }

    void Jump()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRaduis, whatIsGround);

        if (isGrounded == true)
        {
            anim.SetBool("isJump", false);
        }
        else
        {
            anim.SetBool("isJump", true);
        }

        if (Input.GetKeyDown(KeyCode.W) && isGrounded == true)
        {
            rb.velocity = Vector2.up * jumpForce;
        }
    }

    void Attack()
    {
        if (Time.time > nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {  
                //transform.localScale = new Vector3(-direction * 1.7f, 1.7f, 1.7f);
                anim.SetTrigger("stab");
                nextAttackTime = Time.time + timeBetweenAttacks;
            }
            else if (Input.GetKeyDown(KeyCode.K))
            {
                anim.SetTrigger("gunAttack");
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
                anim.SetTrigger("dodge");
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
                anim.SetTrigger("transition");
                nextAttackTime = Time.time + timeBetweenAttacks;
            }
            else if (Input.GetKeyDown(KeyCode.I))
            {  
                anim.SetTrigger("spectialJump");
                rb.velocity = Vector2.up * 1.15f * jumpForce;
                nextAttackTime = Time.time + timeBetweenAttacks;
            }
        }
    }

    void Flip()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        facingRight = !facingRight;
    }

    public void TakeDamage(int damage)
    {
        if(alive)
        {
            health -= damage;
            Debug.Log(health);
            if(health <= 0.1f)
            {
                anim.SetTrigger("die");
                alive = false;
            }
            else
            {
                anim.SetTrigger("isHit");
                if (facingRight)
                {
                    rb.velocity = new Vector3(-2f, 2f, 0f);
                }
                else
                {
                    rb.velocity = new Vector3(2f, 2f, 0f);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }
}

//     void Fall()
//     {
//         if (rb.velocity.y < 0)
//         {
//             anim.SetBool("isFall", true);
//         }
//         else if (rb.velocity.y == 0)
//         {
//             anim.SetBool("isFall", false);
//         }
//     }

//     void Hurt()
//     {
//         if (Input.GetKeyDown(KeyCode.O))
//         {
//             anim.SetTrigger("isHit");
//         }
//     }
//     void Die()
//     {
//         if (Input.GetKeyDown(KeyCode.T))
//         {
//             anim.SetTrigger("die");
//             alive = false;
//         }
//     }



//     void Restart()
//     {
//         if (Input.GetKeyDown(KeyCode.Alpha0))
//         {
//             anim.SetTrigger("idle");
//             alive = true;
//         }
//     }
// }