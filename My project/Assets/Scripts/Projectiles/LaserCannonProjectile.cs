using UnityEngine;

public class LaserCannonProjectile : Projectile
{
    protected override void Awake()
    {
        base.Awake();

        projectileSpeed = 10;
        maxLifespan = 10;
    }


    protected override void DestroyProjectile()
    {
        //bool doYouHaveBitches = NoBitches("yes, please?");
        //Debug.Log("Bitches? " + doYouHaveBitches);

        base.DestroyProjectile();
    }
}
