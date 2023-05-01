using UnityEngine;
using ScriptableObjectArchitecture;

public class EndController : MonoBehaviour
{
    [SerializeField] private GameController gameController;
    private GameEvent timerEndEvent;
    private GameObject endOverlay;

    private void Awake()
    {
        timerEndEvent = Resources.Load<GameEvent>("Events/TimerEnd");
        endOverlay = Resources.Load<GameObject>("Prefabs/UI/EndOverlay");
    }

    private void OnEnable()
    {
        timerEndEvent.AddListener(HandleTimerEnd);
    }

    private void HandleTimerEnd()
    {
        // instanciate prefab
        int score = gameController.delivered.Count;
        GameObject overlay = Instantiate<GameObject>(endOverlay, Vector3.zero, Quaternion.identity);
        overlay.GetComponent<EndOverlayController>().score = score;

        // show on screen
        overlay.transform.SetParent(null);
    }

    private void OnDisable()
    {
        timerEndEvent.RemoveListener(HandleTimerEnd);
    }
}
