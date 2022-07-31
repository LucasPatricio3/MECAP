using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFolliow : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 cameraPositionDisplacement;
    private Vector3 cameraOffset;

    [SerializeField] [Range(0.01f, 1f)] private float smoothness = 0.5f;
    void Start()
    {
        cameraOffset = transform.position - player.transform.position;
    }

    void Update()
    {
        Vector3 newPos = player.position + cameraOffset + cameraPositionDisplacement;
        transform.position = Vector3.Slerp(transform.position, newPos, smoothness);
    }
}
