using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementComponent), typeof(ChaseMovement), typeof(VisionComponent))]
public class StandingEnemy : BaseEnemy
{
    [SerializeField] private AnimationComponent animationComponent;
    [SerializeField] private float chaseSpeed = 40000f;

    private MovementComponent movementComponent;
    private ChaseMovement chaseMovement;
    private VisionComponent visionComponent;
    private Vector2 inputVector = new Vector2();


    void Start()
    {
        movementComponent = GetComponent<MovementComponent>();
        chaseMovement = GetComponent<ChaseMovement>();
        visionComponent = GetComponent<VisionComponent>();
    }

    void Update()
    {
        switch (currentState)
        {
            case State.Chase:
                inputVector = movementComponent.GetDirectionTo(chaseMovement.GetTarget());
                break;
            case State.Idle:
                return;
            case State.Disabled:
                return;
        }

        movementComponent.StepMove(inputVector);
        animationComponent.UpdateAnimation(inputVector);
    }

    public void OnPlayerDetected()
    {
        Debug.Log("Player Detected");
        if (currentState != State.Disabled)
        {
            SetState(State.Chase);
        }
    }

    public void OnPlayerLost()
    {
        Debug.Log("Player Lost");
        if (currentState != State.Disabled)
        {
            SetState(State.Idle);
        }
    }

    protected override void EnterState(State state)
    {
        Debug.Log($"Entering {state}");

        switch (state)
        {
            case State.Idle:
                movementComponent.Disable();
                chaseMovement.Disable();
                break;
            case State.Chase:
                movementComponent.force = chaseSpeed;
                break;
            case State.Disabled:
                movementComponent.AddRandomForce(5000f);
                break;
        }
    }

    protected override void ExitState(State state)
    {
        switch (state)
        {
            case State.Chase:
                animationComponent.UpdateAnimation(Vector2.zero);
                chaseMovement.Disable();
                movementComponent.Disable();
                break;
        }
    }
}
