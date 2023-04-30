using DG.Tweening;
using UnityEngine;

public class HoverEffect : MonoBehaviour
{
    [SerializeField] private float hoverDistance = 5f;
    [SerializeField] private float hoverDuration = 2f;
    [SerializeField] private bool randDelay = false;
    void Start()
    {
        
        RectTransform rectTransform = GetComponent<RectTransform>();

        DOVirtual.DelayedCall(randDelay ? Random.Range(0f, 1f) : 0f, () =>
        {
            rectTransform.DOAnchorPosY(rectTransform.anchoredPosition.y + hoverDistance, hoverDuration)
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo);
        });
    }
}
