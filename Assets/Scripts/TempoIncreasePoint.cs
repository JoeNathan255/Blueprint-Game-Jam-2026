using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempoIncreasePoint : MonoBehaviour
{
    public float radius = 10f;
    void FixedUpdate()
    {
        float distance = Vector2.Distance(transform.position, GlobalEvents.Instance.player.transform.position);
        if (distance < radius)
        {
            float nTempo = GlobalEvents.Instance.maxTempo - (GlobalEvents.Instance.maxTempo - GlobalEvents.Instance.minTempo) * distance / radius;
            GlobalEvents.Instance.SetNextTempo(nTempo, distance);
        }
    }
}
