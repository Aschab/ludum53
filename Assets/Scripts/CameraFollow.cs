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
        Vector3 smoothFollow = applySmooth ? Vector3.Lerp(transform.position, targetPos, smoothSpeed * Time.deltaTime) : targetPos;
        transform.position = smoothFollow;
    }
}
