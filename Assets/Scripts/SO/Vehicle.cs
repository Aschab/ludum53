using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Vehicle", menuName = "Vehicle")]
public class Vehicle : ScriptableObject
{
    public float accelerationForce = 45f;
    public float steerForce = 0.75f;
    public float maxSpeed = 60f;
    public Sprite sprite;
    public float scaleFactor = 1;

    public Vector2 GetScaleVector()
    {
        return new Vector2(scaleFactor, scaleFactor);
    }
}
