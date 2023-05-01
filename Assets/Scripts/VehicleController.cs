using UnityEngine.InputSystem;
using UnityEngine;
using ScriptableObjectArchitecture;
using System.Collections;

public class VehicleController : MonoBehaviour
{
    PlayerControls controls;
    [SerializeField] public Vehicle vehicle;

    [SerializeField] private FloatGameEvent speedUpEvent;
    [SerializeField] private BoolGameEvent onGrassEvent;

    private Rigidbody2D rb;
    private InputAction accelerateAction, steeringAction;
    private float speedUpMultiplier = 1f;
    private float dampValue = 0f;
    private bool applyGrassSlow = false;
    [SerializeField] private float grassFriction = 1f;
    [SerializeField] private float driftThreshHold = 4f;

    [SerializeField] private Vector2 pitchValues;
    [SerializeField] private Vector2 speedPitchValues;
    [SerializeField] private float carAudioVolume;
    [SerializeField] private float minSpeedAudio;
    [SerializeField] private AudioSource carAudio;
    [SerializeField] private float smoothTransition;

    [HideInInspector]
    public bool isBraking = false;

    private GameData data;
    private void Awake()
    {
        data = Resources.Load<GameData>("GameData");
        controls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        carAudio = GetComponent<AudioSource>();
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

        if (!data.muted) HandleAudio();
    }

    private void HandleAudio()
    {
        float targetVolume = carAudioVolume;
        if (rb.velocity.magnitude < minSpeedAudio) targetVolume = 0f;
        float targetPitch = pitchValues.x;
        float speedP = (rb.velocity.magnitude - speedPitchValues.x) / speedPitchValues.y;
        speedP = Mathf.Max(Mathf.Min(speedP, 1f), 0f);
        targetPitch += (pitchValues.y - pitchValues.x) * speedP;

        targetVolume = Mathf.Lerp(carAudio.volume, targetVolume, smoothTransition * Time.deltaTime);
        carAudio.volume = targetVolume;
        targetPitch = Mathf.Lerp(carAudio.pitch, targetPitch, smoothTransition * Time.deltaTime);
        carAudio.pitch = targetPitch;

    }

    private void FixedUpdate()
    {
        bool goingBackwards = accelerationInput < 0;
        isBraking = goingBackwards && Vector2.Dot(rb.velocity, transform.up) > 0;
        Vector2 fowardForce = transform.up * accelerationInput * speedUpMultiplier * vehicle.accelerationForce;
        fowardForce *= goingBackwards
            ? vehicle.backwardMaxSpeedMultiplier
            : 1;

        if (
            isBraking ||
            !goingBackwards && Vector2.Dot(rb.velocity, transform.up) < 0
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
            float finalDamp = rb.velocity.magnitude;
            if (rb.velocity.magnitude > dampValue)
            {
                finalDamp = dampValue + ((rb.velocity.magnitude - dampValue) * vehicle.dampResistance);
            }
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, dampValue);
        }
        else 
        {
            rb.velocity = Vector2.ClampMagnitude(
                rb.velocity,
                vehicle.maxSpeed * (goingBackwards ? vehicle.backwardMaxSpeedMultiplier : 1)
            );
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

    float GetLateralVelocity()
    {
        return Vector2.Dot(transform.right, rb.velocity);
    }

    public bool IsTireScreeching()
    {
        return isBraking || Mathf.Abs(GetLateralVelocity()) > driftThreshHold;
    }

    public string GetDescription()
    {
        return vehicle.description;
    }

}
