using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSpiderAI : MonoBehaviour
{

    public int health;
    [SerializeField] GameObject deathParticles;

    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");   
    }

    // Update is called once per frame
    void Update()
    {
        transform.up = player.transform.position - transform.position;
        this.transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 0.015f);

        if(health <= 0)
        {

            Instantiate(deathParticles,this.transform.position, this.transform.rotation);
            Destroy(this.gameObject);
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Rock"))
        {
            Debug.Log("Collided");
            AttackStats attack = collision.GetComponent<AttackStats>();
            health -= attack.damage;
        }
    }



}
