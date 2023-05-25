using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Captain.Command
{
    public class PlayerMovement : MonoBehaviour
    {

        private IPlayerCommand jump;
        private IPlayerCommand right;
        private IPlayerCommand left;
        public int score;

        //public Animator animator;
        //public Transform transform;

        //public float runSpeed = 40f;
        //float currentSpeed = 0f;
        //bool attack = false;

        void Start()
        {

            this.jump = ScriptableObject.CreateInstance<Jump>();
            this.right = ScriptableObject.CreateInstance<MoveRight>();
            this.left = ScriptableObject.CreateInstance<MoveLeft>();

            //currentSpeed = Input.GetAxisRaw("Horizontal") * runSpeed;

            //animator.SetFloat("Speed", Mathf.Abs(currentSpeed));

            //if (Input.GetButtonDown("S"))
            //{
            //    animator.SetBool("backDodge", false);
            //    transform.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
            //}

            //animator.SetBool("backDodge", false);

            //if (Input.GetButtonDown("Jump"))
            //{
            //    jump = true;
            //}

            //if (Input.GetButtonDown("Attack"))
            //{
            //    attack = true;
            //}

        }

        void Update()
        {

            if (Input.GetAxis("Horizontal") > 0.01)
            {
                this.right.Execute(this.gameObject);
            }
            if (Input.GetAxis("Horizontal") < -0.01)
            {
                this.left.Execute(this.gameObject);
            }
            if (Input.GetButtonDown("Jump"))
            {
                this.jump.Execute(this.gameObject);
            }

            var animator = this.gameObject.GetComponent<Animator>();
            animator.SetFloat("Velocity", Mathf.Abs(this.gameObject.GetComponent<Rigidbody2D>().velocity.x / 5.0f));
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "PickUp")
            {
                Destroy(collision.gameObject);
                this.score++;
            }
        }

    }
}