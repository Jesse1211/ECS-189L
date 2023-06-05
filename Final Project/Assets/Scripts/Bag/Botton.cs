using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public class Button : MonoBehaviour
    {
        public GameObject button;
        public GameObject Pannel;

        private bool isClose;

        private void Awake()
        {
            Pannel.GetComponent<BagDataLoader>().UpdateBagItem();
            Pannel.GetComponent<BagDataLoader>().UpdateWeaponList();
            
            isClose = false;
            Pannel.SetActive(isClose);
        }

        public void UpdateActive()
        {
            isClose = (isClose) ? false : true;

            if (isClose is false)
            {
                Pannel.GetComponent<BagDataLoader>().UpdateBagItem();
                Pannel.GetComponent<BagDataLoader>().UpdateWeaponList();
            }

            Pannel.SetActive(isClose);
        }
    }

}
