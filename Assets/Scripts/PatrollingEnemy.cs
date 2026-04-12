using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementComponent), typeof(VisionComponent))]
public class PatrollingEnemy : MonoBehaviour
{
    public enum State { Patrol, Chase, Disabled }
    private State currentState;

    [SerializeField] private AnimationComponent animationComponent;
    [SerializeField] private ChaseMovement chaseMovement;
    [SerializeField] private WaypointMovement waypointMovement;
    [SerializeField] private float patrolSpeed = 15000f;
    [SerializeField] private float chaseSpeed = 40000f;

    private MovementComponent movementComponent;
    private VisionComponent visionComponent;
    private Vector2 inputVector = new Vector2();

    public void Start()
    {
        movementComponent = GetComponent<MovementComponent>();
        visionComponent = GetComponent<VisionComponent>();
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
        visionComponent.SetLookDirection(inputVector);
    }

    public State GetState()
    {
        return currentState;
    }

    public void SetState(State state)
    {
        ExitState(currentState);
        currentState = state;
        EnterState(currentState);
    }

    public void OnPlayerDetected()
    {
        Debug.Log("Player Detected");
        if (currentState == State.Patrol)
        {
            SetState(State.Chase);
        }
    }

    public void OnPlayerLost()
    {
        //Debug.Log("Player Lost");
        //SetState(State.Patrol);
    }

    private void ExitState(State state)
    {
        switch (state)
        {
            case State.Patrol:
                break;
            case State.Chase:
                break;
            case State.Disabled:
                break;
        }
    }

    private void EnterState(State state)
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
                movementComponent.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-5000f, 5000f), Random.Range(-5000f, 5000f)));
                animationComponent.UpdateAnimation(Vector2.zero);
                chaseMovement.Disable();
                movementComponent.Disable();
                break;
        }
    }

    public void Disable()
    {
        Debug.Log($"{this} was disabled");
        SetState(State.Disabled);
    }
}
