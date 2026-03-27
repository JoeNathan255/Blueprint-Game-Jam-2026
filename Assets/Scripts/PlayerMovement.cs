using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GameObject playerSprite;
    [SerializeField] private float speed = 1f;

    private Rigidbody2D rigidbody;
    private Animator animator;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = playerSprite.GetComponent<Animator>();
    }

    void Update()
    {
        float movX = Input.GetAxis("Horizontal");
        float movY = Input.GetAxis("Vertical");
        Move(movX, movY);
    }

    private void Move(float x, float y)
    {
        rigidbody.velocity = new Vector2(x, y).normalized * speed;

        if (x > 0 || y > 0 || x < 0 || y < 0)
        {
            animator.SetBool("IsWalking", true);
            animator.SetFloat("InputX", x);
            animator.SetFloat("InputY", y);
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }
    }
}
