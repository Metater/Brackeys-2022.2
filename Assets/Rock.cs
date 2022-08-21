using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float rockSpeed;
    [SerializeField] private float rockRadius;
    [SerializeField] private float rockSmoothSpeed;

    [SerializeField] private GameObject rockOrbitDotPrefab;
    [SerializeField] private int rockOrbitDotsCount;

    private Vector3 rockVelocity;
    private List<GameObject> orbitDots;

    private void Awake()
    {
        orbitDots = new();
    }

    private void Start()
    {
        RefreshOrbitDots();
    }

    private void Update()
    {
        // Make rock speed units/sec, not arc/sec
        float rockTime = Time.time * rockSpeed;
        Vector3 rockyPos = player.position + new Vector3(Mathf.Cos(rockTime), Mathf.Sin(rockTime)) * rockRadius;
        transform.position = Vector3.SmoothDamp(transform.position, rockyPos, ref rockVelocity, rockSmoothSpeed);
    }

    public void RefreshOrbitDots()
    {
        orbitDots.ForEach(go => Destroy(go));

        for (int i = 0; i < rockOrbitDotsCount; i++)
        {
            float step = 2 * Mathf.PI * ((float)i / rockOrbitDotsCount - 1);
            Vector2 orbitDotPos = new Vector3(Mathf.Cos(step), Mathf.Sin(step)) * rockRadius;
            orbitDots.Add(Instantiate(rockOrbitDotPrefab, orbitDotPos, Quaternion.identity, player));
        }
    }
}
