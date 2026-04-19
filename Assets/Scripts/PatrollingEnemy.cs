using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WaypointMovement))]
public class PatrollingEnemy : BaseEnemy
{
    [SerializeField] private AnimationComponent animationComponent;
    [SerializeField] private float patrolSpeed = 5000f;
    [SerializeField] private float chaseSpeed = 20000f;

    private WaypointMovement waypointMovement;
    private Vector2 inputVector = new Vector2();

    public void Start()
    {
        defaultState = State.Patrol;
        movementComponent = GetComponent<MovementComponent>();
        chaseMovement = GetComponent<ChaseMovement>();
        waypointMovement = GetComponent<WaypointMovement>();
        SetState(State.Patrol);
    }

    public void Update()
    {
        switch (currentState)
        {
            case State.Patrol:
                inputVector = movementComponent.GetDirectionTo(waypointMovement.GetTargetWaypoint());
                break;
            case State.Chase:
                inputVector = movementComponent.GetDirectionTo(chaseMovement.GetTarget());
                break;
            case State.Disabled:
                return;
        }

        movementComponent.StepMove(inputVector);
        animationComponent.UpdateAnimation(inputVector);
    }

    protected override void EnterState(State state)
    {
        Debug.Log($"Entering {state}");

        switch (state)
        {
            case State.Patrol:
                movementComponent.force = patrolSpeed;
                break;
            case State.Chase:
                movementComponent.force = chaseSpeed;
                break;
            case State.Disabled:
                movementComponent.AddRandomForce(5000f);
                animationComponent.UpdateAnimation(Vector2.zero);
                chaseMovement.Disable();
                movementComponent.Disable();
                break;
        }
    }
}
