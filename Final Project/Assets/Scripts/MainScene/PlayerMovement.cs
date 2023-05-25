using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Animator animator;
    public Transform transform;

    public float runSpeed = 40f;
    float currentSpeed = 0f;
    bool jump = false;
    bool attack = false;

    void Start()
    {
        currentSpeed = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(currentSpeed));

        if (Input.GetButtonDown("S"))
        {
            animator.SetBool("backDodge", false);
            transform.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
        }

        animator.SetBool("backDodge", false);

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

        if (Input.GetButtonDown("Attack"))
        {
            attack = true;
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
