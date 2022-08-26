using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    //[SerializeField]
    //float movementSpeed = 2;
    [SerializeField]
    float maxVelocity = 3;
    [SerializeField]
    float rotationSpeed = 3;

    bool forceOn = false;
    float forceAmount = -10.0f;
    float torqueDirection = 0.0f;
    float torqueAmount = 0.5f;

    Weapon currentWeapon;
    Rigidbody2D rigidBody2D;

    void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        currentWeapon = gameObject.GetComponent<Weapon>();
    }

    void Update()
    {
        //OLD - Getting input
        /*
        float yAxis = Input.GetAxis("Vertical");
        float xAxis = Input.GetAxis("Horizontal");

        //Moving and rotating if input positive
        ThrustForward(yAxis);
        Rotate(transform, -xAxis * rotationSpeed);
        */

        //Collecting inputs and adding values for movement
        forceOn = Input.GetKey(KeyCode.W);

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

        //Activating weapon
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UseWeapon();
        }

        //Calling the teleporting method
        wrapAroundBoundary();
    }

    //Adding velocity to the player or reducing it, depending if key is on or off
    private void FixedUpdate()
    {
        if(forceOn)
        {
            rigidBody2D.AddForce(transform.up * forceAmount);
        }

        if(torqueDirection!=0)
        {
            rigidBody2D.AddTorque(torqueDirection * torqueAmount);
        }
    }

    //Firing weapon if its ready
    void UseWeapon()
    {
        if (currentWeapon.canFire)
        {
            currentWeapon.Fire();
        }
        else
        {
            Debug.Log("Weapon on cooldown!");
        }
    }

    //OLD Movement
    /*
    private void ClampVelocity()
    {
        float x = Mathf.Clamp(rigidBody2D.velocity.x, -maxVelocity, maxVelocity);
        float y = Mathf.Clamp(rigidBody2D.velocity.y, -maxVelocity, maxVelocity);

        rigidBody2D.velocity = new Vector2(x,y);
    }

    private void ThrustForward(float amount)
    {
        Vector2 force = transform.up * amount;
        rigidBody2D.AddForce(force);
    }

    //Rotation
    private void Rotate(Transform t, float amount)
    {
        t.Rotate(0, 0, amount);
    }
    */

    //Teleporting player to opposite part of the map - probably gonna delete later, needed for the tutorial
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

    //Checking if player got rekt - probably need to debug it later
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Asteroid")
        {
            GetRekt();
        }
    }

    //Initating death of the player and his respawn - probably need to debug it later
    void GetRekt()
    {
        transform.position = new Vector2(4000f, 4000f);
        rigidBody2D.velocity = Vector2.zero;
        rigidBody2D.angularVelocity = 0f;
        turnOnVisibility();
        Invoke("reset", 3f);
    }

    //Making player immortal
    void turnOffVisibility()
    {
        gameObject.layer = LayerMask.NameToLayer("Ignore");
    }

    //Respawning player
    void Respawn()
    {
        transform.position = new Vector2(0f, 0f);
        transform.eulerAngles = new Vector3(0f, 0f, 0f);
        Invoke("turnOnCollisions", 3f);
    }

    //Making player mortal
    void turnOnVisibility()
    {
        gameObject.layer = LayerMask.NameToLayer("Ship");
    }
}
