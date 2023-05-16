using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToNextScene : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
     
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.GetType().ToString());
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Player")
            && collision.GetType().ToString() == "UnityEngine.BoxCollider2D")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); 
        }
    }
}
