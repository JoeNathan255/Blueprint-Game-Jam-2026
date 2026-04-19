using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandingEnemy : BaseEnemy
{
    [SerializeField] private AnimationComponent animationComponent;
    [SerializeField] private float chaseSpeed = 20000f;
    [SerializeField] private float chaseTempoIncreaseRadius = 6;
    [SerializeField] private float chaseTempoIncreaseStrength = 15;
    [SerializeField] private float idleTempoIncreaseRadius = 3;
    [SerializeField] private float idleTempoIncreaseStrength = 5;

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
        if (!alive) { return; }

        //Debug.Log($"{this} action on beat");
        switch (currentState)
        {
            case State.Chase:
                tempoIncreaseRadius = chaseTempoIncreaseRadius;
                tempoIncreaseStrength = chaseTempoIncreaseStrength;
                TempoIncreaseCheck();
                inputVector = movementComponent.GetDirectionTo(chaseMovement.GetTarget());
                break;
            case State.Idle:
                tempoIncreaseRadius = idleTempoIncreaseRadius;
                tempoIncreaseStrength = idleTempoIncreaseStrength;
                TempoIncreaseCheck();
                return;
            case State.Disabled:
                return;
        }

        //Debug.Log($"{this} moves {inputVector} on beat");
        movementComponent.BeatMove(inputVector);
        animationComponent.UpdateAnimation(inputVector);

        if (currentState == State.Chase)
        {
            StopAttackingTarget();
        }
    }

    protected override void EnterState(State state)
    {
        if (!alive || animationComponent == null) { return; }
        //Debug.Log($"Entering {state}");

        switch (state)
        {
            case State.Idle:
                tempoIncreaseRadius = chaseTempoIncreaseRadius;
                tempoIncreaseStrength = chaseTempoIncreaseStrength;
                animationComponent.UpdateAnimation(Vector2.zero);
                break;
            case State.Chase:
                tempoIncreaseRadius = idleTempoIncreaseRadius;
                tempoIncreaseStrength = idleTempoIncreaseStrength;
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
