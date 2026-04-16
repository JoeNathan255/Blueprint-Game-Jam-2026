using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(MovementComponent), typeof(WaypointMovement))]
public class CutscenePatient : MonoBehaviour, IKillable
{
    public UnityEvent OnCutscenePatientKilled;

    [SerializeField] private AnimationComponent animationComponent;
    [SerializeField] private float destroyAfterKillTime = 3.0f;
    private WaypointMovement waypointMovement;
    private MovementComponent movementComponent;
    private Vector2 inputVector = new Vector2();
    private bool isCutsceneStarted = false;
    private bool isAlive = true;

    void Start()
    {
        waypointMovement = GetComponent<WaypointMovement>();
        movementComponent = GetComponent<MovementComponent>();
    }

    void Update()
    {
        if (!isCutsceneStarted || !isAlive)
        {
            return;
        }

        inputVector = movementComponent.GetDirectionTo(waypointMovement.GetTargetWaypoint());
        movementComponent.StepMove(inputVector);
        animationComponent.UpdateAnimation(inputVector);
    }

    public void StartCutscene()
    {
        isCutsceneStarted = true;
    }

    public void Kill()
    {
        Debug.Log("Patient Killed");
        animationComponent.UpdateAnimation(Vector2.zero);
        isAlive = false;
        OnCutscenePatientKilled?.Invoke();
        Destroy(gameObject, destroyAfterKillTime);
    }
}
