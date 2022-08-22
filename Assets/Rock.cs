using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float rockSpeed;
    [SerializeField] private float rockRadius;
    [SerializeField] private float rockSmoothSpeed;
    [Space]
    [SerializeField] private GameObject rockOrbitalDotPrefab;
    [SerializeField] private int rockOrbitalDotsCount;
    [Space]
    [SerializeField] private bool pointAwayFromPlayer;
    [SerializeField] private bool hasRandomTumbleSpeed;
    [SerializeField] private Vector2 tumbleSpeedRange;

    private Vector3 rockVelocity;
    private float tumbleSpeed;
    private List<GameObject> orbitalDots;

    private float radians = 0f;

    private void Awake()
    {
        orbitalDots = new();

        if (hasRandomTumbleSpeed)
        {
            tumbleSpeed = Random.Range(tubleSpeedRange.x, tubleSpeedRange.y);
        }
        else
        {
            tumbleSpeed = tubleSpeedRange.x;
        }
    }

    private void Start()
    {
        RefreshOrbitalDots();
    }

    private void Update()
    {
        if (pointAwayFromPlayer)
        {
            var angle = AngleBetweenTwoPoints(transform.position, player.position);
            transform.localEulerAngles = new Vector3(0f, 0f, angle - 90f);
        }
        else
        {
            transform.localEulerAngles = new Vector3(0f, 0f, transform.localEulerAngles.z + Time.deltaTime * tumbleSpeed);
        }

        radians += (Time.deltaTime * rockSpeed) / rockRadius;
        Vector3 rockyPos = player.position + new Vector3(Mathf.Cos(radians), Mathf.Sin(radians)) * rockRadius;
        transform.position = Vector3.SmoothDamp(transform.position, rockyPos, ref rockVelocity, rockSmoothSpeed);
    }

    private float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

    public void RefreshOrbitalDots()
    {
        orbitalDots.ForEach(go => Destroy(go));

        for (int i = 0; i < rockOrbitalDotsCount; i++)
        {
            float step = 2 * Mathf.PI * ((float)i / rockOrbitalDotsCount - 1);
            Vector2 orbitDotPos = new Vector3(Mathf.Cos(step), Mathf.Sin(step)) * rockRadius;
            orbitalDots.Add(Instantiate(rockOrbitalDotPrefab, orbitDotPos, Quaternion.identity, player));
        }
    }
}
