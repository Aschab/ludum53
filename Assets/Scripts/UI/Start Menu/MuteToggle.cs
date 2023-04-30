using UnityEngine;
using UnityEngine.UI;

public class MuteToggle : MonoBehaviour
{
    [SerializeField] private Sprite onSprite;
    [SerializeField] private Sprite offSprite;
    private Image image;
    private GameData data;
    private void Start()
    {
        image = GetComponent<Image>();
        data = Resources.Load<GameData>("GameData");

        UpdateSprite();
    }

    public void Toggle()
    {
        data.muted = !data.muted;
        UpdateSprite();
    }

    private void UpdateSprite()
    {
        image.sprite = data.muted
            ? offSprite
            : onSprite;
    }
}
