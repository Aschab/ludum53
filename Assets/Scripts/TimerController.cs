using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using ScriptableObjectArchitecture;
using DG.Tweening;

public class TimerController : MonoBehaviour
{
    [SerializeField] private float duration = 10.0f;
    [SerializeField] private float raiseDifficultyDuration = 5.0f;
    [SerializeField] private GameEvent onEndEvent;
    [SerializeField] private GameEvent timerEndEvent;
    [SerializeField] private FloatGameEvent onAddTimeEvent;
    [SerializeField] private FloatGameEvent onStopTimeEvent;

    public float totalTime;

    private int stopRemaining = 0;

    private TMP_Text text;
    private float remaining;
    private float raminingToDifficulty;

    private GameController gameController;

    private GameObject plusTime;

    private void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();

        plusTime = Resources.Load<GameObject>("Prefabs/UI/PlusTime");

        totalTime = 0f;

        remaining = duration;
        raminingToDifficulty = raiseDifficultyDuration;
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
        Transform textTransform = null;
        foreach (Transform child in transform)
        {
            if (child != transform){
                textTransform = child;
            }
        }

        textTransform.DOScale(2f, .2f).SetEase(Ease.InOutQuint).OnComplete(() => {
            textTransform.DOScale(1f, .2f).SetEase(Ease.OutBounce);
            text.text = remaining.ToString("F0");
        });
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
                totalTime += 1.0f;
                remaining -= 1.0f; // Decrease the remaining time
            }
            raminingToDifficulty -= 1.0f;
            if (raminingToDifficulty <= 0)
            {
                gameController.IncreaseDifficulty();
                raminingToDifficulty = raiseDifficultyDuration;
            }
            yield return new WaitForSeconds(1.0f); // Wait for one second
        }

        timerEndEvent.Raise();
    }

    public void AddTime(float amount)
    {
        remaining += amount;
        GameObject obj = Instantiate(plusTime, Vector3.zero, Quaternion.identity);
        PlusTimeController indicatorController = obj.GetComponent<PlusTimeController>();
        indicatorController.text = $"+{amount}";

        obj.transform.SetParent(null);
        UpdateText();
    }

    public void RemoveTime(float amount)
    {
        remaining -= amount;
        UpdateText();
    }

    public void StopTime(float amount)
    {
        stopRemaining += Mathf.RoundToInt(amount);
    }
}
