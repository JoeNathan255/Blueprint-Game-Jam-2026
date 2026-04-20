using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GlobalEvents : MonoBehaviour
{
    public PlayerController player;
    public float minTempo = 60;
    public float maxTempo = 180;
    [SerializeField] private string gameOverScene = "DeathScreen";
    [SerializeField] private string winScene = "WinScreen";
    public BeatCheck beatCheck;
    public BeatCount beatCount;

    public static GlobalEvents Instance;

    public UnityEvent PlayerInput;
    private List<Level> levels = new List<Level>();
    public float nextTempoIncrease = 0;

    void Start()
    {
        beatCount = GetComponent<BeatCount>();
        beatCheck = GetComponent<BeatCheck>();
        beatCount.OnBeat.AddListener(OnBeatGlobal);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("increase tempo");
            SetNextTempoIncrease(20);
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("decrease tempo");
            SetNextTempoIncrease(-20);
        }
    }

    public static void BroadcastGameOver()
    {
        SceneManager.LoadScene(Instance.gameOverScene);
    }

    public static void BroadcastGameWon()
    {
        SceneManager.LoadScene(Instance.winScene);
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
        Debug.Log("Accuracy: " + beatCheck.accuracy().ToString("0.##") + (beatCheck.isOnBeat() ? "HIT!" : "miss...") + "0 - " + beatCount.timeBetweenBeats);
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

    public void OnBeatGlobal()
    {
        Debug.Log("Beat");
        SetNewTempo();
    }

    public void SetNextTempoIncrease(float increase)
    {
        nextTempoIncrease = increase;
    }

    private void SetNewTempo()
    {
        beatCount.tempo = Mathf.Clamp(beatCount.tempo + nextTempoIncrease, minTempo, maxTempo);
        nextTempoIncrease = 0;
    }
}
