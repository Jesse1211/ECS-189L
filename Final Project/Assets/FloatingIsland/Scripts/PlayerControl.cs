using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    Rigidbody2D PlayerRigid;

    // run animation
    public float movePower = 6f;
    public float jumpPower = 15f;
    private int direction = 1;

    // fall animation
    // private bool onGround = true;
    // public Vector2 minPosition;
    // public Vector2 maxPosition;
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

        // set Fall
        if (PlayerRigid.velocity.y < 0)
        {
            animator.SetBool("IsFall", true);
        }
        else if (PlayerRigid.velocity.y == 0)
        {
            animator.SetBool("IsFall", false);
        }

        //animator.SetFloat("Health", currentSpeed);

        // set Run
        //animator.SetFloat("Speed", currentSpeed);

        // set Transition
        if (Input.GetKeyDown(KeyCode.U))
        {
            animator.SetTrigger("transitionEnter");
            animator.ResetTrigger("transitionExit");
        }
        else if (Input.GetKeyUp(KeyCode.U))
        {
            animator.ResetTrigger("transitionEnter");
            animator.SetTrigger("transitionExit");
        }

        // set Dodge
        if (Input.GetKeyDown(KeyCode.L))
        { 
            animator.SetTrigger("DodgeEnter");
            animator.ResetTrigger("DodgeExit");
        }
        else if (Input.GetKeyUp(KeyCode.L))
        {
            animator.ResetTrigger("DodgeEnter");
            animator.SetTrigger("DodgeExit");
        }

        // set Stab
        if (Input.GetKeyDown(KeyCode.J))
        {
            animator.SetTrigger("StabEnter");
            animator.ResetTrigger("StabExit");
        }
        else if (Input.GetKeyUp(KeyCode.J))
        {
            animator.ResetTrigger("StabEnter");
            animator.SetTrigger("StabExit");
        }

        // set Gunattack
        if (Input.GetKeyDown(KeyCode.K))
        {
            animator.SetTrigger("GunattackEnter");
            animator.ResetTrigger("GunattackExit");
        }
        else if (Input.GetKeyUp(KeyCode.K))
        {
            animator.ResetTrigger("GunattackEnter");
            animator.SetTrigger("GunattackExit");
        }
    }
    void jump()
    {
        if (Input.GetKey(KeyCode.W))
        {
            PlayerRigid.velocity = Vector2.zero;
            Vector2 jumpVelocity = new Vector2(0, jumpPower);
            PlayerRigid.AddForce(jumpVelocity, ForceMode2D.Impulse);
        }

        if (PlayerRigid.velocity.y > 0)
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
        Vector3 moveVelocity = Vector3.zero;
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            direction = 1;
            moveVelocity = Vector3.left;
            transform.localScale = new Vector3(direction * 1.5f, 1.5f, 1.5f);
        }
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            direction = -1;
            moveVelocity = Vector3.right;
            transform.localScale = new Vector3(direction * 1.5f, 1.5f, 1.5f);
        }
        transform.position += moveVelocity * movePower * Time.deltaTime;
    }

    /// <summary>
    /// check if on ground
    /// </summary>
    // void OnCollisionEnter2D(Collision2D collision)
    // {
    //     if (collision.collider.tag == "ground" || collision.collider.tag == "paltform")
    //     {
    //         onGround = true;
    //     }
    // }

    // void OnCollisionExit2D(Collision2D collision)
    // {
    //     if (collision.collider.tag == "ground" || collision.collider.tag == "paltform")
    //     {
    //         onGround = false;
    //     }
    // }
}