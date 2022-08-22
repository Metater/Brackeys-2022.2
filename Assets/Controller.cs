using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;

    [SerializeField] private List<RockOrbital> orbitals;

    private void Start()
    {

    }

    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        rb.velocity = speed * new Vector3(x, y);
    }
}
