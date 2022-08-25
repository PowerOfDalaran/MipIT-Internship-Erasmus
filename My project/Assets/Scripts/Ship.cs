using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public Bullet bullet;
    Rigidbody2D rb;
    SpriteRenderer sr;
    bool forceOn = false;
    float forceAmount = -10.0f;
    float torqueDirection = 0.0f;
    float torqueAmount = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Bullet theBullet = Instantiate(bullet, transform.position, Quaternion.identity);
            theBullet.shoot(transform.up);
        }

        //move forward
        forceOn = Input.GetKey(KeyCode.W);

        //move right/left
        if(Input.GetKey(KeyCode.A))
        {
            torqueDirection = 1f;
        }
        else if(Input.GetKey(KeyCode.D))
        {
            torqueDirection = -1f;
        }
        else
        {
            torqueDirection = 0f;
        }
        wrapAroundBoundary();
    }
    void wrapAroundBoundary()
    {
        float x = transform.position.x;
        float y = transform.position.y;

        if(x>8f)
        {
            x = x - 16f;
        }
        else if(x<-8f)
        {
            x = x + 16f;
        }

        if(y>4.5f)
        {
            y = y - 9f;
        }
        else if(y<-4.5f)
        {
            y = y + 9f;
        }
        transform.position = new Vector2(x, y);
    }

    private void FixedUpdate()
    {
        if(forceOn)
        {
            rb.AddForce(transform.up * forceAmount);
        }

        if(torqueDirection!=0)
        {
            rb.AddTorque(torqueDirection*torqueAmount);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Asteroid")
        {
            transform.position = new Vector2(4000f, 4000f);
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
            turnOffCollisions();
            Invoke("reset", 3f);
        }
    }

    void turnOffCollisions()
    {
        gameObject.layer = LayerMask.NameToLayer("Ignore");
    }

    void reset()
    {
        transform.position = new Vector2(0f, 0f);
        transform.eulerAngles = new Vector3(0f, 0f, 0f);
        Invoke("turnOnCollisions", 3f);
    }

    void turnOnCollisions()
    {
        gameObject.layer = LayerMask.NameToLayer("Ship");
    }
}
