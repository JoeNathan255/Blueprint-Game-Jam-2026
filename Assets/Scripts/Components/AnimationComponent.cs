using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Sprite), typeof(Animator))]
public class AnimationComponent : MonoBehaviour
{
    private Sprite entitySprite;
    private Animator entityAnimator;

    void Start()
    {
        entitySprite = GetComponent<Sprite>();
        entityAnimator = GetComponent<Animator>();
    }

    public void UpdateAnimation(Vector2 normalizedInputVec)
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
