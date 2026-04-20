using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTempoMinArea : MonoBehaviour
{
    public float newTempoMin = 60f;

    public void OnPlayerEnter()
    {
        GlobalEvents.Instance.minTempo = newTempoMin;
    }
}
