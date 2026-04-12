using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChaseMovement : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private float margin = 2f;
    
    public UnityEvent TargetReached;
    private bool active = true;

    private bool oneShot = true;

    void Update()
    {
        if (IsTargetReached() && active)
        {
            if (oneShot)
            {
                Debug.Log("Target Reached!");
                TargetReached?.Invoke();
            }
            oneShot = false;
        }
        else
        {
            oneShot = true;
        }
    }

    public GameObject GetTarget()
    {
        return target;
    }

    private bool IsTargetReached()
    {
        return Vector2.Distance(transform.position, target.transform.position) < margin;
    }

    public void Disable()
    {
        active = false;
    }
}
