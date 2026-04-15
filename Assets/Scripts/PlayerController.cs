using UnityEngine;

[RequireComponent(typeof(MovementComponent))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private AnimationComponent animationComponent;

    private MovementComponent movementComponent;

    void Start()
    {
        movementComponent = GetComponent<MovementComponent>();
    }

    void Update()
    {
        float movX = Input.GetAxis("Horizontal");
        float movY = Input.GetAxis("Vertical");
        Vector2 inputVec = new Vector2(movX, movY);

        if (Input.anyKeyDown)
        {
            GlobalEvents.BroadcastPlayerInput();
        }

        movementComponent.StepMove(inputVec.normalized);
        animationComponent.UpdateAnimation(inputVec.normalized);
    }
}
