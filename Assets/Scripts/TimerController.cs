using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using ScriptableObjectArchitecture;

public class TimerController : MonoBehaviour
{
    [SerializeField] private float duration = 10.0f;
    [SerializeField] private GameEvent OnEndEvent;

    private TMP_Text text;
    private float remaining;

    private void Start()
    {
        remaining = duration;
        text = GetComponentInChildren<TMP_Text>();
        StartCoroutine(Countdown());
    }

    private IEnumerator Countdown()
    {
        while (remaining > 0)
        {
            text.text = remaining.ToString("F0");
            yield return new WaitForSeconds(1.0f); // Wait for one second
            remaining -= 1.0f; // Decrease the remaining time
        }

        OnEndEvent.Raise();
    }
}
