using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatCount : MonoBehaviour
{
    public float tempo;
    public float timeSinceLastBeat;
    public float timeBetweenBeats;
    public int beatNumber = -1;
    float timeAfterLoad = -2;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timeAfterLoad == -2)
        {
            timeAfterLoad = Time.timeSinceLevelLoad;
            timeSinceLastBeat += timeAfterLoad * -1;
        }
        timeSinceLastBeat += Time.deltaTime;
        timeBetweenBeats = 60f / tempo;
        if (timeSinceLastBeat >= timeBetweenBeats)
        {
            Debug.Log("Beat!");
            timeSinceLastBeat -= timeBetweenBeats;
            beatNumber += 1;
        }
    }
}
