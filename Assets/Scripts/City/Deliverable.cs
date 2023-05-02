using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class Deliverable : MonoBehaviour
{
    public GameController gameController;
    private bool grabbed;
    
    private Sequence deliverableMovement;
    private GameData data;

    void Start()
    {
        grabbed = false;
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        data = Resources.Load<GameData>("GameData");

        Sequence deliverableMovement = DOTween.Sequence();

        deliverableMovement = DOTween.Sequence();
        deliverableMovement.Append(transform.DOScale(1.3f, .3f).SetRelative(true).SetEase(Ease.OutBounce))
        .Append(transform.DORotate(Vector2.zero, 0).SetDelay(2f).SetDelay(2f))
        .Append(transform.DOScale((1f/1.3f), .3f).SetRelative(true).SetEase(Ease.InOutQuint));
        deliverableMovement.SetLoops(-1, LoopType.Restart);
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!grabbed) 
        {
            grabbed = true;
            if (collision.gameObject.tag == "Player")
            {
                if (!data.muted) 
                {
                    AudioSource audio = GetComponent<AudioSource>();
                    audio.Play();
                }
                deliverableMovement.Kill();
                gameController.Grab(DeliverableType.Pizza);
                transform.DOScale(0f, .4f).SetEase(Ease.InOutQuint).OnComplete(() => {                    
                    transform.DOScale(0f, .5f).SetEase(Ease.InOutQuint).OnComplete(() => {
                        Destroy(gameObject);
                    });
                });
            }
        }
    }
}


    
[Serializable]
public enum DeliverableType
{
    None,
    Pizza,
}

