using System.Collections;
using System.Collections.Generic;
using Captain.Command;
using UnityEngine;

/*
 *  works for a 5-10 second duration and produces 1 item every 2 seconds. 
 *  This is not the default command for all Pirates
 */
public class FastWorkerPirateCommand : ScriptableObject, IPirateCommand
{
    private float totalWorkDuration;
    private float totalWorkDone;
    private float currentWork;
    private const float PRODUCTION_TIME = 2.0f;
    private float timeCounter = PRODUCTION_TIME;
    private bool exhausted;

    public FastWorkerPirateCommand()
    {
        totalWorkDuration = randomTime(); // should be random number btn 5-10
        totalWorkDone = 0f;
        currentWork = 0;
        exhausted = false;
    }

    public bool Execute(GameObject pirate, UnityEngine.Object productPrefab)
    {
        //This function returns false when no work is done. 
        //After you implement work according to the specification and
        //generate instances of productPrefab, this function should return true.
        if (totalWorkDuration - totalWorkDone < PRODUCTION_TIME)
        {   // not enough time to produce more -> exhausted
            exhausted = true;
            return false;
        }

        timeCounter -= Time.deltaTime;
        if (timeCounter <= 0)
        {
            Instantiate(productPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            timeCounter = PRODUCTION_TIME;
            totalWorkDone += PRODUCTION_TIME;
            return true;
        }
        return true;
    }

    public float randomTime()   // should be random number btn 5-10
    => new System.Random().Next(5, 10);
}
