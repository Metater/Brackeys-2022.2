using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private float speed;

    [SerializeField] private GameObject rockyOrbitDot;
    [SerializeField] private Transform rockyOrbitDotsParent;
    [SerializeField] private int rockyOrbitDotsCount;

    [SerializeField] private GameObject rocky;
    [SerializeField] private float rockySpeed;
    [SerializeField] private float rockyRadius;
    [SerializeField] private float rockySmoothSpeed;

    [SerializeField] private float cameraFollowSmoothTime;
    [SerializeField] private float cameraFollowMaxSpeed;

    private Vector3 rockyVelocity;
    private Vector3 cameraFollowVelocity = Vector3.zero;

    private void Start()
    {
        for (int i = 0; i < rockyOrbitDotsCount; i++)
        {
            float step = 2 * Mathf.PI * ((float)i / rockyOrbitDotsCount - 1);
            Vector2 rockyOrbitDotSpawnPos = new Vector3(Mathf.Cos(step), Mathf.Sin(step)) * rockyRadius;
            Instantiate(rockyOrbitDot, rockyOrbitDotSpawnPos, Quaternion.identity, rockyOrbitDotsParent);
        }
    }

    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        transform.position += speed * Time.deltaTime * new Vector3(x, y);

        float rockyTime = Time.time * rockySpeed;
        Vector3 rockyPos = transform.position + new Vector3(Mathf.Cos(rockyTime), Mathf.Sin(rockyTime)) * rockyRadius;
        rocky.transform.position = Vector3.SmoothDamp(rocky.transform.position, rockyPos, ref rockyVelocity, rockySmoothSpeed);

        Transform camera = Camera.main.transform;
        Vector3 output = Vector3.SmoothDamp(camera.position, transform.position, ref cameraFollowVelocity, cameraFollowSmoothTime, cameraFollowMaxSpeed);
        camera.position = new Vector3(output.x, output.y, camera.position.z);
    }
}
