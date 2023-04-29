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

    private void FixedUpdate()
    {
        float acceleration = accelerateAction.ReadValue<float>();
        float steering = steeringAction.ReadValue<float>();

        Vector2 fowardForce = transform.up * acceleration * vehicle.accelerationForce * speedUpMultiplier;
        rb.AddForce(fowardForce, ForceMode2D.Force);

        rb.drag = acceleration == 0
            ? Mathf.Lerp(rb.drag, 3.0f, Time.fixedDeltaTime * 3)
            : applyGrassSlow
                ? grassFriction
                : 0;

        rb.rotation += steering * vehicle.steerForce;

        Vector2 forward = new Vector2(0.0f, 10f);

        float steeringRightAngle = rb.angularVelocity > 0
            ? -90
            : 90;
        
        Vector2 rightAngleFromForward = Quaternion.AngleAxis(steeringRightAngle, Vector3.forward) * forward;

        float driftForce = Vector2.Dot(rb.velocity, rb.GetRelativeVector(rightAngleFromForward.normalized));

        Vector2 relativeForce = (rightAngleFromForward.normalized * -1.0f) * (driftForce * 10.0f);

        rb.AddForce(rb.GetRelativeVector(relativeForce));
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, vehicle.maxSpeed);
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
    private void OnGrassEventHandler(bool onGrass)
    {
        applyGrassSlow = onGrass;
    }
}
