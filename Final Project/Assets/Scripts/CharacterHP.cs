using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterHP : MonoBehaviour
{
    Slider HP;
    public int index;
   
    void Start()
    {
        HP = GetComponent<Slider>();
    }

    public void CollideThunder() 
    {
        HP.value -= 0.3f;

    }

    void Update()
    {
        if (HP.value <= 0)
        {
            LoadDeathScene();
        }
    }

    void LoadDeathScene() {
        SceneManager.LoadScene("DeathScene" + index);
    }

}
