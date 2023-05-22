using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_horizontal : MonoBehaviour
{
    // Start is called before the first frame update


    private float CurrentSpeed = 5.0f;
    Rigidbody2D PlayerRigid;
    private float Acceleration = 0.03f;
    private float MaxSpeed = 6.5f;
    private bool IsOnGround = true;
    void Start()
    {
        PlayerRigid = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {

        run();
        jump();

    }
    void jump() 
    {
        if (Input.GetKey(KeyCode.Space) && IsOnGround)
        {
            PlayerRigid.velocity = Vector2.up * 15f;

        }

    }
    void run()
    {
        float horizontal = Input.GetAxis("Horizontal");
     
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) && IsOnGround)
        {
        
            if (CurrentSpeed <= MaxSpeed)
            {
                CurrentSpeed += Acceleration;
                
            }
            else
            {
                CurrentSpeed = MaxSpeed;
            }

        }

        else if ((!Input.GetKey(KeyCode.A) || !Input.GetKey(KeyCode.D)) && IsOnGround)
        {
            if (CurrentSpeed <= 5.0f)
            {
                CurrentSpeed = 5.0f;
            }
            else
            {
                CurrentSpeed -= 0.01f;
            }

        }
        Vector2 moveDir = new Vector2(horizontal * CurrentSpeed, PlayerRigid.velocity.y);
        PlayerRigid.velocity = moveDir;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -9f, 9f), Mathf.Clamp(transform.position.y, -3.62f, 10f), transform.position.z);

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "ground" || collision.collider.tag == "paltform")
        {
            IsOnGround = true;
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "ground" || collision.collider.tag == "paltform")
        {
            IsOnGround = false;
        }
    }
}
