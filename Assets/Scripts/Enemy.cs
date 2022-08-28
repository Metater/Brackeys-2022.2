using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected Player player;
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected float damageCooldown;
    [SerializeField] private float speed;

    public float health = 0;

    public bool IsCurrentlyDamagable => Time.time >= timeWhenDamagableAgain;

    public EphemeralMultipliers SpeedMultipliers { get; private set; }

    private float timeWhenDamagableAgain = 0f;

    public void Init(Player player)
    {
        this.player = player;

        SpeedMultipliers = new();
    }

    public float GetSpeed()
    {
        return SpeedMultipliers.GetProduct(speed);
    }

    public void DamageNoCooldown(Rock rock, float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            // Die
            Destroy(gameObject);
            OnDie();
            if (rock is not null)
            {
                rock.OnKill(this);
            }
        }
    }

    public bool Damage(Rock rock, float amount)
    {
        if (!IsCurrentlyDamagable)
        {
            return false;
        }

        health -= amount;
        timeWhenDamagableAgain = Time.time + damageCooldown;

        if (health <= 0)
        {
            // Die
            Destroy(gameObject);
            OnDie();
            rock.OnKill(this);
        }
        return true;
    }

    // No attack / weak attack
    public abstract void Weakness(float time, float mutliplier);
    // Slow speed
    public abstract void Slowness(float time, float mutliplier);
    // Flee from player
    public abstract void Scare(float time, float mutliplier);
    // Take burn damage
    public abstract void Ignite(float time, float dps);

    protected virtual void OnRockEnter(Rock rock, Collider2D collider)
    {

    }
    protected virtual void OnRockStay(Rock rock, Collider2D collider)
    {

    }
    protected virtual void OnRockExit(Rock rock, Collider2D collider)
    {

    }
    protected virtual void OnDie()
    {

    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent(out Rock rock))
        {
            OnRockEnter(rock, collider);
            rock.OnEnemyEnter(this, collider);
        }
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.TryGetComponent(out Rock rock))
        {
            OnRockStay(rock, collider);
            rock.OnEnemyStay(this, collider);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.TryGetComponent(out Rock rock))
        {
            OnRockExit(rock, collider);
            rock.OnEnemyExit(this, collider);
        }
    }
}