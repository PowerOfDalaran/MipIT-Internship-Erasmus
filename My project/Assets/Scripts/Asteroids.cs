using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroids : MonoBehaviour
{
    public Sprite[] sprites;
    Rigidbody2D rb;
    SpriteRenderer sr;
    PolygonCollider2D pc;
    float speed = 2f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        pc = GetComponent<PolygonCollider2D>();
    }

    public void kick(float theMass, Vector2 direction)
    {
        sr.sprite = this.sprites[Random.Range(0, this.sprites.Length)];

        List<Vector2> path = new List<Vector2>();
        sr.sprite.GetPhysicsShape(0, path);
        pc.SetPath(0, path.ToArray());

        rb.mass = theMass;
        float width = Random.Range(0.75f, 1.33f);
        float height = 1 / width;
        transform.localScale = new Vector2(width, height) * theMass;


        rb.velocity = direction.normalized * speed;
        rb.AddTorque(Random.Range(-4f, 4f));
    }

    void OnTriggerExit2D(Collider2D other)
    {
         if(other.tag == "Background")
            {
                Destroy(gameObject);
            }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Bullet")
        {
            if(rb.mass>0.7f)
            {
                split();
                split();
            }
            Destroy(gameObject);
        }
    }

    void split()
    {
        Vector2 position = this.transform.position;
        position += Random.insideUnitCircle * 0.5f;

        Asteroids small = Instantiate(this, position, this.transform.rotation);
        Vector2 direction = Random.insideUnitCircle;
        float mass = rb.mass / 2;
        small.kick(mass, direction);
    }
}
