using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Project
{
    public class MeterScript : MonoBehaviour
    {
        public GameObject prefab;
        private static PlayerControllerData playerController;

        public Slider slider;
        public Gradient gradient;
        public Image fill;

        private void Start()
        {
            playerController = prefab.GetComponent<PlayerControllerData>();
            SetMaxHealth(10); //sets your meter's fill to maximum from the start
        }

        void FixedUpdate()
        {
            SetHealth(playerController.score); //links your variable to the meter's fill
        }

        public void SetMaxHealth(float health)
        {
            slider.maxValue = health;
            slider.value = health;

            fill.color = gradient.Evaluate(1f);

        }

        public void SetHealth(float health)
        {
            slider.value = health;
            fill.color = gradient.Evaluate(slider.normalizedValue);
        }
    }

}
