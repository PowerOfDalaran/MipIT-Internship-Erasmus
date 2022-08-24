using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    //public float minX;
    //public float maxX;
    //public float maxY;
    //public float minY;
    private Rigidbody2D rb;

    float maxVelocity = 3;

    public float rotationSpeed = 3;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        float yAxis = Input.GetAxis("Vertical");
        float xAxis = Input.GetAxis("Horizontal");

        ThrustForward(yAxis);
        Rotate(transform, -xAxis * rotationSpeed);
    }

    private void ClampVelocity()
    {
        float x = Mathf.Clamp(rb.velocity.x, -maxVelocity, maxVelocity);
        float y = Mathf.Clamp(rb.velocity.y, -maxVelocity, maxVelocity);

        rb.velocity = new Vector2(x,y);
    }

    private void ThrustForward(float amount)
    {
        Vector2 force = transform.up * amount;
        rb.AddForce(force);
    }

    private void Rotate(Transform t, float amount)
    {
        t.Rotate(0,0,amount);
    }
    //Vector2 targetPosition;
    //public float speed;

    //// Start is called before the first frame update
    //void Start()
    //{
    //    targetPosition = GetRandomPosition();
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    if ((Vector2)transform.position != targetPosition)
    //    {
    //        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    //    }
    //    else
    //    {
    //        targetPosition = GetRandomPosition();
    //    }
    //}

    //Vector2 GetRandomPosition()
    //{
    //    float randomX = Random.Range(minX, maxX);
    //    float randomY = Random.Range(minY, maxY);
    //    return new Vector2(randomX, randomY);

    //}
}
