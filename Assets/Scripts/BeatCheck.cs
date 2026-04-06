using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatCheck : MonoBehaviour
{
    // these are fake!!!! and we will get rid of them!! aah!!!!!!!
    float timeSinceLastBeat;
    float timeBetweenBeats;
    float tolerance;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool isOnBeat()
    {
        return timeBetweenBeats - timeSinceLastBeat < tolerance || timeSinceLastBeat <= tolerance;
    }
}
