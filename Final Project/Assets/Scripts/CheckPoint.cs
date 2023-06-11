using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static System.TimeZoneInfo;

public class CheckPoint : MonoBehaviour
{
    public Animator transition;
    public Transform checkPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            StartCoroutine(SwitchScene(1, collision.gameObject));
        }
    }

    IEnumerator SwitchScene(int scene, GameObject gameObject)
    {
        transition.SetTrigger("Start");
        transition.ResetTrigger("StartExit");
        yield return new WaitForSeconds(1);
        gameObject.transform.position = checkPoint.position;
        transition.ResetTrigger("Start");
        transition.SetTrigger("StartExit");
    }
}
