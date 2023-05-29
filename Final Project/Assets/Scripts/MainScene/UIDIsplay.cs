using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiDisplay : MonoBehaviour
{
    private int score = 5;
    public Text healthText;

    void Update()
    {
        healthText.text = "Health: " + score;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            score++;
        }
    }
}
