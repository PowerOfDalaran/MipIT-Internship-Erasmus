public class LaserCannonProjectile : Projectile
{
    protected override void Awake()
    {
        base.Awake();

        //Setting the features of this specific projectile
        damage = 1;
        projectileSpeed = 10;
        maxLifespan = 10;
    }
}
