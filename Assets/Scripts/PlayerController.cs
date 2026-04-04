using UnityEngine;

[RequireComponent(typeof(MovementComponent))]
public class PlayerMovement : MonoBehaviour
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

        movementComponent.Move(inputVec.normalized);
        animationComponent.UpdateAnimation(inputVec.normalized);
    }
}
