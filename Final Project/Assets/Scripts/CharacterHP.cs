using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterHP : MonoBehaviour
{
    // Start is called before the first frame update
    Slider HP;
   
    void Start()
    {
        HP = GetComponent<Slider>();
    }





    public void CollideThunder() 
    {
        HP.value -= 0.3f;

    }
    // Update is called once per frame
    void Update()
    {

       // HP.value -= Time.deltaTime;
        //print(HP.value);
        if (HP.value <= 0)
        {

            // move to the death scene 
            LoadDeathScene();
        }
    }

    void LoadDeathScene() {
        SceneManager.LoadScene("DeathScene");
    }

}
