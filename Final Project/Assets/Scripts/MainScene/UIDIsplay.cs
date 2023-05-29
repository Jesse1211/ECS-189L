using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Project
{
    public class UiDisplay : MonoBehaviour
    {
        public Text healthText;
        public GameObject prefab;
        private static PlayerController playerController;

        private void Start()
        {
            playerController = prefab.GetComponent<PlayerController>();
        }

        void Update()
        {
            healthText.text = "Health: " + playerController.score;
        }
    }

}
