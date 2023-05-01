using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class PlusTimeController : MonoBehaviour
{
    [SerializeField] private TMP_Text textObj;
    [SerializeField] private CanvasGroup canvas;
    [HideInInspector] public string text = "";

    void Start()
    {
        textObj.text = text;
        RectTransform rect = textObj.GetComponent<RectTransform>();
        Vector3 originalScale = rect.localScale;
        rect.localScale = Vector3.zero;

        rect.anchoredPosition = new Vector3(100, 266, 0);

        DOTween.Sequence()
            .Append(rect.DOScale(originalScale, 0.075f))
            .AppendInterval(1f)
            .Append(canvas.DOFade(0f, 0.5f))
            .AppendCallback(() => {
                Destroy(gameObject);
            });
    }
    
}
