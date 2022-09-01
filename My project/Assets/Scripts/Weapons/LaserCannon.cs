using UnityEngine;

public class LaserCannon : Weapon
{
    [SerializeField] AudioClip blastClip;

    public override void Awake()
    {
        base.Awake();
        
        //Setting the features of this specific weapon
        fireCooldown = 0.8f;
    }

    //Method shooting the weapon
    public override void Fire()
    {
        base.Fire();

        if(canFire)
        {
            //Instantiating the projectile, setting its rotation, launching its and rotating it again(all images are turned into wrong dierection)
            GameObject projectile = Instantiate(projectilePrefab, weaponPosition, Quaternion.identity);

            projectile.transform.localRotation = gameObject.transform.rotation.normalized;
            projectile.GetComponent<Projectile>().LaunchProjectile(gameObject.transform.up);
            projectile.transform.Rotate(0, 0, 90);

            //Playing sound effect of the weapon
            musicManager.PlaySingleSound(blastClip);
        }
    }
}
