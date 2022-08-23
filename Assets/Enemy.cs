using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected Transform player;
    [SerializeField] private float startingHealth;

    public float Health { get; private set; } = 0;

    public void Init(Transform player)
    {
        this.player = player;

        Health = startingHealth;
    }

    public void Damage(float amount)
    {
        Health -= amount;

        if (Health <= 0)
        {
            // Die
            Destroy(gameObject);
            OnDie();
        }
    }

    protected virtual void OnRockEnter(Rock rock)
    {

    }
    protected virtual void OnRockStay(Rock rock)
    {

    }
    protected virtual void OnRockExit(Rock rock)
    {

    }
    protected virtual void OnDie()
    {

    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent(out Rock rock))
        {
            rock.OnEnemyEnter(this);
            OnRockEnter(rock);
        }
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.TryGetComponent(out Rock rock))
        {
            rock.OnEnemyStay(this);
            OnRockStay(rock);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.TryGetComponent(out Rock rock))
        {
            rock.OnEnemyExit(this);
            OnRockExit(rock);
        }
    }
}