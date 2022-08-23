using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    protected abstract void EnterRock(Rock rock);

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent(out Rock rock))
        {

        }
    }
}