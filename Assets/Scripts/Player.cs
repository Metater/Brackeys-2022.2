using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] ShakeCamera shakeCamera;
    [SerializeField] List<GameObject> hearts = new List<GameObject>();
    float OGspeed;
    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        rb.velocity = speed * new Vector3(x, y);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(shakeCamera.Shake(.15f, .6f));
        }

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
