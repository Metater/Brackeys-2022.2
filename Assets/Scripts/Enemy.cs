using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected Transform player;
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected float damageCooldown;

    public float health = 0;

    public bool IsCurrentlyDamagable => Time.time >= timeWhenDamagableAgain;

    private float timeWhenDamagableAgain = 0f;

    public void Init(Transform player)
    {
        this.player = player;
    }

    public bool Damage(float amount)
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
        }
        return true;
    }

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