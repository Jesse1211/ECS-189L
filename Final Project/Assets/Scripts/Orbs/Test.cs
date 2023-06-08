using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public RectTransform rectTransform;
    public float degrees;

    private void Start()
    {
        // Get the RectTransform component if not assigned
        if (rectTransform == null)
            rectTransform = GetComponent<RectTransform>();

        // Rotate the RectTransform by 45 degrees
        rectTransform.rotation = Quaternion.Euler(0f, 0f, degrees);
    }
}