using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPulse : MonoBehaviour
{
    BeatCount beatCounter;
    Image buttonFlash;
    // Start is called before the first frame update
    void Start()
    {
        beatCounter = GameObject.Find("Conductor").GetComponent<BeatCount>();
        buttonFlash = GameObject.Find("PlugFlash").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        buttonFlash.color = new Color(1, .958f, 0, ((beatCounter.timeBetweenBeats - beatCounter.timeSinceLastBeat) / beatCounter.timeBetweenBeats * 2));
    }
}
