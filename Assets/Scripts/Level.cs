using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] public BaseEnemy[] levelEnemies;
    [SerializeField] public bool isPlayerInLevel;

    void Start()
    {
        GlobalEvents.Instance.RegisterLevel(this);
    }

    void Update()
    {
        
    }

    public void OnPlayerOffBeat()
    {
        foreach (BaseEnemy enemy in levelEnemies)
        {
            //Debug.Log($"{enemy} aggro player");
            enemy.AggroPlayer();
        }
    }

    public void OnPlayerOnBeat()
    {
        foreach (BaseEnemy enemy in levelEnemies)
        {
            //Debug.Log($"{enemy} aggro player");
            enemy.DeaggroPlayer();
        }
    }
}
