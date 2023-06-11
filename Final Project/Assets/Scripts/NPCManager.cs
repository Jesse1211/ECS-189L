using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public class NPCManager : MonoBehaviour
    {
        public GameObject NPCs;
        private PlayerControllerData playerControllerData;

        void Start()
        {
            NPCs.SetActive(false);
            playerControllerData = this.gameObject.GetComponent<PlayerControllerData>();
        }

        void Update()
        {
            if (playerControllerData.score > 0)
            {
                NPCs.SetActive(true);
            }
        }
    }
}
