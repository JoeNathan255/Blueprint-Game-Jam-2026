using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public enum State { Patrol, Chase }
    private State currentState;

    [SerializeField] private ChaseMovement chaseMovement;
    [SerializeField] private WaypointMovement waypointMovement;

    public void Start()
    {
        SetState(State.Patrol);
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
                waypointMovement.isMovementSystemActive = false;
                break;
            case State.Chase:
                chaseMovement.isMovementSystemActive = false;
                break;
        }
    }

    private void EnterState(State state)
    {
        Debug.Log($"Entering {state}");

        switch (state)
        {
            case State.Patrol:
                waypointMovement.isMovementSystemActive = true;
                break;
            case State.Chase:
                chaseMovement.isMovementSystemActive = true;
                break;
        }
    }
}
