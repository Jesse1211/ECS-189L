using ClearSky;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject Thunder;
    [SerializeField] GameObject Thunder2;
    private bool hasCollide = false;
    private LightningBolt2D ThunderComponent, ThunderComponent2;
    BoxCollider2D ThunderCollider, ThunderCollider2;
    void Awake()
    {
       ThunderComponent = Thunder.GetComponent<LightningBolt2D>();
       ThunderComponent2 = Thunder2.GetComponent<LightningBolt2D>();
       ThunderCollider = Thunder.GetComponent<BoxCollider2D>();
       ThunderCollider2 = Thunder2.GetComponent<BoxCollider2D>();
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player" && !hasCollide)
        {
            ThunderCollider.enabled = true;
            ThunderCollider2.enabled = true;
            hasCollide = true;
            ThunderComponent.lineWidth = 0.2f;
            ThunderComponent2.lineWidth = 0.2f;
        }
    }

}
