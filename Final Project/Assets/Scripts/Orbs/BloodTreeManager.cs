using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public class BloodTreeManager : MonoBehaviour
    {
        public Animator animator;
        public GameObject player;
        private bool fire;
        private bool dead;
        private PlayerControllerData playerControllerData;

        void Start()
        {
            if (this.gameObject.tag == "BloodTreeFire")
            {
                fire = true;
                dead = false;
            }
            else
            {
                fire = false;
                dead = false;
            }

            playerControllerData = player.GetComponent<PlayerControllerData>();
        }

        void Update()
        {
            if (playerControllerData.score > 0)
            {
                fire = false;
                dead = true;
            }

            animator.SetBool("fire", fire);
            animator.SetBool("dead", dead);
        }
    }
}
