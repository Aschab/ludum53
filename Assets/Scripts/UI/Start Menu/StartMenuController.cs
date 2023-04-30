using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuController : MonoBehaviour
{
    [SerializeField] private float initialDelay = 0.5f;
    void Start()
    {
        // Set the initial scale to 0
        transform.localScale = Vector3.zero;

        // Wait for the initial delay
        DOVirtual.DelayedCall(initialDelay, () =>
        {
            // Create the tween to scale the object to 1 with a small bounce
            transform.DOScale(1, 0.5f).SetEase(Ease.OutBounce);
        });
    }

    public void Play()
    {
        SceneManager.LoadScene("Level1");
    }
}
