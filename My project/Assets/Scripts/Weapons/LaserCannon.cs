using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCannon : Weapon
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Method launching the weapon - some problems with rotating the projectiles
    public override void Fire()
    {
        base.Fire();
        GameObject projectile = Instantiate(projectilePrefab, weaponPosition, Quaternion.identity);
        //projectile.transform.Rotate(new Vector3(0, 0, 90));
        projectile.GetComponent<Projectile>().LaunchProjectile(gameObject.transform.up);
        //projectileRigidBody.velocity = transform.forward * projectileSpeed;
        //projectileRigidBody.AddForce(gameObject.transform.forward);
        //Debug.Log(gameObject.transform.rotation);
    }
}
