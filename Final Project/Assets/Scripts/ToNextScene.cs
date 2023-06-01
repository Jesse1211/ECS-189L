using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToNextScene : MonoBehaviour
{
    public Animator transition;

    public float transitionTime = 1f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log(collision.GetType().ToString());
        // Debug.Log(collision.gameObject.tag);
        
        // if (collision.gameObject.CompareTag("Player")
        //     && collision.GetType().ToString() == "UnityEngine.BoxCollider2D")
        // {
        //     SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); 
        // }
        if (other.CompareTag("Player"))
        {
            Debug.Log("switch scene");
            // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); 
            StartCoroutine(SwitchScene(SceneManager.GetActiveScene().buildIndex + 1));
        }

        


    }

    IEnumerator SwitchScene(int scene)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(scene);
    }
}
