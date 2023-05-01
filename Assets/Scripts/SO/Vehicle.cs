using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Vehicle", menuName = "Vehicle")]
public class Vehicle : ScriptableObject
{
    public float accelerationForce = 45f;
    public float steeringForce = 0.75f;
    public float traction = 75f;
    public float breakForce = 1.5f;
    public float maxSpeed = 60f;
    public float backwardMaxSpeedMultiplier = 0.5f;
    public float backwardAccelerationForceMultiplier = 0.5f;
    public float dampResistance = 0f;

    [TextArea]
    public string description;
}
