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
    private List<Level> levels = new List<Level>();

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
        Debug.Log("Accuracy: " + beatCheck.accuracy().ToString("0.##") + (beatCheck.isOnBeat() ? "HIT!" : "miss..."));
        if (beatCheck.isOnBeat())
        {
            foreach (Level level in levels)
            {
                level.OnPlayerOnBeat();
            }
        }
        else
        {
            //Debug.Log("ATTACK");
            foreach (Level level in levels)
            {
                level.OnPlayerOffBeat();
            }
        }
    }

    public void OnContinuedMovement()
    {
        foreach (Level level in levels)
        {
            level.OnPlayerOffBeat();
        }
    }

    public void RegisterLevel(Level level)
    {
        levels.Add(level);
    }
}
