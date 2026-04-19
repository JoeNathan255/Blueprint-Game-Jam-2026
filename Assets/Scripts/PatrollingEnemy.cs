using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WaypointMovement))]
public class PatrollingEnemy : BaseEnemy
{
    [SerializeField] private AnimationComponent animationComponent;
    [SerializeField] private float patrolSpeed = 10000f;
    [SerializeField] private float chaseSpeed = 20000f;
    [SerializeField] private float chaseTempoIncreaseRadius = 6;
    [SerializeField] private float chaseTempoIncreaseStrength = 15;
    [SerializeField] private float patrolTempoIncreaseRadius = 3;
    [SerializeField] private float patrolTempoIncreaseStrength = 5;

    private WaypointMovement waypointMovement;
    private Vector2 inputVector = new Vector2();

    public void Start()
    {
        GlobalEvents.Instance.beatCount.OnBeat.AddListener(ActionOnBeat);
        defaultState = State.Patrol;
        movementComponent = GetComponent<MovementComponent>();
        chaseMovement = GetComponent<ChaseMovement>();
        waypointMovement = GetComponent<WaypointMovement>();
        SetState(State.Patrol);
    }

    public void ActionOnBeat()
    {
        if (!alive) { return; }

        //Debug.Log($"{this} action on beat");
        switch (currentState)
        {
            case State.Patrol:
                tempoIncreaseRadius = patrolTempoIncreaseRadius;
                tempoIncreaseStrength = patrolTempoIncreaseStrength;
                TempoIncreaseCheck();
                inputVector = movementComponent.GetDirectionTo(waypointMovement.GetTargetWaypoint());
                break;
            case State.Chase:
                tempoIncreaseRadius = chaseTempoIncreaseRadius;
                tempoIncreaseStrength = chaseTempoIncreaseStrength;
                TempoIncreaseCheck();
                inputVector = movementComponent.GetDirectionTo(chaseMovement.GetTarget());
                break;
            case State.Disabled:
                return;
        }

        //Debug.Log($"{this} moves {inputVector} on beat");
        movementComponent.BeatMove(inputVector);
        animationComponent.UpdateAnimation(inputVector);
    }

    protected override void EnterState(State state)
    {
        if (!alive || animationComponent == null) { return; }
        //Debug.Log($"Entering {state}");

        switch (state)
        {
            case State.Patrol:
                tempoIncreaseRadius = patrolTempoIncreaseRadius;
                tempoIncreaseStrength = patrolTempoIncreaseStrength;
                movementComponent.force = patrolSpeed;
                break;
            case State.Chase:
                tempoIncreaseRadius = chaseTempoIncreaseRadius;
                tempoIncreaseStrength = chaseTempoIncreaseStrength;
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
