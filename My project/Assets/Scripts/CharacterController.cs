using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField]
    float speed = 3;
    [SerializeField]
    float rotationSpeed = 3;

    bool forceOn = false;
    float forceAmount = -10.0f;
    float torqueDirection = 0.0f;
    float torqueAmount = 0.5f;

    Weapon currentWeapon;
    Rigidbody2D rigidBody2D;
    GameObject flame;
    GameObject impact02;

    void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        currentWeapon = gameObject.GetComponent<Weapon>();
        flame = GameObject.Find("flame");
        impact02 = GameObject.Find("Impact02");
        impact02.SetActive(false);
    }

    void Update()
    {
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
    }

    //Adding velocity to the player or reducing it, depending if key is on or off
    private void FixedUpdate()
    {
        if(forceOn)
        {
            rigidBody2D.AddForce(-transform.up * forceAmount * speed);
        }

        if(torqueDirection!=0)
        {
            rigidBody2D.AddTorque(torqueDirection * torqueAmount * rotationSpeed);
        }

        if (rigidBody2D.velocity.magnitude == 0)
        {
            flame.SetActive(false);
        }
        else
        {
            flame.SetActive(true);
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

    //Checking if player got rekt - probably need to debug it later
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Asteroid" || collision.gameObject.tag == "DeathZone")
        {
            impact02.SetActive(true);
            GetRekt();
        }
    }

    //Initating death of the player and his respawn - probably need to debug it later
    void GetRekt()
    {
        transform.position = new Vector2(0f, 0f);
        rigidBody2D.velocity = Vector2.zero;
        rigidBody2D.angularVelocity = 0f;
        turnOffVisibility();
        Invoke("Respawn", 3f);
    }

    //Making player immortal
    void turnOffVisibility()
    {
        gameObject.layer = LayerMask.NameToLayer("IgnoreAsteroids");
    }

    //Respawning player
    void Respawn()
    {
        Invoke("turnOnVisibility", 3f);
    }

    //Making player mortal
    void turnOnVisibility()
    {
        gameObject.layer = LayerMask.NameToLayer("Ship");
    }
}
