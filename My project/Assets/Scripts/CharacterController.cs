using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] float speed = 3;
    [SerializeField] float rotationSpeed = 3;
    float forceAmount = -10.0f;
    float torqueDirection = 0.0f;
    float torqueAmount = 0.5f;

<<<<<<< Updated upstream
    bool forceOn = false;
=======
    bool rotate_right = false;
    bool rotate_left = false;
    bool moving = false;
    bool fire = false;
>>>>>>> Stashed changes

    Weapon currentWeapon;
    Rigidbody2D rigidBody2D;
    GameObject flame;
    GameObject impact02;

    void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        currentWeapon = gameObject.GetComponent<Weapon>();

        //Variables for flame object(image with animation) and death animation
        flame = GameObject.Find("flame");

        impact02 = GameObject.Find("Impact02");
        impact02.SetActive(false);
    }

    void Update()
    {
<<<<<<< Updated upstream
        //Checking if player want to move forward
        forceOn = Input.GetKey(KeyCode.W);

        //Choosing direction which player want to rotate
        if(Input.GetKey(KeyCode.A))
=======
        //Collecting inputs and adding values for movement
        //forceOn = Input.GetKey(KeyCode.W);

        if(rotate_left)
>>>>>>> Stashed changes
        {
            torqueDirection = 1f;
        }
        else if(rotate_right)
        {
            torqueDirection = -1f;
        }
        else
        {
            torqueDirection = 0f;
        }

<<<<<<< Updated upstream
        //Firing weapon
        if (Input.GetKeyDown(KeyCode.Space))
=======
        //Activating weapon
        if (fire)
>>>>>>> Stashed changes
        {
            currentWeapon.Fire();
        }
    }

    private void FixedUpdate()
    {
<<<<<<< Updated upstream
        //Adding force to player character and activating/deactivating the flame object (image with animation)
        if (forceOn)
=======
        if (moving)
>>>>>>> Stashed changes
        {
            flame.SetActive(true);
            rigidBody2D.AddForce(-transform.up * forceAmount * speed);
        }
        else 
        {
            flame.SetActive(false);
        }

        //Rotating ship in direction based on pressed keyboard button
        if(torqueDirection!=0)
        {
            rigidBody2D.AddTorque(torqueDirection * torqueAmount * rotationSpeed);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Killing the player if he got hit by asteroid or flied into deathzone and activating explosion animation
        if(collision.gameObject.tag == "Asteroid" || collision.gameObject.tag == "DeathZone")
        {
            impact02.SetActive(true);
            KillPlayer();
        }
    }

    //Method killing the player and starting his respawn at original position
    void KillPlayer()
    {
        //Changin position of player and resetting his velocity
        transform.position = new Vector2(0f, 0f);
        rigidBody2D.velocity = Vector2.zero;
        rigidBody2D.angularVelocity = 0f;

        //Activating invcibility and invoking deactivation of his incibility
        TurnOffVisibility();
        Invoke("TurnOnVisibility", 6f);
    }

    //Changing layer of the player, so now he will be ignored by asteroids
    void TurnOffVisibility()
    {
        gameObject.layer = LayerMask.NameToLayer("IgnoreAsteroids");
    }

    //Changing layer of the player back to the original one, in which he can be killed again
    void TurnOnVisibility()
    {
        gameObject.layer = LayerMask.NameToLayer("Ship");
    }

    public void force()
    {
        moving = true;
    }
    
    public void shoot()
    {
        fire = true;
    }
    public void rotateRight()
    {
        rotate_right = true;
    }

    public void rotateLeft()
    {
        rotate_left = true;
    }

    public void stop()
    {
        rotate_right = false;
        rotate_left = false;
        moving = false;
        fire = false;
    }
}
