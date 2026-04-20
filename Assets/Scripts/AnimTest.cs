using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimTest : MonoBehaviour
{
    public Vector2 inputVec;
    Animator entityAnimator;

    void Start()
    {
        entityAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float movX = Input.GetAxis("Horizontal");
        float movY = Input.GetAxis("Vertical");
        inputVec = new Vector2(movX, movY).normalized;

        UpdateAnimation(inputVec);
    }

    private void UpdateAnimation(Vector2 normalizedInputVec)
    {
        if (normalizedInputVec.x > 0 || normalizedInputVec.y > 0 || normalizedInputVec.x < 0 || normalizedInputVec.y < 0)
        {
            entityAnimator.SetBool("IsWalking", true);
            entityAnimator.SetTrigger("TrWalk");
            entityAnimator.SetFloat("InputX", normalizedInputVec.x);
            entityAnimator.SetFloat("InputY", normalizedInputVec.y);
        }
        else
        {
            entityAnimator.SetTrigger("TrIdle");
            entityAnimator.SetBool("IsWalking", false);
        }
    }
}
