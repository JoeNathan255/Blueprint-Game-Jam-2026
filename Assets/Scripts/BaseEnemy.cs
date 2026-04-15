using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementComponent), typeof(ChaseMovement))]
public abstract class BaseEnemy : MonoBehaviour
{
    public enum State { Idle, Patrol, Chase, Disabled }
    protected State currentState;
    protected State defaultState;
    protected MovementComponent movementComponent;
    protected ChaseMovement chaseMovement;

    public void Disable()
    {
        Debug.Log($"{this} was disabled");
        SetState(State.Disabled);
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

    protected virtual void ExitState(State state)
    {

    }

    protected virtual void EnterState(State state)
    {

    }

    public virtual void AggroPlayer()
    {
        SetState(State.Chase);
    }

    public virtual void DeaggroPlayer()
    {
        SetState(defaultState);
    }
}
