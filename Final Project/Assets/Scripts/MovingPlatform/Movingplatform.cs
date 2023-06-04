using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed;
    public int startingPoint;
    public Transform[] points;

    private int index;

    void Start()
    {
        transform.position = points[startingPoint].position;
    }

    void Update()
    {

        if (Vector2.Distance(transform.position, points[index].position) < 0.2f)
        {
            index = (index + 1) % 2;
        }

        transform.position = Vector3.Lerp(transform.position, points[index].position, speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.SetParent(transform);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);
    }
}