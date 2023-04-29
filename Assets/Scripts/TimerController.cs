using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using ScriptableObjectArchitecture;

public class TimerController : MonoBehaviour
{
    [SerializeField] private float duration = 10.0f;
    [SerializeField] private float raiseDifficulty = 5.0f;
    [SerializeField] private GameEvent onEndEvent;
    [SerializeField] private FloatGameEvent onAddTimeEvent;

    private TMP_Text text;
    private float remaining;
    private float raminingToDifficulty;

    private void Start()
    {
        remaining = duration;
        raminingToDifficulty = raiseDifficulty;
        text = GetComponentInChildren<TMP_Text>();
        StartCoroutine(Countdown());
    }

    private void OnEnable()
    {
        onAddTimeEvent.AddListener(AddTime);
    }

    private void OnDisable()
    {
        onAddTimeEvent.RemoveListener(AddTime);
    }

    private void UpdateText()
    {
        text.text = remaining.ToString("F0");
    }

    private IEnumerator Countdown()
    {
        while (remaining > 0)
        {
            UpdateText();
            yield return new WaitForSeconds(1.0f); // Wait for one second
            remaining -= 1.0f; // Decrease the remaining time
            raminingToDifficulty -= 1.0f;
            if (raminingToDifficulty <= 0)
            {
                raminingToDifficulty = raiseDifficulty;
            }
        }

        onEndEvent.Raise();
    }

    public void AddTime(float amount)
    {
        remaining += amount;
        UpdateText();
    }

    public void StopTime()
    {
        
    }
}
