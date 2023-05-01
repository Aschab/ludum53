using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class EndOverlayController : MonoBehaviour
{
    [SerializeField] private TMP_Text mainText, highScoreText;
    [SerializeField] private CanvasGroup canvas;

    [HideInInspector] public int score = 0;

    private void Start()
    {
        bool isHighScore = IsHighScore(score);
        if (isHighScore) SetHighScore(score);
        int highScore = GetHighScore();
        
        mainText.text = $"You delivered <b>{score}</b> packages";
        highScoreText.text = isHighScore
            ? $"new high score! <i>{score}</i> package{IsPlural(score)}"
            : $"high score is {highScore} package{IsPlural(highScore)}";

        canvas.alpha = 0f;
        canvas.DOFade(1f, 0.2f);
    }

    private static string IsPlural(int value)
    {
        return value > 1 ? "s" : "";
    }

    private static string HIGH_SCORE_KEY = "High Score";
    public static int GetHighScore()
    {
        if (!PlayerPrefs.HasKey(HIGH_SCORE_KEY)) {
            PlayerPrefs.SetInt(HIGH_SCORE_KEY, 0);
        }

        return PlayerPrefs.GetInt(HIGH_SCORE_KEY);
    }

    public static bool IsHighScore(int score)
    {
        return score > GetHighScore();
    }

    public static void SetHighScore(int score)
    {
        PlayerPrefs.SetInt(HIGH_SCORE_KEY, score);
    }

    public void TryAgain()
    {
        SceneManager.LoadScene("Level1");
    }

    public void GoToStartMenu()
    {
        SceneManager.LoadScene("Start Menu");
    }

    public void Quit()
    {
        Application.Quit();
    }
    
}
