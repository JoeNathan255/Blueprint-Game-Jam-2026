using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] public BaseEnemy[] levelEnemies;
    [SerializeField] private bool isPlayerInLevel;

    void Start()
    {
        GlobalEvents.Instance.RegisterLevel(this);
    }

    void Update()
    {

    }

    public void OnPlayerOffBeat()
    {
        if (!isPlayerInLevel) { return; }

        foreach (BaseEnemy enemy in levelEnemies)
        {
            //Debug.Log($"{enemy} aggro player");
            enemy.AttackTarget();
        }
    }

    public void OnPlayerOnBeat()
    {
        if (!isPlayerInLevel) { return; }

        foreach (BaseEnemy enemy in levelEnemies)
        {
            //Debug.Log($"{enemy} aggro player");
            enemy.StopAttackingTarget();
        }
    }

    public void SetIsPlayerInLevel(bool isplayerInLevel)
    {
        isPlayerInLevel = isplayerInLevel;
        if (!isPlayerInLevel)
        {
            foreach (BaseEnemy enemy in levelEnemies)
            {
                enemy.StopAttackingTarget();
            }
        }
    }
}
