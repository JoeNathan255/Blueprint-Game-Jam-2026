using System.Collections;
using System.Collections.Generic;
//using TMPro;
using UnityEngine;

public class Rhythm : MonoBehaviour
{

    //public TMP_Text BeatText;
    //public TMP_Text AccText;


    public float songBpm;

    //The number of seconds for each song beat
    public float secPerBeat;

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

    /* pattern beats to track accuracy for!
    * EX: if i wanted to track beats on 1, the and of 2, and 3 for a 3/4 measure,
    * my array would look like [0 1.5 2]. BEATS START COUNTING AT ZERO !! */
    public float[] rhythmPattern;

    //offset before first beat
    //THIS IS WEIRD RIGHT NOW i would try not to use it unless necessary
    public float offset;

    //this helps with the syncing a little bit i think?
    public float lastFrameTime;
    void Start()
    {
        musicSource = GetComponent<AudioSource>();
        secPerBeat = 60f / songBpm;
        //BeatText = GameObject.Find("BeatText").GetComponent<TMP_Text>();
        //AccText = GameObject.Find("AccuracyText").GetComponent<TMP_Text>();

        // things from here down shouldn't be called until the song STARTS. move to separate event?
        dspSongTime = (float)AudioSettings.dspTime;
        musicSource.Play();

        Debug.Log("test!!");
    }

    void Update()
    {
        //add check to see if music has started yet.
        //determine how many seconds since the song started
        songPosition = (float)(AudioSettings.dspTime - dspSongTime - offset);

        //determine how many beats since the song started
        songPositionInBeats = songPosition / secPerBeat;

        // determines how many measures since the song started
        songPositionInMeasures = songPositionInBeats / beatsPerMeasure;

        //BeatText.text = ((Mathf.FloorToInt(songPositionInBeats - 1) % 4) + 1).ToString();

        if (Input.GetKeyDown("space"))
        {
            accuracy();
        }
        if (lastFrameTime == songPosition)
            songPosition += Time.unscaledDeltaTime;

            
        lastFrameTime = songPosition;

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


    // gets the distance from the closest beat in rhythmPattern, in seconds.
    public float accuracy()
    {
        float smallestDist = 999;

        // just for debug logs, we can delete if necessary
        float closestBeat = 200;

        //goes through each beat in the pattern, finds the one closest to the current point in time, and stores the distance to it
        foreach (float b in rhythmPattern)
        {
            float distance = Mathf.Abs(songPositionInBeats - (Mathf.Floor(songPositionInMeasures) * beatsPerMeasure + b));            
            if (Mathf.Abs(songPositionInBeats - (measuresToBeats(Mathf.Floor(songPositionInMeasures)) + b)) < smallestDist)
            {
                smallestDist = distance;
                closestBeat = b;
            }
        }

        //checks the beat AFTER the current measure ends - otherwise it can't check for anything before the first beat of the patern
        if (Mathf.Abs(songPositionInBeats - (measuresToBeats(Mathf.Floor(songPositionInMeasures)) + beatsPerMeasure
            + rhythmPattern[0])) < smallestDist)
        {
            smallestDist = songPositionInBeats - (measuresToBeats(Mathf.Floor(songPositionInMeasures)) + beatsPerMeasure + rhythmPattern[0]);
            closestBeat = rhythmPattern[0] + beatsPerMeasure;
        }

        Debug.Log("accuracy: press was " + beatsToSecs(smallestDist) + "secs from beat " + closestBeat);
        return beatsToSecs(smallestDist);
    }

}
