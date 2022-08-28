using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Rock : MonoBehaviour
{
    [SerializeField] protected Player player;
    [SerializeField] protected bool hasTumble;
    [SerializeField] protected bool hasRandomTumbleSpeed;
    [SerializeField] protected Vector2 tumbleSpeedRange;

    [System.NonSerialized] public Vector3 velocity;

    [SerializeField] protected GameObject groundedLight;

    public float dragRadius;

    public bool IsGrounded { get; private set; } = true;
    public Orbital Orbital { get; private set; } = null;

    public EphemeralMultipliers TumbleSpeedMultipliers { get; private set; }

    private float tumbleSpeed;

    public void Init(Player player)
    {
        this.player = player;

        TumbleSpeedMultipliers = new();
        tumbleSpeed = hasRandomTumbleSpeed ? Random.Range(tumbleSpeedRange.x, tumbleSpeedRange.y) : tumbleSpeedRange.x;
    }

    public void SetOrbital(Orbital orbital)
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
    public virtual void OnKill(Enemy enemy)
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
            groundedLight.SetActive(false);
            if (hasTumble)
            {
                transform.localEulerAngles = new Vector3(0f, 0f, transform.localEulerAngles.z + (Time.deltaTime * TumbleSpeedMultipliers.GetProduct(tumbleSpeed)));
            }
            else
            {
                var angle = Utils.AngleBetweenTwoPoints(transform.position, player.transform.position);
                transform.localEulerAngles = new Vector3(0f, 0f, angle - 90f);
            }
        }
        else
        {
            transform.localEulerAngles = Vector3.zero;
            groundedLight.SetActive(true);
        }
    }
}
