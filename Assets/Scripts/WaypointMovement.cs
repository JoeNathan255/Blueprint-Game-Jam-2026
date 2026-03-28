using UnityEngine;

public class WaypointMovement : MonoBehaviour
{
    [SerializeField] private GameObject sprite;
    [SerializeField] private float speed = 1f;
    [SerializeField] private GameObject[] waypoints;
    [SerializeField] private float margin = 0.1f;
    [SerializeField] private bool isMovementSystemActive = true;

    private Rigidbody2D entityRigidbody;
    private Animator entityAnimator;
    private int nextWaypointIndex = 0;

    void Start()
    {
        entityRigidbody = GetComponent<Rigidbody2D>();
        entityAnimator = sprite.GetComponent<Animator>();
    }

    void Update()
    {
        if (isMovementSystemActive)
        {
            if (IsAtNextWaypoint(margin))
            {
                nextWaypointIndex++;
                nextWaypointIndex = (nextWaypointIndex < waypoints.Length) ? nextWaypointIndex : 0;
            }

            Move(GetDirectionToNextWaypoint());
        }
    }

    private bool IsAtNextWaypoint(float margin)
    {
        return Vector2.Distance(transform.position, waypoints[nextWaypointIndex].transform.position) < margin;
    }

    private Vector2 GetDirectionToNextWaypoint()
    {
        Vector2 direction = waypoints[nextWaypointIndex].transform.position - transform.position;
        return direction.normalized;
    }

    private void Move(Vector2 normalizedInputVec)
    {
        entityRigidbody.velocity = normalizedInputVec * speed;

        if (normalizedInputVec.x > 0 || normalizedInputVec.y > 0 || normalizedInputVec.x < 0 || normalizedInputVec.y < 0)
        {
            entityAnimator.SetBool("IsWalking", true);
            entityAnimator.SetFloat("InputX", normalizedInputVec.x);
            entityAnimator.SetFloat("InputY", normalizedInputVec.y);
        }
        else
        {
            entityAnimator.SetBool("IsWalking", false);
        }
    }
}