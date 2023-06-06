using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    /// <summary>
    /// Open & Close panels
    /// </summary>
    public class Button : MonoBehaviour
    {
        public GameObject button;
        public GameObject Panel;

        private bool isOpen;

        private void Start()
        {
            isOpen = false;
            Panel.SetActive(isOpen);
        }

        public void UpdateActive()
        {
            isOpen = (isOpen) ? false : true;
            Panel.SetActive(isOpen);
        }
    }

}
