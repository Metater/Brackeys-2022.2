using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    GameObject player;
    [SerializeField] private float turnSmoothSpeed;
    [Range(10,20)] public float step;
    private float turnVelocity = 0f;
    Vector3 target;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        target = player.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, step * Time.deltaTime);
        float angleToTarget = Utils.AngleBetweenTwoPoints(player.transform.position, transform.position) + 180f;
        transform.localEulerAngles = new Vector3(0f, 0f, Mathf.SmoothDampAngle(transform.localEulerAngles.z, angleToTarget, ref turnVelocity, turnSmoothSpeed));

        if(transform.position == target)
        {
            Destroy(this.gameObject);
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }
}
