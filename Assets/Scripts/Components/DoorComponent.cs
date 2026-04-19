using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Rigidbody2D), typeof(Collider2D))]
public class DoorComponent : MonoBehaviour
{
    public bool isOpen = false;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D entityRigidbody;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        entityRigidbody = GetComponent<Rigidbody2D>();

        if (isOpen)
        {
            SetOpen();
        }
        else
        {
            SetClosed();
        }
    }

    public void ToggleOpen()
    {
        if (isOpen)
        {
            SetClosed();
        }
        else
        {
            SetOpen();
        }
    }

    public void SetOpen()
    {
        spriteRenderer.color = Color.clear;
        entityRigidbody.simulated = false;
        isOpen = true;
    }

    public void SetClosed()
    {
        spriteRenderer.color = Color.white;
        entityRigidbody.simulated = true;
        isOpen = false;
    }
}
