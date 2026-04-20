using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChaseMovement : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private float margin = 2f;

    public UnityEvent TargetReached;

    public GameObject GetTarget()
    {
        return target;
    }

    public void SetTarget(GameObject newTarget)
    {
        target = newTarget.gameObject;
        //Debug.Log($"Targeting {target}, which is {Vector2.Distance(transform.position, target.transform.position)} units away from this");
    }
}
