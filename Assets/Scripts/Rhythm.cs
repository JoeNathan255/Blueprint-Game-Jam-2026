using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

//using TMPro;
using UnityEngine;

public class Rhythm : MonoBehaviour
{

    //public TMP_Text BeatText;
    //public TMP_Text AccText;


    public float songBpm;

    //The number of seconds for each song beat
    public float secPerBeat;

    //the margin of error between a beat hit and miss, in seconds
    public static float tolerance = .1f;

    // the amount of beats that the pattern detailed in rhythmPattern goes on for before looping.
    // 4 for a song in 4/4, 3 for 3/4, 3.5 for 7/8, etc. does this make sense. i dont know.
    public float beatsPerMeasure;

    //Current song position, in seconds
    public float songPosition;

    //Current song position, in beats. 
    public float songPositionInBeats;

    // current song position, in measures.
    public float songPositionInMeasures;

    //How many seconds have passed since the song started
    public float dspSongTime;

    //an AudioSource attached to this GameObject that will play the music.
    public AudioSource musicSource;

    //the earliness/lateness of each input
    public float calibrationOffset = 0;

    /* pattern beats to track accuracy for!
    * EX: if i wanted to track beats on 1, the and of 2, and 3 for a 3/4 measure,
    * my array would look like [0 1.5 2]. BEATS START COUNTING AT ZERO !! */
    public List<float> rhythmPattern;

    //offset before first beat
    //THIS IS WEIRD RIGHT NOW i would try not to use it unless necessary
    public float offset;

    public bool swapComplete;

    //song position in the last frame. used to check if a beat was hit in between frames.
    public float lastFramePos;

    void Start()
    {
        musicSource = GetComponent<AudioSource>();
        secPerBeat = 60f / songBpm;
        //BeatText = GameObject.Find("BeatText").GetComponent<TMP_Text>();
        //AccText = GameObject.Find("AccuracyText").GetComponent<TMP_Text>();

        // things from here down shouldn't be called until the song STARTS. move to separate event?
        dspSongTime = (float)AudioSettings.dspTime;
        musicSource.Play();
        songPosition = offset * -1;

        Debug.Log("test!!");
    }

    void Update()
    {
        lastFramePos = songPosition;

        //add check to see if music has started yet.
        //determine how many seconds since the song started
        songPosition = (float)(AudioSettings.dspTime - dspSongTime - offset);
        //songPosition += Time.unscaledDeltaTime; (ticks up, but game starts 4 beats ahead?)

        //determine how many beats since the song started
        songPositionInBeats = songPosition / secPerBeat;

        // determines how many measures since the song started
        songPositionInMeasures = songPositionInBeats / beatsPerMeasure;

        //BeatText.text = ((Mathf.FloorToInt(songPositionInBeats - 1) % 4) + 1).ToString();

        if (Input.GetKeyDown("space"))
        {
            isBeatHit();
        }

        if ((beatsToMeasures(secsToBeats(lastFramePos)) % 2 > songPositionInMeasures % 2)
            && songPositionInMeasures > .01)

        {
            if (songBpm == 60)
            {
                songBpm = 120;
                secPerBeat = 60f / songBpm;
                beatsPerMeasure = 8;
                swapComplete = true;
            }
            else
            {
                songBpm = 60;
                secPerBeat = 60f / songBpm;
                beatsPerMeasure = 4;
                swapComplete = true;
            }
        }

    }

    public float secsToBeats(float secs)
    {
        return secs / secPerBeat;
    }

    public float beatsToSecs(float beats)
    {
        return secPerBeat * beats;
    }

    public float beatsToMeasures(float beats)
    {
        return beats / beatsPerMeasure;
    }

    public float measuresToBeats(float measures)
    {
        return measures * beatsPerMeasure;
    }

    // gets a beat in the current measure. for example, if you wanted beat 3 at measure value 3.75 in 4/4,
    // would return beat 15.
    public float currentMeasureBeat(float beat)
    {
        return measuresToBeats(Mathf.Floor(songPositionInMeasures)) + beat;
    }

    public void setBPM(float newbpm)
    {
        songBpm = newbpm;
        secPerBeat = 60f / newbpm;
    }

    public float accuracy()
    {
        float smallestDist = 999;
        float adjustedBeats = songPositionInBeats + secsToBeats(calibrationOffset);
        float adjustedMeasures = songPositionInMeasures + beatsToMeasures(secsToBeats(calibrationOffset));
        float closestBeat = 999;
        List<float> patternPlusNextBeat = rhythmPattern;
        patternPlusNextBeat.Add(patternPlusNextBeat[0] + beatsPerMeasure);

        foreach (float b in patternPlusNextBeat)
        {
            float beatInMeasure = currentMeasureBeat(b);
            if(Mathf.Abs(adjustedBeats - beatInMeasure) < smallestDist)
            {
                smallestDist = beatsToSecs(adjustedBeats - beatInMeasure);
                closestBeat = b;
            }
        }

        Debug.Log("accuracy: " + smallestDist);
        return smallestDist;
    }
    public bool beatPassed(float beat)
    {
        return beatsToSecs(beat) > lastFramePos && beatsToSecs(beat) <= songPosition;
    }

    public bool isBeatHit()
    {
        if(accuracy() <= tolerance)
        {
            Debug.Log("HIT!");
            return true;
        } else
        {
            Debug.Log("miss...");
            return false;
        }
       
    }

}
