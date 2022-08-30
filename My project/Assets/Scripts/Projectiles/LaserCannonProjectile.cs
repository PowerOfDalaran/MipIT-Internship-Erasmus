using UnityEngine;

public class LaserCannonProjectile : Projectile
{
    protected override void Awake()
    {
        base.Awake();

        projectileSpeed = 10;
        maxDistance = 50;
    }


    protected override void DestroyProjectile()
    {
        //bool doYouHaveBitches = NoBitches("yes, please?");
        //Debug.Log("Bitches? " + doYouHaveBitches);

        base.DestroyProjectile();
    }
}
