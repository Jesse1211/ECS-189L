using ClearSky;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Project 
{
public class ThunderTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject Thunder;
    [SerializeField] GameObject Thunder2;
    [SerializeField] GameObject HP;
    private bool hasCollide = false;
    private LightningBolt2D ThunderComponent, ThunderComponent2;
    BoxCollider2D ThunderCollider, ThunderCollider2;
    Slider hp;

    void Awake()
    {
        ThunderComponent = Thunder.GetComponent<LightningBolt2D>();
        ThunderComponent2 = Thunder2.GetComponent<LightningBolt2D>();
        ThunderCollider = Thunder.GetComponent<BoxCollider2D>();
        ThunderCollider2 = Thunder2.GetComponent<BoxCollider2D>();
        hp = HP.GetComponent<Slider>();
       
    }

    // Update is called once per frame
    void Update()
    {
        if (hp.value <= 0) 
        {
            ThunderCollider.enabled = false;
            ThunderCollider2.enabled = false;
            
            ThunderComponent.lineWidth = 0f;
            ThunderComponent2.lineWidth = 0f;
        }
    }


    // void OnCollisionEnter2D(Collision2D collision)
    // {
    //     if (collision.collider.tag == "Player" && !hasCollide)
    //     {
    //         ThunderCollider.enabled = true;
    //         ThunderCollider2.enabled = true;
    //         hasCollide = true;
    //         ThunderComponent.lineWidth = 0.2f;
    //         ThunderComponent2.lineWidth = 0.2f;
    //     }
    // }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !hasCollide)
        {
            ThunderCollider.enabled = true;
            ThunderCollider2.enabled = true;
            hasCollide = true;
            ThunderComponent.lineWidth = 0.2f;
            ThunderComponent2.lineWidth = 0.2f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {


    }


}
}
