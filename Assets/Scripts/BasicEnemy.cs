using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : Enemy
{
    [SerializeField] private float rockRepulsion;

    protected override void OnRockEnter(Rock rock, Collider2D collider)
    {
        rb.AddForce((rock.transform.position - player.position).normalized * rockRepulsion, ForceMode2D.Impulse);
    }
}
