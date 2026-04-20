using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Collider2D))]
public class PushDoor : MonoBehaviour
{
    public Sprite closedSprite;
    public Sprite openSprite;
    public AudioSource audioSource;
    public AudioClip doorOpenClip;
    public AudioClip doorCloseClip;
    private SpriteRenderer spriteRenderer;
    private bool isOpen = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // void OnTriggerEnter();
    // {
    //     if (collision.otherRigidbody.GetComponent<PlayerController>() != null)
    //     {
    //         SetOpen();
    //     }
    // }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            SetOpen();
        }
    }

    public void SetOpen()
    {
        if (isOpen) { return; }

        isOpen = true;
        spriteRenderer.sprite = openSprite;

        audioSource.PlayOneShot(doorOpenClip);
    }
}
