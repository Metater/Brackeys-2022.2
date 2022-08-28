using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeEnemy : Enemy
{
    [SerializeField] private float rockRepulsion;
    [SerializeField] private float turnSmoothSpeed;
    [SerializeField] private bool hasSwerve;
    [SerializeField] private float swerveFrequency;
    [SerializeField] private float swerveAmplitude;

    private float turnVelocity = 0f;


    private void FixedUpdate()
    {
        float angleToTarget = Utils.AngleBetweenTwoPoints(player.transform.position, transform.position) - 90f;
        transform.localEulerAngles = new Vector3(0f, 0f, Mathf.SmoothDampAngle(transform.localEulerAngles.z, angleToTarget, ref turnVelocity, turnSmoothSpeed));

        if (ignitedUntil > Time.time)
        {
            DamageNoCooldown(null, ignitedDps * Time.fixedDeltaTime);
        }
    }

    protected override void OnRockEnter(Rock rock, Collider2D collider)
    {
        if (IsCurrentlyDamagable)
        {
            rb.AddForce((rock.transform.position - player.transform.position).normalized * rockRepulsion, ForceMode2D.Impulse);
        }
    }

    private float slowUntil = 0;
    private float slowMultiplier = 1f;

    public override void Slowness(float time, float mutliplier)
    {
        float timeUntil = Time.time + time;
        if (timeUntil > slowUntil)
        {
            slowUntil = timeUntil;
            slowMultiplier = mutliplier;
        }
    }

    private float scaredUntil = 0;
    private float scaredMultiplier = 1f;

    public override void Scare(float time, float mutliplier)
    {
        float timeUntil = Time.time + time;
        if (timeUntil > scaredUntil)
        {
            scaredUntil = timeUntil;
            scaredMultiplier = mutliplier;
        }
    }

    private float ignitedUntil = 0;
    private float ignitedDps = 0f;

    public override void Ignite(float time, float dps)
    {
        float timeUntil = Time.time + time;
        if (timeUntil > ignitedUntil)
        {
            ignitedUntil = timeUntil;
            ignitedDps = dps;
        }
    }
}
