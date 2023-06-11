using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project
{
    public class ToNextScene : MonoBehaviour
    {
        public Animator transition;
        public GameObject player;
        public int sceneIndex;
        private float transitionTime = 1f;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                if (player.GetComponent<PlayerControllerAnimator>().score >= 5)
                {
                    Debug.Log("switch scene");

                    if (SceneManager.GetActiveScene().buildIndex == 0)
                    {
                        this.GetComponent<NPC>().enabled = false;

                        StartCoroutine(SwitchScene(sceneIndex));
                    }
                    else if (SceneManager.GetActiveScene().buildIndex == 1)
                    {
                        StartCoroutine(SwitchScene(0));
                    }
                    else if (SceneManager.GetActiveScene().buildIndex == 2)
                    {
                        StartCoroutine(SwitchScene(0));
                    }
                }
            }
        }

        IEnumerator SwitchScene(int scene)
        {
            transition.SetTrigger("Start");
            yield return new WaitForSeconds(transitionTime);
            SceneManager.LoadScene(scene);
        }
    }
}