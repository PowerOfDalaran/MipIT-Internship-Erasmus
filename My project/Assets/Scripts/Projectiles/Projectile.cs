using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    protected Rigidbody2D rigidBody2D;
    protected Vector3 startPosition;
    float currentDistance;
    protected float projectileSpeed;
    protected float maxDistance;

    protected virtual void Awake()
    {
        rigidBody2D = gameObject.GetComponent<Rigidbody2D>();
        startPosition = gameObject.transform.position;
    }

    void Update()
    {
        //Checking if projectile crossed his maxDistance limitation
        currentDistance = Vector3.Distance (startPosition, gameObject.transform.position);

        if(currentDistance >= maxDistance)
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
        if(other.gameObject.name != "player" && other.gameObject.transform.tag != "Projectile")
        {
            DestroyProjectile();
        }
    }

    //Destroying projectile
    protected virtual void DestroyProjectile()
    {
        Destroy(gameObject);
    }

    protected bool NoBitches(string bitches)
    {
        bitches = "No bitches? O.o";

        return false;
    }
}
