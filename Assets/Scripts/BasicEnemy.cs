using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : Enemy
{
    [SerializeField] private float rockRepulsion;
    [SerializeField] private float turnSmoothSpeed;
    [SerializeField] private bool hasSwerve;
    [SerializeField] private float swerveFrequency;
    [SerializeField] private float swerveAmplitude;

    private float turnVelocity = 0f;

    private void Update()
    {
        float angleToTarget = Utils.AngleBetweenTwoPoints(player.transform.position, transform.position) - 90f;
        transform.localEulerAngles = new Vector3(0f, 0f, Mathf.SmoothDampAngle(transform.localEulerAngles.z, angleToTarget, ref turnVelocity, turnSmoothSpeed));
        Vector3 vector = hasSwerve ? (transform.up + (swerveAmplitude * Mathf.Sin(Time.time * swerveFrequency) * transform.right)).normalized : transform.up;
        rb.AddForce(vector * GetSpeed(), ForceMode2D.Force);
    }

    protected override void OnRockEnter(Rock rock, Collider2D collider)
    {
        if (IsCurrentlyDamagable)
        {
            rb.AddForce((rock.transform.position - player.transform.position).normalized * rockRepulsion, ForceMode2D.Impulse);
        }
    }
}
