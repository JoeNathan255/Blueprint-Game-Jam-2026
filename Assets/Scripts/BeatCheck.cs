using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatCheck : MonoBehaviour
{
    static public List<float> calibrationBeats = new List<float>();
    static public float calibrationOffset;
    BeatCount beatCount;
    public float tolerance;
    void Start()
    {
        beatCount = GetComponent<BeatCount>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool isOnBeat()
    {
        return beatCount.timeBetweenBeats - beatCount.timeSinceLastBeat - calibrationOffset < tolerance 
            || beatCount.timeSinceLastBeat - calibrationOffset <= tolerance;
    }

    public float calibrateInput()
    {
        if (Mathf.Abs(beatCount.timeBetweenBeats - beatCount.timeSinceLastBeat) <= Mathf.Abs(beatCount.timeSinceLastBeat))
            calibrationBeats.Add(beatCount.timeBetweenBeats - beatCount.timeSinceLastBeat);
        else calibrationBeats.Add(beatCount.timeSinceLastBeat);

        float sum = 0f;
        foreach(float margin in calibrationBeats)
        {
            sum += margin;
        }

        calibrationOffset = sum / calibrationBeats.Count;

        return sum / calibrationBeats.Count;
    }
}
