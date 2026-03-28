using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseMovement : MonoBehaviour
{
    [SerializeField] private GameObject sprite;
    [SerializeField] private float speed = 1f;
    [SerializeField] private bool isMovementSystemActive = true;
    [SerializeField] private GameObject target;
    [SerializeField] private float margin = 1f;

    private Rigidbody2D entityRigidbody;
    private Animator entityAnimator;

    void Start()
    {
        entityRigidbody = GetComponent<Rigidbody2D>();
        entityAnimator = sprite.GetComponent<Animator>();
    }

    void Update()
    {
        if (isMovementSystemActive)
        {
            if (IsTargetReached())
            {
                Debug.Log("Target Reached!");
            }
            else
            {
                Move(GetDirectionToTarget());
            }
        }
    }

    private Vector2 GetDirectionToTarget()
    {
        Vector2 direction = target.transform.position - transform.position;
        return direction.normalized;
    }

    private bool IsTargetReached()
    {
        return Vector2.Distance(transform.position, target.transform.position) < margin;
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
