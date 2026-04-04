using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementComponent), typeof(VisionComponent))]
public class EnemyController : MonoBehaviour
{
    public enum State { Patrol, Chase }
    private State currentState;

    [SerializeField] private AnimationComponent animationComponent;
    [SerializeField] private ChaseMovement chaseMovement;
    [SerializeField] private WaypointMovement waypointMovement;

    private MovementComponent movementComponent;
    private VisionComponent visionComponent;
    private Vector2 inputVector;

    public void Start()
    {
        SetState(State.Patrol);
        movementComponent = GetComponent<MovementComponent>();
        visionComponent = GetComponent<VisionComponent>();
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
        }

        movementComponent.Move(inputVector);
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
        SetState(State.Chase);
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
        }
    }

    private void EnterState(State state)
    {
        Debug.Log($"Entering {state}");

        switch (state)
        {
            case State.Patrol:
                break;
            case State.Chase:
                break;
        }
    }
}
