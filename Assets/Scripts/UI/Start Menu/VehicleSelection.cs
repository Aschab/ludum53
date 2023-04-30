using UnityEngine;

public class VehicleSelection : MonoBehaviour
{
    private VehicleSelectionOption[] options;
    private void Start()
    {
        options = GetComponentsInChildren<VehicleSelectionOption>();
        foreach (var option in options)
        {
            option.selectEvent.AddListener(OnSelect);
        }
    }
    private void OnDisable()
    {
        foreach (var option in options)
        {
            option.selectEvent.RemoveListener(OnSelect);
        }
    }

    private void OnSelect()
    {
        foreach (var option in options)
        {
            option.UnSelect();
        }
    }
}
