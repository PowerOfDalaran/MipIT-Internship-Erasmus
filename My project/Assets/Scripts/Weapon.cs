using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField]
    protected GameObject projectilePrefab;
    [SerializeField]
    protected float fireCooldown = 2;
    [SerializeField]
    protected float projectileSpeed = 2;

    protected float cooldownCounter = 0;
    protected Vector3 weaponPosition;
    protected CharacterController characterController;

    public bool canFire = true;

    public virtual void Awake()
    { 
        characterController = gameObject.GetComponent<CharacterController>();
    }

    public virtual void FixedUpdate()
    {
        //Checking if cooldown has passed
        if (Time.time > cooldownCounter)
        {
            canFire = true;
            cooldownCounter = 0;
        }
        else
        {
            canFire = false;
        }
    }

    //Method launching the weapon
    public virtual void Fire()
    {
        weaponPosition = gameObject.transform.Find("WeaponPosition").transform.position;
        Debug.Log("Weapon Fired!");
        cooldownCounter = Time.time + fireCooldown;
    }
}
