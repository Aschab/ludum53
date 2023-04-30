using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float smoothSpeed = 15f;
    [SerializeField] private bool applySmooth = false;
    [SerializeField] private bool applySize = false;

    public float minSize;
    public float maxSize;

    private void Start()
    {
        offset = transform.position - target.transform.position;
    }

    private void LateUpdate()
    {
        SmoothFollow();
    }

    public void SmoothFollow()
    {
        Vector3 targetPos = target.transform.position + offset;
        float magnitude = target.GetComponent<Rigidbody2D>().velocity.magnitude;
        float targetSize = Mathf.Min(1f, target.GetComponent<Rigidbody2D>().velocity.magnitude / 100f);
        targetSize = (maxSize - minSize) * targetSize + minSize;
        Camera cam = GetComponent<Camera>();
        float smoothSize = applySize ? Mathf.Lerp(cam.orthographicSize, targetSize, smoothSpeed * Time.deltaTime) : minSize;
        cam.orthographicSize = smoothSize;
        Vector3 smoothFollow = applySmooth ? Vector3.Lerp(transform.position, targetPos, smoothSpeed * Time.deltaTime) : targetPos;
        transform.position = smoothFollow;
    }
}
