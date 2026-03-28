using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionArea : MonoBehaviour
{
    public Action OnEnter;
    public Action OnExit;

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.GetComponent<PlayerMovement>())
        {
            Debug.Log("Player Detected");
        }
    }

    public void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.GetComponent<PlayerMovement>())
        {
            Debug.Log("Player Lost");
        }
    }
}
