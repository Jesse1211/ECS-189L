using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToFight : MonoBehaviour
{
    // Start is called before the first frame update

   

    public void BackToFightScene(int scene)
    {
        SceneManager.LoadScene(scene);
        
    }
}
