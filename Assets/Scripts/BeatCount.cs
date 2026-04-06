using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatCount : MonoBehaviour
{
    public float tempo;
    public float timeSinceLastBeat;
    public float timeBetweenBeats;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastBeat += Time.deltaTime;
        timeBetweenBeats = 60f / tempo;
        if (timeSinceLastBeat >= timeBetweenBeats)
        {
            Debug.Log("Beat!");
            timeSinceLastBeat = 0;
            // maybe this is what we want though? timeSinceLastBeat -= timeBetweenBeats;
        }
    }
}
