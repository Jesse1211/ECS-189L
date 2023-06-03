using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class OrbController : MonoBehaviour
{
    public GameObject redPrefab;
    public GameObject whitePrefab;
    public GameObject orbPlant;
    public float timeInterval;
    public float speed = 10f;

    private float temp;
    private GameObject tempPrefab;
    private Vector3 destination;

    private void Awake()
    {
        destination = new Vector3(orbPlant.transform.position.x, orbPlant.transform.position.y + 10000, 1);
        temp = timeInterval;
    }

    void Update()
    {
        if (temp < 0)
        {
            tempPrefab = Generator();
            temp = timeInterval;
        }
        else
        {
            temp -= Time.deltaTime;
        }

        if (tempPrefab != null)
        {
            Movement(tempPrefab);
        }
    }

    private GameObject Generator()
    {
        var select = Random.Range(0, 2);
        var newPosition = new Vector3(orbPlant.transform.position.x, orbPlant.transform.position.y + 1, orbPlant.transform.position.z);
        if (select is 0)
        {
            Instantiate(redPrefab, newPosition, Quaternion.identity);
        }
        return Instantiate(whitePrefab, newPosition, Quaternion.identity);
    }

    private void Movement(GameObject prefab)
    {
        prefab.transform.position = Vector3.Lerp(prefab.transform.position, destination, Time.deltaTime * speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(collision.gameObject);
    }
}
