using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockOrbital : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float radius;
    [SerializeField] private float velocity;
    [SerializeField] private float smoothSpeed;
    [SerializeField] private int capacity;
    [SerializeField] private int orbitalDotsCount;
    [SerializeField] private bool isOrbitalDotsCountPropertionalToCircumference;
    [SerializeField] private float unitsOfArcPerDot;
    [SerializeField] private GameObject orbitalDotPrefab;

    public float Capacity => capacity;

    public EphemeralMultipliers RadiusMultipliers { get; private set; }
    public EphemeralMultipliers VelocityMultipliers { get; private set; }

    public List<Rock> rocks;

    private List<GameObject> orbitalDots;
    private float cachedRadius;
    private float cachedOrbitalDotsCount;
    private float cachedDotsPerUnitOfArc;
    private float radians = 0f;

    private void Awake()
    {
        if (isOrbitalDotsCountPropertionalToCircumference)
        {
            orbitalDotsCount = Mathf.RoundToInt((2 * Mathf.PI * radius) * unitsOfArcPerDot);
        }

        VelocityMultipliers = new();
        RadiusMultipliers = new();

        rocks = new();

        orbitalDots = new();
        cachedRadius = radius;
        cachedOrbitalDotsCount = orbitalDotsCount;
        cachedDotsPerUnitOfArc = unitsOfArcPerDot;
        RefreshOrbitalDots();
    }

    private void Update()
    {
        float processedRadius = GetRadius();
        float processedVelocity = GetVelocity();

        if (cachedRadius != radius || cachedOrbitalDotsCount != orbitalDotsCount || cachedDotsPerUnitOfArc != unitsOfArcPerDot)
        {
            cachedRadius = radius;
            cachedOrbitalDotsCount = orbitalDotsCount;
            cachedDotsPerUnitOfArc = unitsOfArcPerDot;
            orbitalDotsCount = Mathf.RoundToInt((2 * Mathf.PI * radius) * unitsOfArcPerDot);
            RefreshOrbitalDots();
        }

        radians += (Time.deltaTime * processedVelocity) / processedRadius;
        for (int i = 0; i < rocks.Count; i++)
        {
            Rock rock = rocks[i];
            float offset = rocks.Count <= 1 ? 0 : 2 * Mathf.PI * ((float)i / rocks.Count - 1);
            float processedRadians = radians + offset;
            Vector3 pos = player.position + (new Vector3(Mathf.Cos(processedRadians), Mathf.Sin(processedRadians)) * processedRadius);
            rock.transform.position = Vector3.SmoothDamp(rock.transform.position, pos, ref rock.velocity, smoothSpeed);
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

    public float GetRadius()
    {
        return RadiusMultipliers.GetProduct(radius);
    }
    public float GetVelocity()
    {
        return VelocityMultipliers.GetProduct(velocity);
    }

    public void Add(Rock rock)
    {
        rocks.Add(rock);
    }

    public void Remove(Rock rock)
    {
        rocks.Remove(rock);
    }

    public void RemoveAll()
    {
        rocks.ForEach(rock => Destroy(rock.gameObject));
        rocks.Clear();
    }
}
