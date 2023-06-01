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
        private static PlayerControllerData playerController;

        private void Start()
        {
            playerController = prefab.GetComponent<PlayerControllerData>();
        }

        void Update()
        {
            healthText.text = "Health: " + playerController.score;
        }
    }

}
