using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelTrailHandler : MonoBehaviour
{
    private VehicleController vehicleController;
    private TrailRenderer trailRenderer;
    private void Awake()
    {
        vehicleController = GetComponentInParent<VehicleController>();
        trailRenderer = GetComponent<TrailRenderer>();
        trailRenderer.emitting = false;
    }

    private void Update()
    {
        trailRenderer.emitting = vehicleController.IsTireScreeching();
    }
}
