using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCannon : Weapon
{
    //Method launching the weapon - some problems with rotating the projectiles
    public override void Fire()
    {
        base.Fire();
        GameObject projectile = Instantiate(projectilePrefab, weaponPosition, Quaternion.identity);
        projectile.transform.localRotation = gameObject.transform.rotation.normalized;
        projectile.GetComponent<Projectile>().LaunchProjectile(gameObject.transform.up);
        projectile.transform.Rotate(0, 0, 90);
    }
}
