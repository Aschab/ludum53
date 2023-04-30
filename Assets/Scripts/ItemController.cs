using UnityEngine;
using DG.Tweening;

public class ItemController : MonoBehaviour
{
    [SerializeField] private Item item;

    private Sequence movement;

    private void Start()
    {

        float pulseScale = 1.1f;
        float pulseDuration = 0.5f;
        Ease pulseEase = Ease.InOutSine;
        int loopCount = -1;

        Vector3 originalScale = transform.localScale;

        movement = DOTween.Sequence()
            .Append(transform.DOScale(originalScale * pulseScale, pulseDuration).SetEase(pulseEase))
            .Append(transform.DOScale(originalScale, pulseDuration).SetEase(pulseEase))
            .SetLoops(loopCount, LoopType.Restart);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            item.Pickup();
            movement.Kill();
            AudioSource audio = GetComponent<AudioSource>();
            audio.Play();
            transform.DOScale(0f, .4f).SetEase(Ease.InOutQuint).OnComplete(() => {
                transform.DOScale(0f, .5f).SetEase(Ease.InOutQuint).OnComplete(() => {
                    Destroy(gameObject);
                });
            });
        }
    }
}
