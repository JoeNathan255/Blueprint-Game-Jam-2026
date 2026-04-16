using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TriggerArea : MonoBehaviour
{
    public Action<GameObject> OnEnter;
    public Action<GameObject> OnExit;

    public void OnTriggerEnter2D(Collider2D collider)
    {
        OnEnter?.Invoke(collider.gameObject);
    }

    public void OnTriggerExit2D(Collider2D collider)
    {
        OnExit?.Invoke(collider.gameObject);
    }

    public void DestroyArea()
    {
        Destroy(gameObject);
    }
}
