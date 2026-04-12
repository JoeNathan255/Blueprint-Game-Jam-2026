using UnityEngine;

public class WaypointMovement : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    [SerializeField] private float margin = 0.1f;
    
    private int nextWaypointIndex = 0;

    void Update()
    {
        if (IsAtNextWaypoint(margin))
        {
            nextWaypointIndex++;
            nextWaypointIndex = (nextWaypointIndex < waypoints.Length) ? nextWaypointIndex : 0;
        }
    }

    public GameObject GetTargetWaypoint()
    {
        return waypoints[nextWaypointIndex];
    }

    private bool IsAtNextWaypoint(float margin)
    {
        return Vector2.Distance(transform.position, waypoints[nextWaypointIndex].transform.position) < margin;
    }
}