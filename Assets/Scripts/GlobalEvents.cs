using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalEvents : MonoBehaviour
{
    [SerializeField] private string gameOverScene = "DeathScreen";

    public static GlobalEvents Instance;

    public static void BroadcastGameOver()
    {
        SceneManager.LoadScene(Instance.gameOverScene);
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
}
