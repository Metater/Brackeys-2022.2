using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;

    private float xInput = 0f;
    private float yInput = 0f;

    private void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
        rb.velocity = speed * new Vector3(yInput, yInput);
    }

    public Vector2 GetFuturePosition(float time)
    {
        return rb.position + (time * speed * new Vector2(xInput, yInput));
    }
}
