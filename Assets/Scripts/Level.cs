using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    public float levelMinTempo = 60;
    public float TempoDecreaseValue = -10;
    [SerializeField] public BaseEnemy[] levelEnemies;
    [SerializeField] private bool isPlayerInLevel;

    void Start()
    {
        GlobalEvents.Instance.RegisterLevel(this);
    }

    void Update()
    {
        if (isPlayerInLevel)
        {
            GlobalEvents.Instance.minTempo = levelMinTempo;
            DecreaseTempoIfNoEnemiesNear();
        }
    }

    public void OnPlayerOffBeat()
    {
        if (!isPlayerInLevel) { return; }

        GameObject.Find("HitOrMiss").GetComponent<Image>().color = new Color(1, 0, 0, .5f); // THIS IS DEBUG AHHHHH!!!!!!

        foreach (BaseEnemy enemy in levelEnemies)
        {
            //Debug.Log($"{enemy} aggro player");
            enemy.AttackTarget();
        }
    }

    public void OnPlayerOnBeat()
    {
        if (!isPlayerInLevel) { return; }
        GameObject.Find("HitOrMiss").GetComponent<Image>().color = new Color(0, 1, 0, .5f); // THIS IS DEBUG AHHHHH!!!!!

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

    private void DecreaseTempoIfNoEnemiesNear()
    {
        foreach (BaseEnemy enemy in levelEnemies)
        {
            if (Vector2.Distance(enemy.transform.position, GlobalEvents.Instance.player.transform.position) < enemy.getTempoIncreaseRadius())
            {
                return;
            }
        }

        GlobalEvents.Instance.SetNextTempoIncrease(TempoDecreaseValue);
    }
}
