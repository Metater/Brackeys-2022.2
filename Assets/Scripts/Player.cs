using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] ShakeCamera shakeCamera;
    [SerializeField] List<GameObject> hearts = new List<GameObject>();

    private float xInput = 0f;
    private float yInput = 0f;
    float OGspeed;
    private void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
        rb.velocity = speed * new Vector3(yInput, yInput);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(shakeCamera.Shake(.15f, .6f));
        }
    }

    public Vector2 GetFuturePosition(float time)
    {
        return rb.position + (time * speed * new Vector2(xInput, yInput));
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Web"))
        {
            OGspeed = speed;
            speed = OGspeed - 5;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Web"))
        {
            speed = OGspeed;
        }

    }
}
