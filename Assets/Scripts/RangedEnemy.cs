using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
    [SerializeField] private Vector2 attackRingRadii;
    [SerializeField] private Vector2 attackRingRadiiRandomOffsetRange;
    [SerializeField] private float attackRingUnitsOfArcPerPoint;
    [SerializeField] private float attackRingMovementDirectionReversalChance;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float attackTime;

    [SerializeField] private float attackRingSpeed;

    [SerializeField] private float rockRepulsion;
    [SerializeField] private float turnSmoothSpeed;
    [SerializeField] private bool hasSwerve;
    [SerializeField] private float swerveFrequency;
    [SerializeField] private float swerveAmplitude;

    [SerializeField] private float radiansPushbackArcLength;
    [SerializeField] private float radiansPushbackDistanceThreshold;

    [SerializeField] GameObject projectile;


    // ring: min radius, max radius
    // walk towards closest point on ring
    // randomly choose direction to circle
    // start attack cooldown
    // when attack cooldown is done
    // aim towards player
    // when aimed within a range
    // shoot towards predicted player location
    // or maybe no predicted, shoot towards current location
    // just make the web big?
    // finish shot and set cooldown again

    private float AttackRingAverageRadius => (attackRingRadii.x + attackRingRadii.y) / 2f;
    private bool IsOnAttackCooldown => ((Time.time - startTime) % (attackCooldown + attackTime)) <= attackCooldown;

    private float radians = 0f;
    private bool isAttackRingMovementDirectionReversed;

    private float turnVelocity = 0f;

    private float startTime = 0f;
    private bool attacked = false;

    private void Awake()
    {
        float attackRingRadiiRandomOffset = Random.Range(attackRingRadiiRandomOffsetRange.x, attackRingRadiiRandomOffsetRange.y);
        attackRingRadii.x += attackRingRadiiRandomOffset;
        attackRingRadii.y += attackRingRadiiRandomOffset;

        startTime = Time.time;

        isAttackRingMovementDirectionReversed = Random.value > 0.5f;
    }

    private int fixedUpdateCount = 0;

    private void FixedUpdate()
    {
        if (!IsWithinAttackRing())
        {
            radians = GetClosestPointStepOnAttackRing();
        }

        Vector2 attackRingTarget = GetPointOnAttackRing(radians);
        Vector2 target = IsOnAttackCooldown ? attackRingTarget : player.transform.position;
        float angleToTarget = Utils.AngleBetweenTwoPoints(target, transform.position) - 90f;
        transform.localEulerAngles = new Vector3(0f, 0f, Mathf.SmoothDampAngle(transform.localEulerAngles.z, angleToTarget, ref turnVelocity, turnSmoothSpeed));
        Vector3 vector = hasSwerve ? (transform.up + (swerveAmplitude * Mathf.Sin(Time.time * swerveFrequency) * transform.right)).normalized : transform.up;

        if (IsOnAttackCooldown)
        {
            attacked = false;
            float multiplier = slowUntil < Time.time ? 1 : slowMultiplier;
            rb.AddForce((IsWithinAttackRing() ? attackRingSpeed : GetSpeed()) * multiplier * vector, ForceMode2D.Force);
            if (Vector2.Distance(attackRingTarget, transform.position) <= radiansPushbackDistanceThreshold)
            {
                float averageRadius = AttackRingAverageRadius;
                radians += (radiansPushbackArcLength * averageRadius * (isAttackRingMovementDirectionReversed ? -1f : 1f));
            }
        }
        else
        {
            if (!attacked)
            {
                attacked = true;
                if (scaredUntil < Time.time && Vector2.Distance(player.transform.position, transform.position) < 6.35f)
                {
                    Attack();
                }
            }
        }

        if (ignitedUntil > Time.time)
        {
            DamageNoCooldown(null, ignitedDps * Time.fixedDeltaTime);
        }

        fixedUpdateCount++;
        if (fixedUpdateCount % 50 == 0) // Once a second
        {
            bool shouldReverseDirection = Random.value <= attackRingMovementDirectionReversalChance;
            if (shouldReverseDirection)
            {
                isAttackRingMovementDirectionReversed = !isAttackRingMovementDirectionReversed;
            }
        }
    }

    private void Attack()
    {
        Instantiate(projectile, transform.position,transform.rotation);
    }

    private bool IsWithinAttackRing()
    {
        float distance = Vector2.Distance(player.transform.position, transform.position);
        return distance >= attackRingRadii.x && distance <= attackRingRadii.y;
    }

    private Vector2 GetPointOnAttackRing(float step)
    {
        return player.transform.position + (new Vector3(Mathf.Cos(step), Mathf.Sin(step)) * AttackRingAverageRadius);
    }

    private float GetClosestPointStepOnAttackRing()
    {
        float closestPointDistance = float.MaxValue;
        float closestPointStep = 0f;
        int attackRingPoints = Mathf.RoundToInt((2 * Mathf.PI * AttackRingAverageRadius) * attackRingUnitsOfArcPerPoint);
        for (int i = 0; i < attackRingPoints; i++)
        {
            float step = 2 * Mathf.PI * ((float)i / attackRingPoints - 1);
            Vector2 point = GetPointOnAttackRing(step);
            float pointDistance = Vector2.Distance(point, transform.position);
            if (closestPointDistance > pointDistance)
            {
                closestPointDistance = pointDistance;
                closestPointStep = step;
            }
        }
        return closestPointStep;
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