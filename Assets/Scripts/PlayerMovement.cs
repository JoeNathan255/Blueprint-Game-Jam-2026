using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GameObject sprite;
    [SerializeField] private float speed = 1f;
    [SerializeField] private bool isMovementSystemActive = true;

    private Rigidbody2D entityRigidbody;
    private Animator entityAnimator;

    void Start()
    {
        entityRigidbody = GetComponent<Rigidbody2D>();
        entityAnimator = sprite.GetComponent<Animator>();
    }

    void Update()
    {
        if (isMovementSystemActive)
        {
            float movX = Input.GetAxis("Horizontal");
            float movY = Input.GetAxis("Vertical");
            Vector2 inputVec = new Vector2(movX, movY);
            Move(inputVec.normalized);
        }
    }

    private void Move(Vector2 normalizedInputVec)
    {
        entityRigidbody.velocity = normalizedInputVec * speed;

        if (normalizedInputVec.x > 0 || normalizedInputVec.y > 0 || normalizedInputVec.x < 0 || normalizedInputVec.y < 0)
        {
            entityAnimator.SetBool("IsWalking", true);
            entityAnimator.SetFloat("InputX", normalizedInputVec.x);
            entityAnimator.SetFloat("InputY", normalizedInputVec.y);
        }
        else
        {
            entityAnimator.SetBool("IsWalking", false);
        }
    }
}
