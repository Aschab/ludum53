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
        Vector3 originalScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
        transform.localScale = Vector3.zero;

        DOTween.Sequence()
            .Append(transform.DOScale(originalScale, 0.075f))
            .AppendInterval(1f)
            .Append(canvas.DOFade(0f, 0.5f))
            .AppendCallback(() => {
                Destroy(gameObject);
            });
    }
    
}
