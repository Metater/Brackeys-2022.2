using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
    [SerializeField] private Vector2 attackRingRadii;
    [SerializeField] private int attackRingPoints;

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

    private bool hasReachedAttackRingYet = false;
    private float radians = 0f;

    private void Update()
    {
        if (!hasReachedAttackRingYet)
        {
            if (!IsWithinAttackRing)
            {

            }
            else
            {

            }
        }
        else
        {

        }
    }

    private Vector2 GetClosestPointOnAttackRing()
    {
        float closestPointDistance = float.MaxValue;
        Vector2 closestPoint = Vector2.zero;
        for (int i = 0; i < attackRingPoints; i++)
        {
            float step = 2 * Mathf.PI * ((float)i / attackRingSegments - 1);
            Vector2 point = GetPointOnAttackRing(step);
            float pointDistance = Vector2.Distance(point, transform.position);
            if (closestPointDistance > pointDistance)
            {
                closestPointDistance = pointDistance;
                closestPoint = point;
            }
        }
        return closestPoint;
    }

    private Vector2 GetPointOnAttackRing(float step)
    {
        reutrn player.transform.position + (new Vector2(Mathf.Cos(step), Mathf.Sin(step)) * radius);
    }

    protected override void OnRockEnter(Rock rock, Collider2D collider)
    {
        if (IsCurrentlyDamagable)
        {
            rb.AddForce((rock.transform.position - player.transform.position).normalized * rockRepulsion, ForceMode2D.Impulse);
        }
    }
}