using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionComponent : MonoBehaviour
{
    [SerializeField] private VisionArea visionArea;
    [SerializeField] private GameObject visionPivot;

    private Collider2D visionCollider;

    public void Start()
    {
        visionCollider = visionArea.GetComponent<Collider2D>();
    }

    public void SetLookDirection(Vector2 dir)
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        visionPivot.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
