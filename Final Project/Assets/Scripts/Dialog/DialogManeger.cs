using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManeger : MonoBehaviour
{
    private Queue<string> sentences;

    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialog(Dialog dialog)
    {
        Debug.Log("Start");
    }
}
