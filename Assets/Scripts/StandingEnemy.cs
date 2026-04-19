using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandingEnemy : BaseEnemy
{
    [SerializeField] private AnimationComponent animationComponent;
    [SerializeField] private float chaseSpeed = 20000f;

    private Vector2 inputVector = new Vector2();


    void Start()
    {
        GlobalEvents.Instance.beatCount.OnBeat.AddListener(ActionOnBeat);
        defaultState = State.Idle;
        movementComponent = GetComponent<MovementComponent>();
        chaseMovement = GetComponent<ChaseMovement>();
        SetState(State.Idle);
    }

    public void ActionOnBeat()
    {
        //Debug.Log($"{this} action on beat");
        switch (currentState)
        {
            case State.Chase:
                TempoIncreaseCheck();
                inputVector = movementComponent.GetDirectionTo(chaseMovement.GetTarget());
                break;
            case State.Idle:
                return;
            case State.Disabled:
                return;
        }

        Debug.Log($"{this} moves {inputVector} on beat");
        movementComponent.BeatMove(inputVector);
        animationComponent.UpdateAnimation(inputVector);
    }

    protected override void EnterState(State state)
    {
        //Debug.Log($"Entering {state}");

        switch (state)
        {
            case State.Idle:
                animationComponent.UpdateAnimation(Vector2.zero);
                break;
            case State.Chase:
                movementComponent.force = chaseSpeed;
                break;
            case State.Disabled:
                animationComponent.UpdateAnimation(Vector2.zero);
                chaseMovement.Disable();
                movementComponent.Disable();
                movementComponent.AddRandomForce(5000f);
                break;
        }
    }
}
