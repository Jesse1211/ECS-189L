using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public class DeathSwamp : MonoBehaviour
    {
        public bool isTouchingSwamp;

        void Awake()
        {
            isTouchingSwamp = false;
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.tag == "Player")
            {
                isTouchingSwamp = true;
            }
        }

        void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.collider.tag == "Player")
            {
                isTouchingSwamp = false;
            }
        }
    }

}
