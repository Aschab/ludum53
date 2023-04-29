using UnityEngine.InputSystem;
using UnityEngine;
using ScriptableObjectArchitecture;
using System.Collections;

public class VehicleController : MonoBehaviour
{
    PlayerControls controls;
    [SerializeField] private Vehicle vehicle;

    [SerializeField] private FloatGameEvent speedUpEvent;
    [SerializeField] private BoolGameEvent onGrassEvent;

    private Rigidbody2D rb;
    private InputAction accelerateAction, steeringAction;
    private float speedUpMultiplier = 1f;
    private float dampValue = 0f;
    private bool applyGrassSlow = false;
    [SerializeField] private float grassFriction = 1f;

    private void Awake()
    {
        controls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        GetComponent<SpriteRenderer>().sprite = vehicle.sprite;
        transform.localScale = vehicle.GetScaleVector();
    }

    private void OnEnable()
    {
        accelerateAction = controls.Driving.Accelerate;
        controls.Driving.Accelerate.Enable();

        steeringAction = controls.Driving.Steering;
        controls.Driving.Steering.Enable();

        speedUpEvent.AddListener(SpeedUpEventHandler);
        onGrassEvent.AddListener(OnGrassEventHandler);
    }

    private float accelerationInput = 0;
    private float steeringInput = 0;
    private void Update()
    {
        accelerationInput = accelerateAction.ReadValue<float>();
        steeringInput = steeringAction.ReadValue<float>();
    }

    // private void FixedUpdate()
    // {
    //     if (accelerationInput == 0)
    //     {
    //         // Gradually stop the vehicle when acceleration is not pressed
    //         rb.velocity *= (1 - 10f * Time.fixedDeltaTime);
    //     }
    //     else
    //     {
    //         // Accelerate the vehicle
    //         rb.AddForce(transform.up * vehicle.accelerationForce * accelerationInput, ForceMode2D.Force);
    //     }

    //     // Limit the vehicle's speed
    //     rb.velocity = Vector2.ClampMagnitude(rb.velocity, vehicle.maxSpeed);

    //     // Prevent player from turning when the car is at a standstill or very slow speed
    //     if (rb.velocity.magnitude > 0.5f)
    //     {
    //         // Steering
    //         float steering = vehicle.steeringForce * steeringInput;
    //         rb.MoveRotation(rb.rotation + steering * Time.fixedDeltaTime);
    //     }
    // }

    private void FixedUpdate()
    {

        Vector2 fowardForce = transform.up * accelerationInput * (vehicle.accelerationForce) * speedUpMultiplier;
        if (
            accelerationInput < 0 && Vector2.Dot(rb.velocity, transform.up) > 0 ||
            accelerationInput > 0 && Vector2.Dot(rb.velocity, transform.up) < 0
        ) fowardForce *= vehicle.breakForce; // apply break force only when breaking
        rb.AddForce(fowardForce, ForceMode2D.Force);

        rb.drag = accelerationInput == 0
            ? Mathf.Lerp(rb.drag, 3.0f, Time.fixedDeltaTime * 3)
            : applyGrassSlow
                ? grassFriction
                : 0;

        float minTurningSpeed = Mathf.Clamp01(rb.velocity.magnitude / 2);
        rb.rotation += steeringInput * vehicle.steeringForce * minTurningSpeed;

        Vector2 forward = new Vector2(0.0f, 1f);

        float steeringRightAngle = rb.angularVelocity > 0
            ? -90
            : 90;
        
        Vector2 rightAngleFromForward = Quaternion.AngleAxis(steeringRightAngle, Vector3.forward) * forward;

        float driftForce = Vector2.Dot(rb.velocity, rb.GetRelativeVector(rightAngleFromForward.normalized));

        Vector2 relativeForce = (rightAngleFromForward.normalized * -1.0f) * (driftForce * vehicle.traction);

        rb.AddForce(rb.GetRelativeVector(relativeForce));
        if (dampValue > 1f) 
        {
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, dampValue);
        }
        else 
        {
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, vehicle.maxSpeed);
        }
    }
    public void OnDisable()
    {
        controls.Driving.Accelerate.Disable();
        controls.Driving.Steering.Disable();
        speedUpEvent.RemoveListener(SpeedUpEventHandler);
        onGrassEvent.RemoveListener(OnGrassEventHandler);
    }
    private void SpeedUpEventHandler(float multiplier)
    {
        StartCoroutine(SpeedUp(3, multiplier));
    }
    public IEnumerator SpeedUp(float duration, float multiplier)
    {
        speedUpMultiplier = multiplier;
        yield return new WaitForSeconds(duration);
        speedUpMultiplier = 1;
    }

    public void Damp(float d)
    {
        dampValue = d;
    }

    private void OnGrassEventHandler(bool onGrass)
    {
        applyGrassSlow = onGrass;
    }
}
