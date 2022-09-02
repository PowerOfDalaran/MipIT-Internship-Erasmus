public class LaserCannonProjectile : Projectile
{
    protected override void Awake()
    {
        base.Awake();

        //Setting the features of this specific projectile
        projectileSpeed = 10;
        maxLifespan = 10;
    }
}
