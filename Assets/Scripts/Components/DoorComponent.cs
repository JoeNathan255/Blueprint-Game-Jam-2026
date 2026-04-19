using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Rigidbody2D), typeof(Collider2D))]
public class DoorComponent : MonoBehaviour
{
    public bool isOpen = false;
    public AudioSource audioSource;
    public AudioClip doorOpenClip;
    public AudioClip doorCloseClip;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D entityRigidbody;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        entityRigidbody = GetComponent<Rigidbody2D>();

        if (isOpen)
        {
            SetOpen(false);
        }
        else
        {
            SetClosed(false);
        }
    }

    public void ToggleOpen()
    {
        if (isOpen)
        {
            SetClosed(true);
        }
        else
        {
            SetOpen(true);
        }
    }

    public void SetOpen()
    {
        spriteRenderer.color = Color.clear;
        entityRigidbody.simulated = false;
        isOpen = true;

        audioSource.PlayOneShot(doorOpenClip);
    }

    public void SetClosed()
    {
        spriteRenderer.color = Color.white;
        entityRigidbody.simulated = true;
        isOpen = false;

        audioSource.PlayOneShot(doorOpenClip);
    }

    public void SetOpen(bool playSound)
    {
        spriteRenderer.color = Color.clear;
        entityRigidbody.simulated = false;
        isOpen = true;

        if (playSound)
        {
            audioSource.PlayOneShot(doorOpenClip);
        }
    }

    public void SetClosed(bool playSound)
    {
        spriteRenderer.color = Color.white;
        entityRigidbody.simulated = true;
        isOpen = false;

        if (playSound)
        {
            audioSource.PlayOneShot(doorOpenClip);
        }
    }
}
