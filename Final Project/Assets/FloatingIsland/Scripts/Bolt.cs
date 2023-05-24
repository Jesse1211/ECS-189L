using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolt : MonoBehaviour
{
    public float speed;
    public int damage;
    public float lifeTime;
    public GameObject boltHit;

    void Start()
    { 
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if (collision.tag == "Enemy")
        // {
        //     collision.GetComponent<Enemy>().TakeDamage(damage);
        // }
        Destroy(gameObject);
        Instantiate(boltHit, transform.position, transform.rotation);
    }
}
