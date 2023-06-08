using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FanboyController : MonoBehaviour
{
    public float movePower = 6f;
    public float jumpPower = 15f; //Set Gravity Scale in Rigidbody2D Component to 5

    private Rigidbody2D rb;
    private Animator anim;
    Vector3 movement;
    private int direction = 1;
    bool isJumping = false;
    private bool alive = true;

    //public GameObject bolt;
    public float timeBetweenAttacks;
    private float nextAttackTime;
    //public Transform shotPoint;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        Restart();
        if (alive)
        {
            Hurt();
            Die();
            Attack();
            Jump();
            Run();

        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        anim.SetBool("isJump", false);
    }


    void Run()
    {
        Vector3 moveVelocity = Vector3.zero;
        anim.SetBool("isRun", false);


        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            direction = -1;
            moveVelocity = Vector3.left;

            transform.localScale = new Vector3(direction * 1.7f, 1.7f, 1.7f);
            if (!anim.GetBool("isJump"))
                anim.SetBool("isRun", true);

        }
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            direction = 1;
            moveVelocity = Vector3.right;

            transform.localScale = new Vector3(direction * 1.7f, 1.7f, 1.7f);
            if (!anim.GetBool("isJump"))
                anim.SetBool("isRun", true);

        }
        transform.position += moveVelocity * movePower * Time.deltaTime;
    }
    void Jump()
    {
        if ((Input.GetButtonDown("Jump") || Input.GetAxisRaw("Vertical") > 0)
        && !anim.GetBool("isJump"))
        {
            isJumping = true;
            anim.SetBool("isJump", true);
        }
        if (!isJumping)
        {
            return;
        }

        rb.velocity = Vector2.zero;

        Vector2 jumpVelocity = new Vector2(0, jumpPower);
        rb.AddForce(jumpVelocity, ForceMode2D.Impulse);

        isJumping = false;
    }
    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.J) && Time.time > nextAttackTime)
        {  
            anim.SetTrigger("attack");
            nextAttackTime = Time.time + timeBetweenAttacks;
        }

        if (Input.GetKeyDown(KeyCode.K) && Time.time > nextAttackTime)
        {
            anim.SetTrigger("heavyAttack");
            // Quaternion boltRotation = Quaternion.identity;
            // if (direction < 0)
            // {
            //     // Player is facing left
            //     boltRotation = Quaternion.Euler(0f, 180f, 0f);
            // }

            // Instantiate(bolt, shotPoint.position, boltRotation);
            nextAttackTime = Time.time + timeBetweenAttacks;
        }

        if (Input.GetKeyDown(KeyCode.L) && Time.time > nextAttackTime)
        {  
            anim.SetTrigger("dodgeDash");
            nextAttackTime = Time.time + timeBetweenAttacks;
        }

        if (Input.GetKeyDown(KeyCode.U) && Time.time > nextAttackTime)
        {  
            anim.SetTrigger("jumpAttack");
            nextAttackTime = Time.time + timeBetweenAttacks;
        }

        if (Input.GetKeyDown(KeyCode.I) && Time.time > nextAttackTime)
        {  
            anim.SetTrigger("deflectHeal");
            nextAttackTime = Time.time + timeBetweenAttacks;
        }

        if (Input.GetKeyDown(KeyCode.P) && Time.time > nextAttackTime)
        {  
            anim.SetTrigger("teleVanish");
            nextAttackTime = Time.time + timeBetweenAttacks;
        }

    }
    void Hurt()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            anim.SetTrigger("isHit");
        }
    }
    void Die()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            anim.SetTrigger("die");
            alive = false;
        }
    }
    void Restart()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            anim.SetTrigger("idle");
            alive = true;
        }
    }
}