using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class VehicleSelectionOption : MonoBehaviour
{
    [SerializeField] public VehicleController vehicle;
    [SerializeField] private GameData gameData;
    [SerializeField] private TMP_Text description;

    [HideInInspector] public UnityEvent selectEvent;    

    private GameObject border;
    private Vector3 originalScale;
    private Vector3 targetScale;

    private void Start()
    {
        selectEvent = new UnityEvent();

        border = transform.Find("Selection Border").gameObject;
        originalScale = border.transform.localScale;
        targetScale = originalScale * 1.05f;

        if (gameData.selectedVehicle.name == vehicle.name) ApplySelect();
    }

    private void ApplySelect()
    {
        transform.DOScale(1.25f, 0.2f).SetEase(Ease.InOutQuad);
        border.transform.DOScale(targetScale * 1.05f, 0.2f).SetEase(Ease.InOutQuad);
    }
    
    public void Select()
    {
        selectEvent.Invoke();
        gameData.selectedVehicle = vehicle;
        //description.text = vehicle.description;
        ApplySelect();
    }

    public void UnSelect()
    {
        transform.DOScale(1f, 0.2f).SetEase(Ease.InOutQuad);
        border.transform.DOScale(originalScale, 0.2f).SetEase(Ease.InOutQuad);
    }
}
