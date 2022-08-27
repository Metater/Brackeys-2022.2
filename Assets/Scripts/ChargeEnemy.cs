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

    bool charging;
    private float turnVelocity = 0f;

    float timer;


    private void Update()
    {
        float angleToTarget = Utils.AngleBetweenTwoPoints(player.transform.position, transform.position) - 90f;
        transform.localEulerAngles = new Vector3(0f, 0f, Mathf.SmoothDampAngle(transform.localEulerAngles.z, angleToTarget, ref turnVelocity, turnSmoothSpeed));
        
        if(timer >= 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            charging = false;
        }

        

    }

    protected override void OnRockEnter(Rock rock, Collider2D collider)
    {
        if (IsCurrentlyDamagable && !charging)
        {
            rb.AddForce((rock.transform.position - player.transform.position).normalized * rockRepulsion, ForceMode2D.Impulse);
        }
    }

    public override void Weakness(float time, float mutliplier)
    {

    }

    public override void Slowness(float time, float mutliplier)
    {

    }

    public override void Scare(float time, float mutliplier)
    {

    }

    public override void Ignite(float time, float dps)
    {

    }
}
