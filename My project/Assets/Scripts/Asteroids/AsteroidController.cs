using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    [SerializeField]
    public Sprite[] possibleSprites;
    [SerializeField]
    GameObject smallerAsteroidPrefab;

    Rigidbody2D rigidBody2D;
    SpriteRenderer spriteRenderer;
    PolygonCollider2D polygonCollider2D;
    
    public float speed = 4f;

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

    void OnTriggerExit2D(Collider2D other)
    {
        //Destroying asteroid if it moves out of border of the game
         if(other.tag == "Background")
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Splitting or destroying asteroid when it hit with projectile
        if(other.tag == "Projectile")
        {
            AsteroidOnlyGM.pointsCounter++;
            AsteroidOnlyGM.spawnedAsteroids--;

            ScoreVisualizationManager.instance.AddPoint();

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

    //Splitting current asteroid into 2 smaller ones
    void SplitAsteroid()
    {
        //Calculating position and creating new asteroid
        Vector2 position = this.transform.position;
        position += Random.insideUnitCircle * 0.5f;

        AsteroidController small = Instantiate(smallerAsteroidPrefab, position, this.transform.rotation).GetComponent<AsteroidController>();

        //Assignig direction and changing mass
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
