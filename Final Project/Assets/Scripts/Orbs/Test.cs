using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Transform transform;
    public float degrees;

    private void Start()
    {
        // Get the RectTransform component if not assigned
        if (transform == null)
            transform = GetComponent<RectTransform>();

        // Rotate the RectTransform by 45 degrees
        transform.rotation = Quaternion.Euler(0f, 0f, degrees);
    }
}