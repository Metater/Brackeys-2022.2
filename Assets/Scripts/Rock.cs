using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Rock : MonoBehaviour
{
    [SerializeField] protected Transform player;
    [SerializeField] protected bool hasTumble;
    [SerializeField] protected bool hasRandomTumbleSpeed;
    [SerializeField] protected Vector2 tumbleSpeedRange;

    [System.NonSerialized] public Vector3 velocity;

    public float dragRadius;

    public bool IsGrounded { get; private set; } = false;
    public RockOrbital Orbital { get; private set; } = null;

    public EphemeralMultipliers TumbleSpeedMultipliers { get; private set; }

    private float tumbleSpeed;

    public void Init(Transform player)
    {
        this.player = player;

        TumbleSpeedMultipliers = new();
        tumbleSpeed = hasRandomTumbleSpeed ? Random.Range(tumbleSpeedRange.x, tumbleSpeedRange.y) : tumbleSpeedRange.x;
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

    public virtual void OnEnemyEnter(Enemy enemy, Collider2D collider)
    {

    }
    public virtual void OnEnemyStay(Enemy enemy, Collider2D collider)
    {

    }
    public virtual void OnEnemyExit(Enemy enemy, Collider2D collider)
    {

    }

    protected virtual void InternalUpdate()
    {

    }

    private void Update()
    {
        InternalUpdate();

        UpdateRotation();
    }

    private void UpdateRotation()
    {
        if (!IsGrounded)
        {
            if (hasTumble)
            {
                transform.localEulerAngles = new Vector3(0f, 0f, transform.localEulerAngles.z + (Time.deltaTime * TumbleSpeedMultipliers.GetProduct(tumbleSpeed)));
            }
            else
            {
                var angle = Utils.AngleBetweenTwoPoints(transform.position, player.position);
                transform.localEulerAngles = new Vector3(0f, 0f, angle - 90f);
            }
        }
        else
        {
            transform.localEulerAngles = Vector3.zero;
        }
    }
}
