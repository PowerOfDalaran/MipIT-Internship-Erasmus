using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidBlocker : MonoBehaviour
{
    void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.CompareTag("Asteroid"))
        {
            int asteroidLayer = LayerMask.NameToLayer("Asteroid");
            collider.gameObject.layer = asteroidLayer;
            Debug.Log(collider.gameObject.layer);
        }
    }
}
