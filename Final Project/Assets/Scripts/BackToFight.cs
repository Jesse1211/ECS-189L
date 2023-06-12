using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToFight : MonoBehaviour
{  
    public void BackToFightScene(int scene)
    {
        SceneManager.LoadScene(scene);
        
    }
}
