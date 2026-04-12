using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Events;

public class VisionComponent : MonoBehaviour
{
    [SerializeField] private TriggerArea visionArea;
    [SerializeField] private GameObject visionPivot;
    [SerializeField] private string detectionTag;

    public UnityEvent ObjectDetected;
    public UnityEvent ObjectLost;

    public void Start()
    {
        visionArea.OnEnter += OnObjectEntered;
        visionArea.OnExit += OnObjectExited;
    }

    public void OnDestroy()
    {
        visionArea.OnEnter -= OnObjectEntered;
        visionArea.OnExit -= OnObjectExited;
    }

    public void SetLookDirection(Vector2 dir)
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        visionPivot.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void OnObjectEntered(GameObject gameObject)
    {
        if (gameObject.tag == detectionTag)
        {
            ObjectDetected?.Invoke();
        }
    }

    public void OnObjectExited(GameObject gameObject)
    {
        if (gameObject.tag == detectionTag)
        {
            ObjectLost?.Invoke();
        }
    }
}
