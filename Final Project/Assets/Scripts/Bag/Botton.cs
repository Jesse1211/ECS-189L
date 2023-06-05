using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public class Button : MonoBehaviour
    {
        public GameObject button;
        public GameObject pannel;

        private bool isClose;

        private void Awake()
        {
            isClose = true;
            pannel.GetComponent<DataLoader>().UpdateBagItem();
        }

        public void UpdateActive()
        {
            isClose = (isClose) ? false : true;

            if (isClose is false)
            {
                pannel.GetComponent<DataLoader>().UpdateBagItem();
            }

            pannel.SetActive(isClose);

        }
    }

}
