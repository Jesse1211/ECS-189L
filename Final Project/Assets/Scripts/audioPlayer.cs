using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource background;
    public AudioSource playerSound;


    public static audioPlayer audioInstance;
    float timer = 0;
    void Start()
    {

        audioInstance = this;

    }

    public void playSound(string soundName)
    {

        AudioClip clip = Resources.Load<AudioClip>(soundName);
        playerSound.PlayOneShot(clip);
    }

}
