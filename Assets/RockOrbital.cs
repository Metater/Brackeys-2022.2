using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockOrbital : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private float velocity;
    [SerializeField] private int capacity;
    [SerializeField] private int orbitalDotsCount;
    [SerializeField] private GameObject orbitalDotPrefab;

    public EphemeralMultipliers RadiusMultipliers { get; private set; }
    public EphemeralMultipliers VelocityMultipliers { get; private set; }

    private List<GameObject> orbitalDots;
    private float cachedRadius;
    private float cachedOrbitalDotsCount;

    private void Awake()
    {
        VelocityMultipliers = new();
        RadiusMultipliers = new();

        orbitalDots = new();
        cachedRadius = radius;
        cachedOrbitalDotsCount = orbitalDotsCount;
    }

    private void Update()
    {
        float processedRadius = RadiusMultipliers.GetProduct(radius);
        float processedVelocity = VelocityMultipliers.GetProduct(velocity);

        if (cachedRadius != radius || cachedOrbitalDotsCount != orbitalDotsCount)
        {
            cachedRadius = radius;
            cachedOrbitalDotsCount = orbitalDotsCount;
            RefreshOrbitalDots();
        }
    }

    private void RefreshOrbitalDots()
    {
        orbitalDots.ForEach(go => Destroy(go));

        for (int i = 0; i < orbitalDotsCount; i++)
        {
            float step = 2 * Mathf.PI * ((float)i / orbitalDotsCount - 1);
            Vector2 orbitalDotPos = new Vector3(Mathf.Cos(step), Mathf.Sin(step)) * radius;
            orbitalDots.Add(Instantiate(orbitalDotPrefab, orbitalDotPos, Quaternion.identity, transform));
        }
    }
}
