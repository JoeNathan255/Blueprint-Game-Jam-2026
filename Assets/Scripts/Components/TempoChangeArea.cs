using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempoChangeArea : MonoBehaviour
{
    public float tempoChangeAmount = 20f;

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GlobalEvents.Instance.SetNextTempoIncrease(tempoChangeAmount);
        }
    }

    public void OnPlayerEnter()
    {
        GlobalEvents.Instance.SetNextTempoIncrease(tempoChangeAmount);
    }

    public void OnPlayerExit()
    {
        GlobalEvents.Instance.SetNextTempoIncrease(0);
    }
}
