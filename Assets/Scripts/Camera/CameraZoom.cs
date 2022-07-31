using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] [Range(0, 60)] private float defaultDistance = 60;
    [SerializeField] [Range(0, 60)] private float minDistance = 20;
    [SerializeField] [Range(0, 60)] private float maxDistance = 60;

    [SerializeField] [Range(0, 20)] private float smoothing = 4;
    [SerializeField] [Range(0, 20)] private float zoomSensitivity = 1;

    [SerializeField] private Camera cam;

    private float currentTargetDistance;

    private void Awake()
    {
        currentTargetDistance = defaultDistance;
    }
    private void Update()
    {
        Zoom();
    }

    private void Zoom()
    {
        float zoomValue = -Input.GetAxisRaw("Mouse ScrollWheel") * zoomSensitivity;

        currentTargetDistance = Mathf.Clamp(currentTargetDistance + zoomValue, minDistance, maxDistance);

        float currentDistance = cam.fieldOfView;
        if (currentDistance == currentTargetDistance)
            return;
        float lerpedZoomValue = Mathf.Lerp(currentDistance, currentTargetDistance, smoothing * Time.deltaTime);
        cam.fieldOfView = lerpedZoomValue;
    }
}
