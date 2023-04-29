using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DeliverArea : MonoBehaviour
{
    public GameController gameController;

    public DeliverableType type;
    public float stoppedAt = 3f;

    public float minScaleX;
    public float minScaleY;
    public float maxScaleX;
    public float maxScaleY;
    
    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        transform.localScale = new Vector3(UnityEngine.Random.Range(minScaleX, maxScaleX), UnityEngine.Random.Range(minScaleY, maxScaleY), 1f);
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Rigidbody2D body = collision.gameObject.GetComponent<Rigidbody2D>();
            if (isStopped(body))
            {
                gameController.Deliver(type);
                Destroy(gameObject);
            }
        }
    }

    private bool isStopped(Rigidbody2D body)
    {
        Vector2 vel = body.velocity;
        float absoluteSpeed = (float) Math.Sqrt(vel.x*vel.x + vel.y*vel.y);
        return absoluteSpeed < stoppedAt;
    }
}