using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public class DeathWater : MonoBehaviour
    {
        public bool isTouchingWater;

        void Awake()
        {
            isTouchingWater = false;
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.tag == "Player")
            {
                isTouchingWater = true;
            }
        }

        void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.collider.tag == "Player")
            {
                isTouchingWater = false;
            }
        }
    }

}
