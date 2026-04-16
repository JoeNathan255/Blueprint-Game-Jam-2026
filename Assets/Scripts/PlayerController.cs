using UnityEngine;

[RequireComponent(typeof(MovementComponent))]
public class PlayerController : MonoBehaviour, IKillable
{
    [SerializeField] private AnimationComponent animationComponent;

    private MovementComponent movementComponent;
    private bool isMobile = true;

    void Start()
    {
        movementComponent = GetComponent<MovementComponent>();
    }

    void Update()
    {
        if (!isMobile)
        {
            return;
        }

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

    public void SetImmobile()
    {
        animationComponent.UpdateAnimation(Vector2.zero);
        isMobile = false;
    }

    public void SetMobile()
    {
        isMobile = true;
    }

    public void Kill()
    {
        Debug.Log("Player Killed");
        GlobalEvents.BroadcastGameOver();
    }
}
