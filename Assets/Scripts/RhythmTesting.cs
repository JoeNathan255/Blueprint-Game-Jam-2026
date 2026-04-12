using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmTesting : MonoBehaviour
{

    BeatCheck beatChecker;
    BeatCount beatCounter;
    public float visBarPos;

    // this one is just stuff for testing and should not be used in the actual game 

    void Start()
    {
        beatChecker = GetComponent<BeatCheck>();
        beatCounter = GetComponent<BeatCount>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("space"))
        {
            Debug.Log("Accuracy: " + accuracy().ToString("0.##") + (beatChecker.isOnBeat() ? "HIT!" : "miss..."));
            visBarPos = accuracy();
        }
        if(Input.GetKeyDown("left shift"))
        {
            beatChecker.calibrateInput();
            Debug.Log("Calibrated! Tolerance: " + beatChecker.calibrationOffset);
        }
    }

    public float accuracy()
    {
        if (Mathf.Abs(beatCounter.timeBetweenBeats - beatCounter.timeSinceLastBeat - beatChecker.calibrationOffset)
            <= Mathf.Abs(beatCounter.timeSinceLastBeat - beatChecker.calibrationOffset))
            return beatCounter.timeBetweenBeats - beatCounter.timeSinceLastBeat;
        else return beatCounter.timeSinceLastBeat;
    }
}
