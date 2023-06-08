using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public class HealthManager : MonoBehaviour
    {
        public GameObject player;
        public GameObject water;
        public GameObject swamp;

        private PlayerControllerData playerControllerData;
        private DeathSwamp deathSwamp;
        private DeathWater deathWater;

        void Awake()
        {
            playerControllerData = player.GetComponent<PlayerControllerData>();
            deathSwamp = swamp.GetComponent<DeathSwamp>();
            deathWater = water.GetComponent<DeathWater>();
        }

        void Update()
        {
            if (deathWater.isTouchingWater)
            {
                playerControllerData.health = 0;
            }
            else if (deathSwamp.isTouchingSwamp)
            {
                playerControllerData.health -= 0.001f;
            }
        }
    }
}
