using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using ScriptableObjectArchitecture;

public class TimerController : MonoBehaviour
{
    [SerializeField] private float duration = 10.0f;
    [SerializeField] private GameEvent onEndEvent;
    [SerializeField] private FloatGameEvent onAddTimeEvent;
    [SerializeField] private FloatGameEvent onStopTimeEvent;


    private int stopRemaining = 0;

    private TMP_Text text;
    private float remaining;

    private void Start()
    {
        remaining = duration;
        text = GetComponentInChildren<TMP_Text>();
        StartCoroutine(Countdown());
    }

    private void OnEnable()
    {
        onAddTimeEvent.AddListener(AddTime);
        onStopTimeEvent.AddListener(StopTime);
    }

    private void OnDisable()
    {
        onAddTimeEvent.RemoveListener(AddTime);
        onStopTimeEvent.RemoveListener(StopTime);
    }

    private void UpdateText()
    {
        text.text = remaining.ToString("F0");
    }

    private IEnumerator Countdown()
    {
        while (remaining > 0)
        {
            if (stopRemaining > 0)
            {
                stopRemaining -= 1;
            } else
            {
                UpdateText();
                remaining -= 1.0f; // Decrease the remaining time
            }
            yield return new WaitForSeconds(1.0f); // Wait for one second
        }

        onEndEvent.Raise();
    }

    public void AddTime(float amount)
    {
        remaining += amount;
        UpdateText();
    }

    public void StopTime(float amount)
    {
        stopRemaining += Mathf.RoundToInt(amount);
    }
}
