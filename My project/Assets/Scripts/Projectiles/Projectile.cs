using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    protected float projectileSpeed;
    float currentLifespan;
    public float damage;
    public int durability = 1;

    protected int maxLifespan;

    protected Rigidbody2D rigidBody2D;
    protected Vector3 startPosition;

    protected virtual void Awake()
    {
        //Assigment of variables
        rigidBody2D = gameObject.GetComponent<Rigidbody2D>();
        startPosition = gameObject.transform.position;

        currentLifespan = 0;
    }

    void Update()
    {
        //Increasing current lifespan of projectile and Checking if projectile crossed his maximum lifespan, and if he did - destroying the projectile
        currentLifespan += Time.deltaTime;

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

    void OnTriggerEnter2D(Collider2D other)
    {
        //Destroying projectile on contact if it touched anything other than player, teleport or other projectile
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
