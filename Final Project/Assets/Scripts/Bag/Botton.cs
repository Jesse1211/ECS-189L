using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public GameObject button;
    
    private bool isClose;

    private void Awake()
    {
        isClose = false;
    }

    private void updateActive()
    {
        isClose = (isClose) ? false : true;
        gameObject.SetActive(isClose);
    }
}
