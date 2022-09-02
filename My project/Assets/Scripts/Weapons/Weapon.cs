using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected float fireCooldown = 2;
    protected float cooldownCounter = 0;

    public bool canFire = true;

    protected Vector3 weaponPosition;
    [SerializeField] protected GameObject projectilePrefab;
    protected CharacterController characterController;
    protected MusicManager musicManager;

    public virtual void Awake()
    { 
        characterController = gameObject.GetComponent<CharacterController>();
        musicManager = GameObject.Find("MusicManager").GetComponent<MusicManager>();
    }

    public virtual void FixedUpdate()
    {
        //Checking if cooldown has passed and weapon can shoot again
        if (Time.time > cooldownCounter)
        {
            canFire = true;
        }
        else
        {
            canFire = false;
        }
    }

    //Method setting the position for creating the projectile and resetting the couldown
    public virtual void Fire()
    {
        if(canFire)
        {
            weaponPosition = gameObject.transform.Find("WeaponPosition").transform.position;
            cooldownCounter = Time.time + fireCooldown;            
        }
    }
}
