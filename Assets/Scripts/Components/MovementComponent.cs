using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementComponent : MonoBehaviour
{

    public float force = 25000f;
    public float timerThreshold = 0.5f;

    public bool isMoving { get; private set; } = false;
    private Rigidbody2D entityRigidbody;

    private float timer;

    private bool oneShot = true;
    private float firstTimerThreshold;
    private float holdTimerThreshold;
    private bool active = true;


    void Start()
    {
        firstTimerThreshold = timerThreshold;
        holdTimerThreshold = timerThreshold / 2f;

        entityRigidbody = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 normalizedInputVec)
    {
        if (!active) {return;}

        entityRigidbody.AddForce(normalizedInputVec * force * Time.deltaTime);
        //entityRigidbody.velocity = normalizedInputVec * speed;

        if (normalizedInputVec.x > 0 || normalizedInputVec.y > 0 || normalizedInputVec.x < 0 || normalizedInputVec.y < 0)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
    }

    public void StepMove(Vector2 normalizedInputVec)
    {
        if (!active) {return;}

        if (oneShot || timer >= timerThreshold)
        {
            timerThreshold = oneShot ? firstTimerThreshold : holdTimerThreshold;
            entityRigidbody.AddForce(normalizedInputVec * force);
            oneShot = false;
            timer = 0;
        }


        if (normalizedInputVec.x > 0 || normalizedInputVec.y > 0 || normalizedInputVec.x < 0 || normalizedInputVec.y < 0)
        {
            isMoving = true;
            timer += Time.deltaTime;
        }
        else
        {
            isMoving = false;
            oneShot = true;
            timer = 0;
        }
    }

    public Vector2 GetDirectionTo(GameObject target)
    {
        Vector2 direction = target.transform.position - this.transform.position;
        return direction.normalized;
    }

    public void Disable()
    {
        isMoving = false;
    }

    public void AddRandomForce(float intensity)
    {
        entityRigidbody.AddForce(new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * intensity);
    }
}
