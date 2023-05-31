using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public class PlayerDataController : MonoBehaviour
    {
        public int score;

        void Start()
        {
    
        }

        void Update()
        {

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