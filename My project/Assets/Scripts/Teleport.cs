using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] bool isAvailable = true;
    [SerializeField] bool isUnstable = false;
    [SerializeField] bool forProjectiles = false;
    [SerializeField] bool forAsteroids = false;
    [SerializeField] bool isVertical = false;

    [SerializeField] float maxYPosition;
    [SerializeField] float minYPosition;
    [SerializeField] float maxXPosition;
    [SerializeField] float minXPosition;
    [SerializeField] Vector2 cameraPosition;
    [SerializeField] float playerVelocityMultiplicator = 0.2f;
    [SerializeField] float asteroidVelocityMultiplicator = 1.5f;
    [SerializeField] int positionZ = 0;

    CameraManager mainCamera;

    void Awake()
    {
        mainCamera = Camera.main.gameObject.GetComponent<CameraManager>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(isAvailable)
        {
            if(collision.gameObject.CompareTag("Player"))
            {
                //Teleporting the player in random position from given range, lowering his velocity, teleporting camera and possibly changing its rotation if unstable
                GameObject player = collision.gameObject;

                float positionX = Random.Range(minXPosition, maxXPosition);
                float positionY = Random.Range(minYPosition, maxYPosition);

                player.transform.position = new Vector3(positionX, positionY, positionZ);

                player.GetComponent<Rigidbody2D>().velocity = new Vector2(player.GetComponent<Rigidbody2D>().velocity.x * playerVelocityMultiplicator, player.GetComponent<Rigidbody2D>().velocity.y * playerVelocityMultiplicator);

                if(isVertical)
                {
                    mainCamera.TeleportCamera(cameraPosition.x, positionY);
                }
                else
                {
                    mainCamera.TeleportCamera(positionX, cameraPosition.y);
                }

                if(isUnstable)
                {
                    int rotationTrigger = Random.Range(1, 10); 

                    if(rotationTrigger > 6)
                    {
                        player.transform.rotation = new Quaternion(0, 0, Random.rotation.z, Random.rotation.w);                       
                    }
                }
            }
            else if(collision.gameObject.CompareTag("Projectile") && forProjectiles)
            {
                //Teleporting the projectile with some chance to destroy it if unstable
                GameObject projectile = collision.gameObject;       
                bool objectExist = true;

                if(isUnstable)
                {
                    int destructionTrigger = Random.Range(1, 10); 

                    if(destructionTrigger > 6)
                    {
                        Destroy(projectile);
                        objectExist = false;
                    }
                }

                if(objectExist)
                {
                    float positionX = Random.Range(minXPosition, maxXPosition);
                    float positionY = Random.Range(minYPosition, maxYPosition);
                    
                    projectile.transform.position = new Vector3(positionX, positionY, positionZ);
                }               
            }
            else if(collision.gameObject.CompareTag("Asteroid") && forAsteroids)
            {
                //Teleporting asteroid, increasing its velocity and  (NOT DEVELOPED YET) changing its direction if unstable
                GameObject asteroid = collision.gameObject;  

                float positionX = Random.Range(minXPosition, maxXPosition);
                float positionY = Random.Range(minYPosition, maxYPosition);
                    
                asteroid.transform.position = new Vector3(positionX, positionY, positionZ);

                asteroid.GetComponent<Rigidbody2D>().velocity = new Vector2(asteroid.GetComponent<Rigidbody2D>().velocity.x * asteroidVelocityMultiplicator, asteroid.GetComponent<Rigidbody2D>().velocity.y * asteroidVelocityMultiplicator);

                /*
                if(isUnstable)
                {
                    int directionChangeTrigger = Random.Range(1, 10); 

                    if(directionChangeTrigger > 6)
                    {
                        
                    }
                }
                */
            }
        }
    }
}
