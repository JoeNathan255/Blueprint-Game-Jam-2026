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
    public bool counting = false;
    AudioSource beats;

    private void OnEnable()
    {
        
    }
    void Start()
    {
        beats = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (timeAfterLoad == -2)
        {
            timeAfterLoad = Time.timeSinceLevelLoad;
            timeSinceLastBeat += timeAfterLoad * -1;
        }*/
        if (counting)
        {
            timeSinceLastBeat += Time.deltaTime;
            timeBetweenBeats = 60f / tempo;
            if (timeSinceLastBeat >= timeBetweenBeats)
            {
                Debug.Log("Beat!");
                OnBeat?.Invoke();
                timeSinceLastBeat -= timeBetweenBeats;
                beatNumber += 1;
            }
        }
    }

    public void pauseBeats()
    {
        counting = false;
        beats.Pause();
    }

    public void playBeats()
    {
        counting = true;
        beats.Play();
    }

    public void stopBeats()
    {
        counting = false;
        beatNumber = -1;
        timeSinceLastBeat = 0;
        beats.Stop();
    }
}
