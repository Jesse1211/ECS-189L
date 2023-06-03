using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoving : MonoBehaviour
{
    [SerializeField] public float followSpeed;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        transform.position = new Vector3(player.position.x, player.position.y + 2f, -10f);
    }

    void LateUpdate()
    {
        var targetPosition = new Vector3(player.position.x, player.position.y + 2f, transform.position.z);
        var distance = Vector3.Distance(targetPosition, transform.position);

        if (distance > 1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, followSpeed * Time.deltaTime);
        }
        else 
        {
            transform.position = targetPosition;
        }
    }
}
