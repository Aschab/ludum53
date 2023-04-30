using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class Deliverable : MonoBehaviour
{
    public GameController gameController;
    
    private Sequence deliverableMovement;

    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();

        Sequence deliverableMovement = DOTween.Sequence();

        deliverableMovement = DOTween.Sequence();
        deliverableMovement.Append(transform.DOScale(1.3f, .3f).SetRelative(true).SetEase(Ease.OutBounce))
        .Append(transform.DORotate(Vector2.zero, 0).SetDelay(2f).SetDelay(2f))
        .Append(transform.DOScale((1f/1.3f), .3f).SetRelative(true).SetEase(Ease.InOutQuint));
        deliverableMovement.SetLoops(-1, LoopType.Restart);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            deliverableMovement.Kill();
            gameController.Grab(DeliverableType.Pizza);
            Destroy(gameObject);
        }
    }
}


    
[Serializable]
public enum DeliverableType
{
    None,
    Pizza,
}

