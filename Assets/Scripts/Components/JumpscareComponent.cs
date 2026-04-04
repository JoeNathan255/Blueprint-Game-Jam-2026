using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpscareComponent : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject sprite;
    [SerializeField] private float force;
    private Rigidbody2D entityRigidbody;
    private Animator entityAnimator;

    void Start()
    {
        entityRigidbody = GetComponent<Rigidbody2D>();
        entityAnimator = sprite.GetComponent<Animator>();
    }

    public void Jumpscare()
    {
        Debug.Log("AAAAAAHHHHHH!!!!!!!");
        entityRigidbody.AddForce(GetDirectionToTarget() * force);
    }

    private Vector2 GetDirectionToTarget()
    {
        Vector2 direction = target.transform.position - transform.position;
        return direction.normalized;
    }
}
