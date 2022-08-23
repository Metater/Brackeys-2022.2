using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float cameraFollowSmoothTime;
    [SerializeField] private float cameraFollowMaxSpeed;

    private Vector3 cameraFollowVelocity = Vector3.zero;

    private void LateUpdate()
    {
        Transform camera = Camera.main.transform;
        Vector3 output = Vector3.SmoothDamp(camera.position, transform.position, ref cameraFollowVelocity, cameraFollowSmoothTime, cameraFollowMaxSpeed);
        camera.position = new Vector3(output.x, output.y, camera.position.z);
    }
}
