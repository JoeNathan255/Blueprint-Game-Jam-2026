using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CalibrationController : MonoBehaviour

{
    MenuStage currentMenu = MenuStage.stage1;
    TextMeshProUGUI text1;
    TextMeshProUGUI text2a;
    TextMeshProUGUI text2b;
    TextMeshProUGUI text3;
    TextMeshProUGUI text4;
    Button accept4;
    Button repeat4;
    BeatCheck beatChecker;
    BeatCount beatCounter;
    Image visualizer3;

    enum MenuStage // this is how i'm gonna track where in the menu we are. each one corresponds to different text/buttons/etc activated.
    {
        stage1, // WAKE UP.
        stage2a, // Your heart stirs.
        stage2b, // Your heart stirs. Give it power.
        stage3, // Press any button in time with the music. (Calibration happens here).
        stage4 // Your offset is .XXX seconds. ACCEPT (starts next scene) / REPEAT (return to text3)
    }
    void Start()
    {
        // is there a better way to do this? probably. i'm happy to edit if so!
        text1 = GameObject.Find("Text1").GetComponent<TextMeshProUGUI>();
        text2a = GameObject.Find("Text2a").GetComponent<TextMeshProUGUI>();
        text2b = GameObject.Find("Text2b").GetComponent<TextMeshProUGUI>();
        text3 = GameObject.Find("Text3").GetComponent<TextMeshProUGUI>();
        text4 = GameObject.Find("Text4").GetComponent<TextMeshProUGUI>();
        accept4 = GameObject.Find("Accept4").GetComponent<Button>();
        repeat4 = GameObject.Find("Repeat4").GetComponent<Button>();
        beatChecker = GameObject.Find("Conductor").GetComponent<BeatCheck>();
        beatCounter = GameObject.Find("Conductor").GetComponent<BeatCount>();
        visualizer3 = GameObject.Find("Visualizer3").GetComponent<Image>();
    }

    void Update()
    {
        switch(currentMenu)
        {
            case MenuStage.stage1: // shows text1 and waits for next input
                text1.enabled = true;
                text2a.enabled = false;
                text2b.enabled = false;
                text3.enabled = false;
                text4.enabled = false;
                accept4.enabled = false;
                repeat4.enabled = false;
                visualizer3.enabled = false;
                accept4.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
                repeat4.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
                if (Input.anyKeyDown)
                    currentMenu = MenuStage.stage2a;
                break;

            case MenuStage.stage2a: // shows text2a and waits for next input
                text1.enabled = false;
                text2a.enabled = true;
                text2b.enabled = false;
                text3.enabled = false;
                text4.enabled = false;
                accept4.enabled = false;
                repeat4.enabled = false;
                visualizer3.enabled = false;

                accept4.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
                repeat4.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
                if (Input.anyKeyDown)
                    currentMenu = MenuStage.stage2b;
                break;

            case MenuStage.stage2b: // shows text2a + text2b and waits for next input 
                text1.enabled = false;
                text2a.enabled = true;
                text2b.enabled = true;
                text3.enabled = false;
                text4.enabled = false;
                accept4.enabled = false;
                repeat4.enabled = false;
                visualizer3.enabled = false;

                accept4.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
                repeat4.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
                if (Input.anyKeyDown)
                    currentMenu = MenuStage.stage3;
                break;

            case MenuStage.stage3: // shows text3, starts audio and beat counting. calibrates 10 beats before advancing.
                text1.enabled = false;
                text2a.enabled = false;
                text2b.enabled = false;
                text3.enabled = true;
                text4.enabled = false;
                accept4.enabled = false;
                repeat4.enabled = false;
                visualizer3.enabled = true;

                accept4.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
                repeat4.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
                if (!beatCounter.GetComponentInParent<AudioSource>().isPlaying)
                    // if the audio isn't already playing, start counting beats.
                    beatCounter.playBeats();
                if (Input.anyKeyDown) {
                    // if any key is pressed, do a calibration beat.
                    if (beatChecker.isOnBeat())
                        visualizer3.color = new Color(0, 1, 0, 1);
                    if (!beatChecker.isOnBeat())
                        visualizer3.color = new Color(1, 0, 0, 1);
                        beatChecker.calibrateInput();
                    }
                if (BeatCheck.calibrationBeats.Count >= 10)
                {
                    // after 10 inputs, stop counting and move to stage 4
                    beatCounter.stopBeats();
                    currentMenu = MenuStage.stage4;
                }
                break;

            case MenuStage.stage4: // enables text4, accept4, and repeat4. further action handled from button events
                text1.enabled = false;
                text2a.enabled = false;
                text2b.enabled = false;
                text3.enabled = false;
                text4.enabled = true;
                accept4.enabled = true;
                repeat4.enabled = true;
                visualizer3.enabled = false;
                accept4.GetComponentInChildren<TextMeshProUGUI>().enabled = true;
                repeat4.GetComponentInChildren<TextMeshProUGUI>().enabled = true;

                text4.SetText("Your offset is " + BeatCheck.calibrationOffset.ToString("0.###") + '.');
                break;

            default:
                currentMenu = MenuStage.stage1;
                break;
        }
    }

    public void OnAcceptButtonClicked()
    {
        if (GameObject.Find("UIController") != null)
            GameObject.Find("UIController").GetComponent<SceneFader>().ChangeScene("TopDownScene");
        else SceneManager.LoadScene("TopDownScene");
            return;
    }

    public void OnRepeatButtonClicked()
    {
        BeatCheck.calibrationBeats = new List<float>();
        BeatCheck.calibrationOffset = 0;
        currentMenu = MenuStage.stage3;

        return;

    }
}
