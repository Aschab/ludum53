using UnityEngine;
using UnityEngine.UI;

public class DialogToggle : MonoBehaviour
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
        data.dialog = !data.dialog;
        UpdateSprite();
    }

    private void UpdateSprite()
    {
        image.sprite = data.dialog
            ? onSprite
            : offSprite;
    }
}
