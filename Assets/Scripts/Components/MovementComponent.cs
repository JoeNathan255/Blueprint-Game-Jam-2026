using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementComponent : MonoBehaviour
{
    public float speed = 25f;

    public bool isMoving { get; private set; } = false;
    private Rigidbody2D entityRigidbody;

    void Start()
    {
        entityRigidbody = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 normalizedInputVec)
    {
        entityRigidbody.AddForce(normalizedInputVec * speed);
        //entityRigidbody.velocity = normalizedInputVec * speed;

        if (normalizedInputVec.x > 0 || normalizedInputVec.y > 0 || normalizedInputVec.x < 0 || normalizedInputVec.y < 0)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
    }

    public Vector2 GetDirectionTo(GameObject target)
    {
        Vector2 direction = target.transform.position - this.transform.position;
        return direction.normalized;
    }
}
