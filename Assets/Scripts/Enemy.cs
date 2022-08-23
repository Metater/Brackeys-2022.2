using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected Transform player;
    [SerializeField] protected Rigidbody2D rb;

    public float health = 0;

    public void Init(Transform player)
    {
        this.player = player;
    }

    public void Damage(float amount)
    {
        health -= amount;

        if (health <= 0)
        {
            // Die
            Destroy(gameObject);
            OnDie();
        }
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
            rock.OnEnemyEnter(this, collider);
            OnRockEnter(rock, collider);
        }
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.TryGetComponent(out Rock rock))
        {
            rock.OnEnemyStay(this, collider);
            OnRockStay(rock, collider);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.TryGetComponent(out Rock rock))
        {
            rock.OnEnemyExit(this, collider);
            OnRockExit(rock, collider);
        }
    }
}