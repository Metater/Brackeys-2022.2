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

    [SerializeField] private float turnSmoothSpeed;
    [SerializeField] private bool hasSwerve;
    [SerializeField] private float swerveFrequency;
    [SerializeField] private float swerveAmplitude;

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
    private bool IsWithinAttackRing => Vector2.Distance(player.transform.position, transform.position);
    private Vector2 CurrentAttackRingPoint => player.transform.position + (new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)) * AttackRingAverageRadius);

    private float radians = 0f;
    private bool isAttackRingMovementDirectionReversed = false;

    private void Awake()
    {
        float attackRingRadiiRandomOffset = Random.Range(attackRingRadiiRandomOffsetRange.x, attackRingRadiiRandomOffsetRange.y);
        attackRingRadii.x += attackRingRadiiRandomOffset;
        attackRingRadii.y += attackRingRadiiRandomOffset;
    }

    private int fixedUpdateCount = 0;
    private void FixedUpdate()
    {
        fixedUpdateCount++;
        if (fixedUpdateCount % 50 == 0) // Once a second
        {
            bool shouldReverseDirection = Random.range <= attackRingMovementDirectionReversalChance;
            if (shouldReverseDirection)
            {
                isAttackRingMovementDirectionReversed = !isAttackRingMovementDirectionReversed;
            }
        }
    }

    private void Update()
    {
        if (!IsWithinAttackRing)
        {
            radians = GetClosestPointStepOnAttackRing();
        }

        Vector2 target = CurrentAttackRingPoint;
        float angleToTarget = Utils.AngleBetweenTwoPoints(target, transform.position) - 90f;
        transform.localEulerAngles = new Vector3(0f, 0f, Mathf.SmoothDampAngle(transform.localEulerAngles.z, angleToTarget, ref turnVelocity, turnSmoothSpeed));
        Vector3 vector = hasSwerve ? (transform.up + (swerveAmplitude * Mathf.Sin(Time.time * swerveFrequency) * transform.right)).normalized : transform.up;
        rb.AddForce(vector * GetSpeed(), ForceMode2D.Force);

        // TODO No GetSpeed() here, just scoot radians along when you get close to the point you are aiming for.
        // ^^^ Also account for that mechanic when attacking
        radians += (Time.deltaTime * GetSpeed() * isAttackRingMovementDirectionReversed ? -1f : 1f) / AttackRingAverageRadius;
    }

    private void Shoot()
    {

    }

    private float GetClosestPointStepOnAttackRing()
    {
        float closestPointDistance = float.MaxValue;
        float closestPointStep = 0f;
        Vector2 closestPoint = Vector2.zero;
        int attackRingPoints = Mathf.RoundToInt((2 * Mathf.PI * AttackRingAverageRadius) * attackRingUnitsOfArcPerPoint);
        for (int i = 0; i < attackRingPoints; i++)
        {
            float step = 2 * Mathf.PI * ((float)i / attackRingSegments - 1);
            Vector2 point = GetPointOnAttackRing(step);
            float pointDistance = Vector2.Distance(point, transform.position);
            if (closestPointDistance > pointDistance)
            {
                closestPointDistance = pointDistance;
                closestPointStep = step;
                closestPoint = point;
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
}