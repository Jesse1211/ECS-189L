using System.Collections;
using System.Collections.Generic;
using Captain.Command;
using UnityEngine;

/*
 *  works for a 10-20 second duration and produces 1 item every 4 seconds. 
 *  This is not the default command for all Pirates
 */
public class NormalWorkerPirateCommand : ScriptableObject, IPirateCommand
{
    private float totalWorkDuration;
    private float totalWorkDone;
    private float currentWork;
    private const float PRODUCTION_TIME = 4.0f;
    private float timeCounter = PRODUCTION_TIME;
    private bool exhausted;

    public NormalWorkerPirateCommand()
    {
        totalWorkDuration = randomTime(); // should be random number btn 10-20
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

    public float randomTime()   // should be random number btn 10-20
    => new System.Random().Next(10, 20);
}
