using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;


public class GhostingAround : MonoBehaviour
{
    public float penalty;

    private bool ghosted;
    public GameController gameController;

    private GameData data;

    void Start()
    {
        ghosted = false;
        data = Resources.Load<GameData>("GameData");
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!ghosted) 
        {
            ghosted = true;
            if (collision.gameObject.tag == "Player")
            {
                if (!data.muted) 
                {
                    AudioSource audio = GetComponent<AudioSource>();
                    if (audio)
                    {
                        audio.Play();
                    }
                }

                transform.DOScale(0f, .4f).SetEase(Ease.InOutQuint).OnComplete(() => {                    
                    gameController.Ghosted(penalty);
                    transform.DOScale(0f, .5f).SetEase(Ease.InOutQuint).OnComplete(() => {
                        Destroy(gameObject);
                    });
                });
            }
        }
    }
}
