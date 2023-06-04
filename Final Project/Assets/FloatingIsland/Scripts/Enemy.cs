using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float speed;
    int currentPointIndex;

    float waitTime;
    public float startWaitTime;
    Animator anim;

    public int damage;
    public int health;
    public GameObject blood;

    bool foundPlayer;
    public Transform playerCheck;
    public float checkRaduis;
    public LayerMask playerLayer;

    private float timeBetweenAttacks = 2f;
    private float nextAttackTime;
    private bool alive = true;

    private void Start()
    {
        transform.position = patrolPoints[0].position;
        transform.rotation = patrolPoints[0].rotation;
        waitTime = startWaitTime;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if(alive)
        {
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[currentPointIndex].position, speed * Time.deltaTime);
        
            if(transform.position == patrolPoints[currentPointIndex].position)
            {
                transform.rotation = patrolPoints[currentPointIndex].rotation;
                anim.SetBool("isRun", false);
                if (waitTime <= 0)
                {
                    if (currentPointIndex + 1 < patrolPoints.Length)
                    {
                        currentPointIndex++;
                    }
                    else
                    {
                        currentPointIndex = 0;
                    }
                    waitTime = startWaitTime;
                }
                else
                {
                    waitTime -= Time.deltaTime;
                }
            }
            else
            {
                anim.SetBool("isRun", true);
            }

            foundPlayer = Physics2D.OverlapCircle(playerCheck.position, checkRaduis, playerLayer);

            if (foundPlayer && Time.time > nextAttackTime)
            {
                anim.SetTrigger("attack");
                nextAttackTime = Time.time + timeBetweenAttacks;

                Collider2D playerToDamage = Physics2D.OverlapCircle(playerCheck.position, checkRaduis, playerLayer);
                if (playerToDamage != null)
                {
                    Debug.Log("Demon hits player!");
                    playerToDamage.GetComponent<GirlController>().TakeDamage(damage);
                }
            }
        }
    }

    // private void OnTriggerEnter2D(Collider2D collision)
    // {
    //     if (collision.tag == "Player")
    //     {
    //         collision.GetComponent<GirlController>().TakeDamage(damage);
    //     }
    // }

    public void TakeDamage(int damage)
    {
        if(alive)
        {
            health -= damage;
            Debug.Log("Enemy's health: " + health);
            if(health <= 0.1f)
            {
                anim.SetTrigger("die");
                alive = false;
                Destroy(gameObject, 1f);
                return;
            }

            anim.SetTrigger("isHit");
            Instantiate(blood, transform.position, Quaternion.identity);
        }
    }
}