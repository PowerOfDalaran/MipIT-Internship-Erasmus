using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroids : MonoBehaviour
{
    public Sprite[] sprites;
    Rigidbody2D rigidBody2D;
    SpriteRenderer spriteRenderer;
    PolygonCollider2D polygonCollider2D;
    float speed = 2f;

    void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        polygonCollider2D = GetComponent<PolygonCollider2D>();
    }

    //Shove asteroid at random direction
    public void ShoveAtRandom(float theMass, Vector2 direction)
    {
        spriteRenderer.sprite = this.sprites[Random.Range(0, this.sprites.Length)];

        List<Vector2> path = new List<Vector2>();
        spriteRenderer.sprite.GetPhysicsShape(0, path);
        polygonCollider2D.SetPath(0, path.ToArray());

        rigidBody2D.mass = theMass;
        float width = Random.Range(0.75f, 1.33f);
        float height = 1 / width;
        transform.localScale = new Vector2(width, height) * theMass;


        rigidBody2D.velocity = direction.normalized * speed;
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
            if(rigidBody2D.mass>0.7f)
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

    //Splits asteroid into smaller ones and start their movement
    void SplitAsteroid()
    {
        Vector2 position = this.transform.position;
        position += Random.insideUnitCircle * 0.5f;

        Asteroids small = Instantiate(this, position, this.transform.rotation);
        Vector2 direction = Random.insideUnitCircle;
        float mass = rigidBody2D.mass / 2;
        small.ShoveAtRandom(mass, direction);
    }
}
