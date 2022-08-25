using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicLaserCannon : Weapon
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Method launching the weapon
    public override void Fire()
    {
        base.Fire();
        GameObject projectile = Instantiate(projectilePrefab, weaponPosition, new Quaternion(gameObject.transform.rotation.x, gameObject.transform.rotation.y, gameObject.transform.rotation.z + 90, gameObject.transform.rotation.w));
        Rigidbody2D projectileRigidBody = projectile.GetComponent<Rigidbody2D>();
        //projectileRigidBody.velocity = transform.forward * projectileSpeed;
        //projectileRigidBody.AddForce(gameObject.transform.forward);
        //Debug.Log(gameObject.transform.forward);
    }
}
