using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class DeliverArea : MonoBehaviour
{
    public GameController gameController;

    public DeliverableType type;
    public float stoppedAt = 3f;

    private bool delivered;
    
    void Start()
    {
        delivered = false;
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (!delivered)
        {
            if (collision.gameObject.tag == "Player")
            {
                Rigidbody2D body = collision.gameObject.GetComponent<Rigidbody2D>();
                if (isStopped(body))
                {
                    delivered = true;

                    AudioSource audio = GetComponent<AudioSource>();
                    audio.Play();
                    ParticleSystem particles = GetComponentInChildren<ParticleSystem>();
                    particles.Stop();
                    transform.DOScale(0f, 2f).SetEase(Ease.InOutQuint).OnComplete(() => {
                        gameController.Deliver(type);
                        Destroy(gameObject);
                    });
                }
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
