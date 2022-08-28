using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] ShakeCamera shakeCamera;
    [SerializeField] List<GameObject> hearts = new List<GameObject>();
    [SerializeField] GameOverManager gameOver;

    int health = 3;
    private float xInput = 0f;
    private float yInput = 0f;
    public float OGspeed;

    [SerializeField] GameObject damageLight;
    bool hurt = false;
    float timer;

    public int money;
    [SerializeField] TMP_Text moneyText;

    [SerializeField] GameObject sprite;
    public float rotateLerp;
    private float lastAngle = 0;

    private void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
        rb.velocity = speed * new Vector3(xInput, yInput);

        if (hurt && timer >= 0)
        {
            timer -= Time.deltaTime;
        }
        if(timer <= 0)
        {
            hurt = false;
            damageLight.SetActive(false);
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
            
            speed = OGspeed/2;
        }

        if (collision.CompareTag("Enemy") && !hurt)
        {
            damageLight.SetActive(true);
            timer = 0.5f;
            hurt = true;
            health--;
            UpdateHearts();
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Web"))
        {
            speed = OGspeed;
        }

    }

    private void FixedUpdate()
    {
        moneyText.text = money +"";

        Vector2 moveDirection = rb.velocity;
        if (moveDirection != Vector2.zero)
        {
            float angle = (Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg) - 90;
            float lerpAngle = Mathf.LerpAngle(lastAngle, angle, rotateLerp);
            sprite.transform.rotation = Quaternion.AngleAxis(lerpAngle, Vector3.forward);
            lastAngle = lerpAngle;
        }
    }

    void UpdateHearts()
    {
        if(health == 3)
        {
            hearts[2].SetActive(true);
            hearts[1].SetActive(true);
            hearts[0].SetActive(true);
        }
        else if(health == 2)
        {
            StartCoroutine(shakeCamera.Shake(.15f, .6f));
            hearts[2].SetActive(false);
        }
        else if(health == 1)
        {
            StartCoroutine(shakeCamera.Shake(.15f, .6f));
            hearts[1].SetActive(false);
        }else if(health <= 0)
        {
            StartCoroutine(shakeCamera.Shake(.15f, .6f));
            hearts[0].SetActive(false);
            this.enabled = false;
            Time.timeScale = 0;
            gameOver.GameOver();
        }
    }

    private void Start()
    {
        Time.timeScale = 1;
        UpdateHearts();
        OGspeed = speed;
    }
}
