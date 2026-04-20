using System.Threading;
using UnityEngine;

[RequireComponent(typeof(MovementComponent))]
public class PlayerController : MonoBehaviour, IKillable
{
    public float deathTime = 0.5f;
    [SerializeField] private bool immortal = false;
    [SerializeField] private AnimationComponent animationComponent;

    private MovementComponent movementComponent;
    private bool isMobile = true;
    private bool isDying = false;
    private Vector2 inputVec;
    private float deathTimer = 0f;

    void Start()
    {
        movementComponent = GetComponent<MovementComponent>();
    }

    void Update()
    {
        if (isDying)
        {
            deathTimer += Time.deltaTime;
            if (deathTimer >= deathTime)
            {
                GlobalEvents.BroadcastGameOver();
            }
        }

        if (!isMobile)
        {
            return;
        }

        float movX = Input.GetAxis("Horizontal");
        float movY = Input.GetAxis("Vertical");
        inputVec = new Vector2(movX, movY);

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
        if (!immortal)
        {
            isDying = true;
            isMobile = false;
            animationComponent.entityAnimator.SetTrigger("TrDeath");
            
            Debug.Log("Player Killed");
            //GlobalEvents.BroadcastGameOver();
        }
    }
}
