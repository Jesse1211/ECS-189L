using System;
using System.Collections;
using System.Collections.Generic;
using Captain.Command;
using UnityEngine;


/*
 *  works for a 20-40 second duration and produces 1 item every 8 seconds. 
 *  This should be the default command for all Pirates other than the Captain.
 */
public class SlowWorkerPirateCommand : ScriptableObject, IPirateCommand
{
    private float totalWorkDuration;
    private float totalWorkDone;
    private float currentWork;
    private const float PRODUCTION_TIME = 8.0f;
    private float timeCounter = PRODUCTION_TIME;
    private bool exhausted;

    public SlowWorkerPirateCommand()
    {
        totalWorkDuration = randomTime(); // get exhausted when reaches
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
            var newPos = pirate.transform.position;
            newPos.x = newPos.x + 1;
            Instantiate(productPrefab, newPos, Quaternion.identity);
            //Instantiate(productPrefab, new Vector3(0, 1, 0), Quaternion.identity);

            timeCounter = PRODUCTION_TIME;
            totalWorkDone += PRODUCTION_TIME;
            return true;
        }
        return true;
    }

    public float randomTime()   // should be random number btn 20-40
        => new System.Random().Next(20, 40);
}
