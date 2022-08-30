using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    protected Rigidbody2D rigidBody2D;
    protected Vector3 startPosition;
    float currentLifespan;
    protected float projectileSpeed;
    protected int maxLifespan;

    protected virtual void Awake()
    {
        rigidBody2D = gameObject.GetComponent<Rigidbody2D>();
        startPosition = gameObject.transform.position;
        currentLifespan = 0;
    }

    void Update()
    {
        currentLifespan += Time.deltaTime;

        //Checking if projectile crossed his maxDistance limitation
        if(currentLifespan >= maxLifespan)
        {
            DestroyProjectile();
        }
    }

    //Launching the projectile in chosen direction
    public virtual void LaunchProjectile(Vector2 direciton)
    {
        rigidBody2D.velocity = direciton.normalized * projectileSpeed;
    }

    //Destroying projectile on contact
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag != "Player" && other.gameObject.tag != "Projectile" && other.gameObject.tag != "Teleport")
        {
            DestroyProjectile();
        }
    }

    //Destroying projectile
    protected virtual void DestroyProjectile()
    {
        Destroy(gameObject);
    }

    //ALBERT.EXE
    protected bool NoBitches(string bitches)
    {
        bitches = "No bitches? O.o";

        return false;
    }
}
