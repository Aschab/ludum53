using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RandomMovement : MonoBehaviour
{

    public float maxMovement;

    void Start()
    {
        DoRandomMovement();
    }

    void DoRandomMovement()
    {
        Vector2 pos = new Vector2(Random.Range(-maxMovement, maxMovement), Random.Range(-maxMovement, maxMovement));
        transform.DOLocalMove(pos, Random.Range(.2f, 1f)).SetEase(Ease.InOutQuint).OnComplete(() => {
            DoRandomMovement();
        });
    }
}
