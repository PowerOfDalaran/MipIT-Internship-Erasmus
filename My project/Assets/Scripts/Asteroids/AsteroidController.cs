using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    public float speed = 4f;
    float maxHealth = 1000;
    [SerializeField] float currentHealth = 1000;
    bool didntGotHit = true;

    [SerializeField] Sprite[] smallSprites;
    [SerializeField] Sprite[] mediumSprites;
    [SerializeField] Sprite[] bigSprites;
    [SerializeField] Sprite[] hugeSprites;

    Rigidbody2D rigidBody2D;
    SpriteRenderer spriteRenderer;
    PolygonCollider2D polygonCollider2D;

    void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        //Checking if asteroid should be destroyed
        if(currentHealth <= 0)
        {
            //Increasing number of points
            AsteroidOnlyGM.pointsCounter++;

            //Adding point to the counter (CODE IS KIND OF REPEATING, NEED TO CHANGE IT LATER)
            ScoreVisualizationManager.instance.AddPoint();

            //Splitting the asteroid twice or destroying it basing on its mass
            if (rigidBody2D.mass > 1)
            {
                SplitAsteroid();

                //Adding ONE point to counter of existing asteroids (because one asteroid gets destroyed, two got created)
                AsteroidOnlyGM.existingAsteroids++;
            }
            else
            {
                //Removing one asteroid from counter of existing asteroids and destryoing game object
                AsteroidOnlyGM.existingAsteroids--;

                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Checking if asteroid didn't got hit by projectile twice
        if(other.tag == "Projectile" && other.GetComponent<Projectile>().durability > 0)
        {
            //Dealing damage to asteroid and removing one durability point from projectile
            other.GetComponent<Projectile>().durability--;
            DealDamage(other.GetComponent<Projectile>().damage);
        }
    }

    //Dealing damage to asteroid
    void DealDamage(float damage)
    {
        currentHealth -= damage;
    }

    //Randomizing sprite of asteroid and adding poylgon collider to it
    public void InitializateAsteroid(int asteroidSize)
    {
        int randomSprite;

        Sprite randomizedSprite = null;

        //Destroying polygon collider if it already exist
        if(gameObject.GetComponent<PolygonCollider2D>() != null)
        {
            Destroy(gameObject.GetComponent<PolygonCollider2D>());
        }

        //Choosing random sprite for asteroid, from the pool of sprites basing on its mass and max health
        switch(asteroidSize)
        {
            case 4:
                randomSprite = Random.Range(0, hugeSprites.Length - 1);
                randomizedSprite = hugeSprites[randomSprite];
                maxHealth = 8;
                break;
            case 3:
                randomSprite = Random.Range(0, bigSprites.Length - 1);
                randomizedSprite = bigSprites[randomSprite];
                maxHealth = 4;
                break;
            case 2:
                randomSprite = Random.Range(0, mediumSprites.Length - 1);
                randomizedSprite = mediumSprites[randomSprite];
                maxHealth = 2;
                break;
            case 1:
                randomSprite = Random.Range(0, smallSprites.Length - 1);
                randomizedSprite = smallSprites[randomSprite];
                maxHealth = 1;
                break;
            default:
                Debug.Log("An error has occured.");
                break;
        }

        //Assigning mass to rigidbody, setting up current health and sprite for sprite renderer and adding polygon collider so the collider will adjust to chosen sprite
        rigidBody2D.mass = asteroidSize;
        currentHealth = maxHealth;
        spriteRenderer.sprite = randomizedSprite;
        polygonCollider2D = gameObject.AddComponent<PolygonCollider2D>();
    }

    //Shove asteroid at chosen direction
    public void ShoveAtRandom(Vector2 direction)
    {
        //Choosing path for asteroids and assigning it for sprite renderer and polygon collider
        List<Vector2> path = new List<Vector2>();
        spriteRenderer.sprite.GetPhysicsShape(0, path);
        polygonCollider2D.SetPath(0, path.ToArray());

        //Assigning velocity and adding torque to rigidbody
        rigidBody2D.velocity = direction.normalized * speed;
        rigidBody2D.AddTorque(Random.Range(-4f, 4f));
    }

    //Splitting current asteroid into smaller one
    void SplitAsteroid()
    {
        //Choosing position for new asteroid
        Vector2 position = this.transform.position;
        position += Random.insideUnitCircle * 0.5f;

        //Spawning 2 new asteroid, initializating them and shoving them at chosen direction
        for(int i = 0; i < 2; i++)
        {
            AsteroidController spawnedAsteroid = Instantiate(gameObject, position, this.transform.rotation).GetComponent<AsteroidController>();

            Vector2 direction = Random.insideUnitCircle;
            int mass = (int)rigidBody2D.mass - 1;
        
            spawnedAsteroid.InitializateAsteroid(mass);
            spawnedAsteroid.ShoveAtRandom(direction);
        }
        
        Destroy(gameObject);
    }
}
