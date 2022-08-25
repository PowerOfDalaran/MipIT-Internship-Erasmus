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

    Weapon currentWeapon;
    Rigidbody2D rigidBody2D;

    void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        currentWeapon = gameObject.GetComponent<Weapon>();
    }

    void Update()
    {
        //Getting input
        float yAxis = Input.GetAxis("Vertical");
        float xAxis = Input.GetAxis("Horizontal");

        //Moving and rotating if input positive
        ThrustForward(yAxis);
        Rotate(transform, -xAxis * rotationSpeed);

        //Activating weapon
        if (Input.GetKeyDown(KeyCode.F))
        {
            UseWeapon();
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

    //Movement
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
}
