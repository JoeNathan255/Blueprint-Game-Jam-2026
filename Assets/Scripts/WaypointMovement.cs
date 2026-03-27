using UnityEngine;

public class WaypointMovement : MonoBehaviour
{
    [SerializeField] private GameObject sprite;
    [SerializeField] private float speed = 1f;
    [SerializeField] private GameObject[] waypoints;
    [SerializeField] private float margin = 0.1f;

    private Rigidbody2D rigidbody;
    private Animator animator;
    private int nextWaypointIndex = 0;


    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = sprite.GetComponent<Animator>();
    }

    void Update()
    {
        if (IsAtNextWaypoint(margin))
        {
            nextWaypointIndex++; 
            nextWaypointIndex = (nextWaypointIndex < waypoints.Length) ? nextWaypointIndex : 0;
        }

        Move(GetDirectionToNextWaypoint());
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
        rigidbody.velocity = normalizedInputVec * speed;

        if (normalizedInputVec.x > 0 || normalizedInputVec.y > 0 || normalizedInputVec.x < 0 || normalizedInputVec.y < 0)
        {
            animator.SetBool("IsWalking", true);
            animator.SetFloat("InputX", normalizedInputVec.x);
            animator.SetFloat("InputY", normalizedInputVec.y);
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }
    }
}
