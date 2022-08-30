using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidBooster : MonoBehaviour
{
    [SerializeField] Vector2 direction;
    void OnTriggerStay2D(Collider2D collider)
    {
        if(collider.CompareTag("Asteroid"))
        {
            Debug.Log(collider.tag);
            collider.gameObject.GetComponent<Rigidbody2D>().AddForce(direction);
        }
    }
}
