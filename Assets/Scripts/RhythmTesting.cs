using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class RhythmTesting : MonoBehaviour
{

    BeatCheck beatChecker;
    BeatCount beatCounter;
    public float visBarPos;

    // this one is just stuff for testing and should not be used in the actual game 


    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        beatCounter.counting = true;
    }

    void Start()
    {
        beatChecker = GetComponent<BeatCheck>();
        beatCounter = GetComponent<BeatCount>();
        Debug.Log(BeatCheck.calibrationOffset);
        beatCounter.playBeats();
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
            Debug.Log("Calibrated! Tolerance: " + BeatCheck.calibrationOffset);
        }
    }

    public float accuracy()
    {
        if (Mathf.Abs(beatCounter.timeBetweenBeats - beatCounter.timeSinceLastBeat - BeatCheck.calibrationOffset)
            <= Mathf.Abs(beatCounter.timeSinceLastBeat - BeatCheck.calibrationOffset))
            return beatCounter.timeBetweenBeats - beatCounter.timeSinceLastBeat;
        else return beatCounter.timeSinceLastBeat;
    }
}
