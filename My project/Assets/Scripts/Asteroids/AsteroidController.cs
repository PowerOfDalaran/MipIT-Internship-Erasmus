using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    public float speed = 4f;

    [SerializeField] public Sprite[] possibleSprites;
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
        //Choosing sprite
        spriteRenderer.sprite = this.possibleSprites[Random.Range(0, this.possibleSprites.Length)];

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
        if(other.tag == "Projectile")
        {
            //Increasing number of points and lowering number of existing asteroid
            AsteroidOnlyGM.pointsCounter++;
            AsteroidOnlyGM.spawnedAsteroids--;

            //Adding point to the counter (CODE IS KIND OF REPEATING, NEED TO CHANGE IT LATER)
            ScoreVisualizationManager.instance.AddPoint();

            //Splitting the asteroid twice or destroying it basing on its mass (RECODE THIS PART - SplitAsteroid() NEEDS TO SPAWN 2 ASTEROIDS)
            if (rigidBody2D.mass > 1)
            {
                SplitAsteroid();
                SplitAsteroid();
            }
            else
            {
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

    //ALBER.EXE
    private string[] Crewmates(string[] crewmates)
    {
        string[] sus = new string[1];
        sus = crewmates;
        return sus;
    }
}
