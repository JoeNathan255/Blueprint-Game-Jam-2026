using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Canvas))]
public class TutorialUIComponent : MonoBehaviour
{
    public UnityEvent OnTutorialOver;
    private Canvas canvas;
    private bool timerActive = false;
    private float timerThreshold = 0f;
    private float timer = 0f;

    void Start()
    {
        canvas = GetComponent<Canvas>();
    }

    void Update()
    {
        if (timerActive)
        {
            if (timer < timerThreshold)
            {
                timer += Time.deltaTime;
            }
            else
            {
                Hide();
                timer = 0f;
                timerActive = false;
            }

        }
    }

    public void Show(float time)
    {
        canvas.enabled = true;
        timerThreshold = time;
        timerActive = true;
    }

    public void Show()
    {
        canvas.enabled = true;
    }

    public void Hide()
    {
        OnTutorialOver?.Invoke();
        canvas.enabled = false;
    }
}
