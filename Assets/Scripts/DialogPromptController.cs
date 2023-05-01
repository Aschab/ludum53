using UnityEngine;
using TMPro;
using DG.Tweening;
using System.Collections;
using System;

public class DialogPromptController : MonoBehaviour
{
    [SerializeField] private GameObject character;
    [SerializeField] private GameObject dialogBox;
    [SerializeField] private TMP_Text dialogText;

    [HideInInspector]
    public float duration = 5f;
    public string text = "";

    public bool isActive = true;

    private void Start()
    {
        RectTransform characterRect = character.GetComponent<RectTransform>();
        RectTransform dialogBoxRect = dialogBox.GetComponent<RectTransform>();
        Vector3 characterTargetPosition = characterRect.anchoredPosition;
        Vector3 dialogBoxTargetSize = dialogBoxRect.sizeDelta;

        dialogText.SetText(text);
        dialogText.color = new Color(dialogText.color.r, dialogText.color.g, dialogText.color.b, 0);

        characterRect.anchoredPosition = characterTargetPosition + new Vector3(characterRect.rect.width, 0, 0);
        dialogBoxRect.sizeDelta = new Vector3(0f, dialogBoxTargetSize.y, dialogBoxTargetSize.z);

        DOTween.Sequence()
            .Append(characterRect.DOAnchorPos(characterTargetPosition, 0.2f))
            .Append(dialogBoxRect.DOSizeDelta(dialogBoxTargetSize, 0.35f))
            .Append(dialogText.DOFade(1, 0.15f));

        StartCoroutine(Countdown());
    }

    private IEnumerator Countdown()
    {
        yield return new WaitForSeconds(duration);
        Kill(false, null);
    }

    public void Kill(bool quick, Action callback)
    {
        RectTransform characterRect = character.GetComponent<RectTransform>();
        RectTransform dialogBoxRect = dialogBox.GetComponent<RectTransform>();
        Vector3 characterStartPosition = characterRect.anchoredPosition;
        Vector3 dialogBoxStartSize = dialogBoxRect.sizeDelta;

        Vector3 characterTargetPosition = characterStartPosition + new Vector3(characterRect.rect.width, 0, 0);
        Vector3 dialogBoxTargetSize = new Vector3(0f, dialogBoxStartSize.y, dialogBoxStartSize.z);

        float quickMultiplier = quick ? 0.5f : 1.0f;
        DOTween.Sequence()
            .Append(dialogText.DOFade(0, 0.15f * quickMultiplier))
            .Append(dialogBoxRect.DOSizeDelta(dialogBoxTargetSize, 0.35f * quickMultiplier))
            .Append(characterRect.DOAnchorPos(characterTargetPosition, 0.2f * quickMultiplier))
            .AppendCallback(() => {
                Destroy(gameObject);
                isActive = false;
                callback?.Invoke();
            });
    }

    public static DialogPromptController Spawn(float duration, string text)
    {
        GameObject instance = Instantiate(Resources.Load<GameObject>("Prefabs/UI/DialogPrompt"), Vector3.zero, Quaternion.identity);
        DialogPromptController controller = instance.GetComponent<DialogPromptController>();

        controller.duration = duration;
        controller.text = text;

        instance.transform.SetParent(null);
        return controller;
    }

}
