using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempoIncreasePoint : MonoBehaviour
{
    public float radius = 10f;
    void Update()
    {
        float distance = Vector2.Distance(transform.position, GlobalEvents.Instance.player.transform.position);
        //Debug.Log(distance);
    }
}
