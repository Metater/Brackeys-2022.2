using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private bool hasTumble;
    [SerializeField] private bool hasRandomTumbleSpeed;
    [SerializeField] private Vector2 tumbleSpeedRange;

    [System.NonSerialized] public Vector3 velocity;

    public float dragRadius;

    public bool IsGrounded { get; private set; } = false;
    public RockOrbital Orbital { get; private set; } = null;

    public EphemeralMultipliers TumbleSpeedMultipliers { get; private set; }

    private float tumbleSpeed;

    public void Init(Transform player)
    {
        this.player = player;
    }

    public void SetOrbital(RockOrbital orbital)
    {
        IsGrounded = false;
        Orbital = orbital;
        orbital.Add(this);
    }

    public void SetGrounded()
    {
        IsGrounded = true;
        Orbital = null;
    }

    private void Awake()
    {
        TumbleSpeedMultipliers = new();

        tumbleSpeed = hasRandomTumbleSpeed ? Random.Range(tumbleSpeedRange.x, tumbleSpeedRange.y) : tumbleSpeedRange.x;
    }

    private void Update()
    {
        if (!IsGrounded)
        {
            if (hasTumble)
            {
                transform.localEulerAngles = new Vector3(0f, 0f, transform.localEulerAngles.z + (Time.deltaTime * TumbleSpeedMultipliers.GetProduct(tumbleSpeed)));
            }
            else
            {
                var angle = AngleBetweenTwoPoints(transform.position, player.position);
                transform.localEulerAngles = new Vector3(0f, 0f, angle - 90f);
            }
        }
        else
        {
            transform.localEulerAngles = Vector3.zero;
        }
    }

    private float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
}
