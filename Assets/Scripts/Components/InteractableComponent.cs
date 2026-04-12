using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableComponent : MonoBehaviour
{
    public UnityEvent OnInteract;
    public bool isInRange { get; private set; } = false;
    [SerializeField] bool isToggle = true;

    private SpriteRenderer spriteRenderer;
    private bool toggleState = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Interact");
            OnInteract?.Invoke();
            if (isToggle)
            {
                Toggle();
            }
        }
    }

    public void SetIsInRange(bool isInRange)
    {
        this.isInRange = isInRange;
    }

    private void Toggle()
    {
        if (toggleState)
        {
            spriteRenderer.color = Color.red;
            toggleState = false;
        }
        else
        {
            spriteRenderer.color = Color.green;
            toggleState = true;
        }
    }
}
