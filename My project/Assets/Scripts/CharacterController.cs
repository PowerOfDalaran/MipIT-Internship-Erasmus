using UnityEngine;

public class CharacterController : MonoBehaviour
{
    int damageTreshold = 1;
    [SerializeField] float speed = 3;
    [SerializeField] float rotationSpeed = 0.1f;
    float maxHealth = 20;
    [SerializeField] float currentHealth;
    float forceAmount = -10.0f;
    float torqueDirection = 0.0f;
    float torqueAmount = 0.5f;

    bool rotate_right = false;
    bool rotate_left = false;
    bool moveForward = false;
    bool fireWeapon = false;
    bool respawning = false;

    Weapon currentWeapon;
    Animator characterAnimator;
    Rigidbody2D rigidBody2D;
    GameObject flame;
    GameObject impact02;

    void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        currentWeapon = gameObject.GetComponent<Weapon>();
        currentHealth = maxHealth;

        //Variables for flame object(image with animation) and death animation
        flame = GameObject.Find("flame");
        characterAnimator = gameObject.GetComponent<Animator>();

        impact02 = GameObject.Find("Impact02");
        impact02.SetActive(false);
    }

    void Update()
    {
        //Temporary trigger for testing the game with keyboards
        if(Input.GetKey(KeyCode.Space))
        {
            fireWeapon = true;
        }
        else
        {
            fireWeapon = false;
        }
        if(Input.GetKey(KeyCode.A))
        {
            rotate_left = true;
        }
        else
        {
            rotate_left = false;
        }
        if(Input.GetKey(KeyCode.D))
        {
            rotate_right = true;
        }
        else
        {
            rotate_right = false;
        }
        if(Input.GetKey(KeyCode.W))
        {
            moveForward = true;
        }
        else
        {
            moveForward = false;
        }

        //Choosing direction which player want to rotate
        if(rotate_left)
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

        //Checking if player should be killed
        if(currentHealth <= 0)
        {
            impact02.SetActive(true);
            KillPlayer();
        }

        //Activating weapon if player isn't respawning
        if (fireWeapon && respawning != true)
        {
            currentWeapon.Fire();
        }
    }

    private void FixedUpdate()
    {
        //Adding force to player character and activating/deactivating the flame object (image with animation)
        if (moveForward)
        {
            flame.SetActive(true);
            rigidBody2D.AddForce(-transform.up * forceAmount * speed);
        }
        else 
        {
            flame.SetActive(false);
        }

        //Rotating ship in direction based on trigger
        if(torqueDirection!=0)
        {
            rigidBody2D.AddTorque(torqueDirection * torqueAmount);
            //rigidBody2D.SetRotation(new Quaternion(gameObject.transform.rotation.x, gameObject.transform.rotation.y, gameObject.transform.rotation.z + (torqueDirection * rotationSpeed), gameObject.transform.rotation.w));
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Dealing damage to player if he flied into asteroid
        if(collision.gameObject.tag == "Asteroid")
        {
            float damage = collision.relativeVelocity.magnitude * collision.gameObject.GetComponent<Rigidbody2D>().mass;
            DealDamage(damage);

            //Dealing damage to asteroid
            collision.gameObject.GetComponent<AsteroidController>().DealDamage(1);
        }
        //Dealing damage to player if he flied into terrain
        else if(collision.gameObject.tag == "Obstacle")
        {
            float damage = collision.relativeVelocity.magnitude;
            DealDamage(damage);

        }
        //Killing player if he flied into deathzone and activating explosion animation
        else if(collision.gameObject.tag == "DeathZone")
        {
            impact02.SetActive(true);
            KillPlayer();
        }
    }

    //Dealing damage to player character
    public void DealDamage(float damage)
    {
        Debug.Log(damage);
        if(damage >= damageTreshold)
        {
            currentHealth -= damage;
        }
    }

    //Method killing the player and starting his respawn at original position
    void KillPlayer()
    {
        //Changing position of player, resetting his velocity and health
        transform.position = new Vector2(0f, 0f);
        rigidBody2D.velocity = Vector2.zero;
        rigidBody2D.angularVelocity = 0f;
        currentHealth = maxHealth;

        //Activating invcibility, turning on respawning animation, blocking shooting and invoking deactivation of his incibility
        TurnOffVisibility();
        characterAnimator.SetBool("respawned", true);
        respawning = true;
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
        
        //Deactivating respawning animation and allowing player to shoot
        characterAnimator.SetBool("respawned", false);
        respawning = false;
    }

    //Methods for buttons for turning on triggers
    public void MoveForwardButtonPressed()
    {
        moveForward = true;
    }
    public void ShootButtonPressed()
    {
        fireWeapon = true;
    }
    public void RotateRightButtonPressed()
    {
        rotate_right = true;
    }
    public void RotateLeftButtonPressed()
    {
        rotate_left = true;
    }

    //Method for turning off all triggers
    public void NoButtonPressed()
    {
        rotate_right = false;
        rotate_left = false;
        moveForward = false;
        fireWeapon = false;
    }
}
