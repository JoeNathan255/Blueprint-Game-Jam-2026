using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BeatCount : MonoBehaviour
{
    public UnityEvent OnBeat;
    public float tempo;
    public float timeSinceLastBeat;
    public float timeBetweenBeats;
    public int beatNumber = -1;
    float timeAfterLoad = -2;

    void Update(){

        if (timeAfterLoad == -2)
        {
            timeAfterLoad = Time.timeSinceLevelLoad;
            timeSinceLastBeat += timeAfterLoad * -1;
        }
        
        timeSinceLastBeat += Time.smoothDeltaTime;
        timeBetweenBeats = 60f / tempo;
        if (timeSinceLastBeat >= timeBetweenBeats)
        {
            //Debug.Log("Beat!");
            OnBeat?.Invoke();
            //timeSinceLastBeat -= timeBetweenBeats;
            timeSinceLastBeat = 0f;
            beatNumber += 1;
        }
    }
}
