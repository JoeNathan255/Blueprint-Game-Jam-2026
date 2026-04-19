using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementComponent), typeof(ChaseMovement))]
public abstract class BaseEnemy : MonoBehaviour, IKillable
{
    public enum State { Idle, Patrol, Chase, Disabled }
    protected float tempoIncreaseStrength;
    protected float tempoIncreaseRadius;
    protected State currentState;
    protected State defaultState;
    protected MovementComponent movementComponent;
    protected ChaseMovement chaseMovement;
    protected bool alive = true;

    public void Kill()
    {
        Debug.Log($"{this} was disabled");
        SetState(State.Disabled);
        alive = false;
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

    public virtual void AttackTarget()
    {
        if (!alive) { return; }
        SetState(State.Chase);
    }

    public virtual void StopAttackingTarget()
    {
        if (!alive) { return; }
        SetState(defaultState);
    }

    protected virtual void TempoIncreaseCheck()
    {
        if (!alive) { return; }

        if (Vector2.Distance(transform.position, GlobalEvents.Instance.player.transform.position) < tempoIncreaseRadius)
        {
            GlobalEvents.Instance.SetNextTempoIncrease(tempoIncreaseStrength);
        }
    }

    public float getTempoIncreaseRadius()
    {
        return tempoIncreaseRadius;
    }
}
