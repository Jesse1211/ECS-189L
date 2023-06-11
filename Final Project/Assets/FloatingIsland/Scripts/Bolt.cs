using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Project
{
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
            if (tag == "Bolt1")
            {
                transform.Translate(Vector2.right * speed * Time.deltaTime);
            }
            else
            {
                transform.Translate(new Vector3() { x = 1, y = 1, z = 0 } * speed * Time.deltaTime);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Enemy")
            {
                collision.GetComponent<Enemy>().TakeDamage(damage);
            }
            if (collision.tag == "Enemy")
            {
                collision.GetComponent<FSM>().param.hp.GetComponent<Slider>().value -=10f;
                collision.GetComponent<FSM>().param.getHit = true;
            }
            Destroy(gameObject);

            if (boltHit != null)
            {
                Instantiate(boltHit, transform.position, transform.rotation);
            }

        }
    }
}
