using UnityEngine;

    /// ARCHIVED ///

public class Bullet : MonoBehaviour
{
    Rigidbody2D rb;
    float speed = 15f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    //  USED
    public void shoot(Vector2 direciton)
    {
        rb.velocity = direciton.normalized * speed;
    }

    //  NOT USED
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Background")
        {
            Destroy(gameObject);
        }
    }

    //  NOT USED
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Asteroid")
        {
            Destroy(gameObject);
        }
    }
}
