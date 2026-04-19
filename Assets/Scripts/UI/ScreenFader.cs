// Source - https://stackoverflow.com/a/59748498
// Posted by neuromouse
// Retrieved 2026-04-17, License - CC BY-SA 4.0

using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneFader : MonoBehaviour
{
    enum FadeStatus
    {
        fading_id,
        fading_out,
        none
    }

    public static SceneFader Instance;
    public Image fadeImage;
    public float fadeDuration = 1;

    private FadeStatus currentFadeStatus = FadeStatus.none;
    private float fadeTimer;
    private string sceneToLoad;

    void Start()
    {
        fadeImage = GameObject.Find("Fade").GetComponent<Image>();

        if (Instance == null)
        {
            Instance = this;

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(GameObject.Find("FadeCanvas"));
    }


    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //scene loaded, running fade-in
        currentFadeStatus = FadeStatus.fading_id;
    }

    public void ChangeScene(string _name)
    {
        sceneToLoad = _name;
        currentFadeStatus = FadeStatus.fading_out;
    }

    void Update()
    {
        if (currentFadeStatus != FadeStatus.none)
        {
            fadeTimer += Time.deltaTime;

            if (fadeTimer > fadeDuration) //done fading
            {
                fadeTimer = 0;

                if (currentFadeStatus == FadeStatus.fading_out)
                {
                    SceneManager.LoadScene(sceneToLoad);
                    fadeImage.color = Color.black;
                }
                else
                    fadeImage.color = Color.clear;

                currentFadeStatus = FadeStatus.none;
            }
            else //still fading
            {
                float alpha = 0;
                if (currentFadeStatus == FadeStatus.fading_out)
                    alpha = Mathf.Lerp(0, 1, fadeTimer / fadeDuration);
                else
                    alpha = Mathf.Lerp(1, 0, fadeTimer / fadeDuration);

                fadeImage.color = new Color(0, 0, 0, alpha);
            }
        }
    }
}
