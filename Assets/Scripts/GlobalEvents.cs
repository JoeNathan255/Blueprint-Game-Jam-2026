using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GlobalEvents : MonoBehaviour
{
    [SerializeField] private string gameOverScene = "DeathScreen";
    [SerializeField] private BeatCheck beatCheck;
    [SerializeField] private BeatCount beatCount;

    public static GlobalEvents Instance;

    public UnityEvent PlayerInput;

    public static void BroadcastGameOver()
    {
        SceneManager.LoadScene(Instance.gameOverScene);
    }

    public static void BroadcastPlayerInput()
    {
        Instance.PlayerInput?.Invoke();
    }

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void OnPlayerInput()
    {
        //Debug.Log("Accuracy: " + beatCheck.accuracy().ToString("0.##") + (beatCheck.isOnBeat() ? "HIT!" : "miss..."));
        if (!beatCheck.isOnBeat())
        {
            Debug.Log("ATTACK");
        }
    }


}
