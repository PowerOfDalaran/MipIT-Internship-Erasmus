using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    public float speed = 4f;

    bool didntGotHit = true;

    [SerializeField] GameObject smallerAsteroidPrefab;

    Rigidbody2D rigidBody2D;
    SpriteRenderer spriteRenderer;
    PolygonCollider2D polygonCollider2D;

    void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        polygonCollider2D = GetComponent<PolygonCollider2D>();
    }

    //Shove asteroid at random direction
    public void ShoveAtRandom(float theMass, Vector2 direction)
    {
        //Choosing path to shove the asteroid
        List<Vector2> path = new List<Vector2>();
        spriteRenderer.sprite.GetPhysicsShape(0, path);
        polygonCollider2D.SetPath(0, path.ToArray());

        //Assigning values to variables
        rigidBody2D.mass = theMass;
        rigidBody2D.velocity = direction.normalized * speed;

        //Moving the asteroid
        rigidBody2D.AddTorque(Random.Range(-4f, 4f));
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Checking if asteroid didn't got hit by projectile twice
        if(other.tag == "Projectile" && didntGotHit)
        {
            //Turning off the flag
            didntGotHit = false;

            //Increasing number of points
            AsteroidOnlyGM.pointsCounter++;

            //Adding point to the counter (CODE IS KIND OF REPEATING, NEED TO CHANGE IT LATER)
            ScoreVisualizationManager.instance.AddPoint();

            //Splitting the asteroid twice or destroying it basing on its mass (RECODE THIS PART - SplitAsteroid() NEEDS TO SPAWN 2 ASTEROIDS)
            if (rigidBody2D.mass > 1)
            {
                SplitAsteroid();
                SplitAsteroid();

                //Adding ONE point to counter of existing asteroids (because one asteroid gets destroyed, two got created)
                AsteroidOnlyGM.existingAsteroids++;
            }
            else
            {
                //Removing one asteroid from counter of existing asteroids
                AsteroidOnlyGM.existingAsteroids--;

                Destroy(gameObject);
            }
        }
    }

    //Splitting current asteroid into smaller one
    void SplitAsteroid()
    {
        //Calculating position and instantiating new asteroid
        Vector2 position = this.transform.position;
        position += Random.insideUnitCircle * 0.5f;

        AsteroidController small = Instantiate(smallerAsteroidPrefab, position, this.transform.rotation).GetComponent<AsteroidController>();

        //Assigning direction and changing mass
        Vector2 direction = Random.insideUnitCircle;
        float mass = rigidBody2D.mass - 1;
        
        //Shoving created asteroid and destroying current one
        small.ShoveAtRandom(mass, direction);
        Destroy(gameObject);
    }

    //ALBERT.EXE
    private string[] Crewmates(string[] crewmates)
    {
        string[] sus = new string[1];
        sus = crewmates;
        return sus;
    }
}
