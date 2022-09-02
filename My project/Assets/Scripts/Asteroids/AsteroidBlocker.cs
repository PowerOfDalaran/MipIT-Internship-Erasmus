using UnityEngine;

public class AsteroidBlocker : MonoBehaviour
{
    void OnTriggerExit2D(Collider2D collider)
    {
        //Changing the layer of asteroid to "Asteroid" from "NewAsteroid", so now it can't fly through blockad and is locked on arena
        if(collider.CompareTag("Asteroid"))
        {
            int asteroidLayer = LayerMask.NameToLayer("Asteroid");
            collider.gameObject.layer = asteroidLayer;
        }
    }
}
