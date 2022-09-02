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
    [SerializeField] float playerVelocityMultiplicator = 0.2f;
    [SerializeField] float asteroidVelocityMultiplicator = 1.5f;
    [SerializeField] int positionZ = 0;
    
    [SerializeField] Vector2 cameraPosition;
    [SerializeField] CameraManager mainCamera;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(isAvailable)
        {
            //Teleporting the player
            if(collision.gameObject.CompareTag("Player"))
            {
                //Assigment of variables
                GameObject player = collision.gameObject;

                float positionX = Random.Range(minXPosition, maxXPosition);
                float positionY = Random.Range(minYPosition, maxYPosition);

                //Changing player position and lowering his velocity
                player.transform.position = new Vector3(positionX, positionY, positionZ);

                player.GetComponent<Rigidbody2D>().velocity = new Vector2(player.GetComponent<Rigidbody2D>().velocity.x * playerVelocityMultiplicator, player.GetComponent<Rigidbody2D>().velocity.y * playerVelocityMultiplicator);

                //Changing camera position to proper position basing on the rotation of portal
                if(isVertical)
                {
                    mainCamera.TeleportCamera(cameraPosition.x, positionY);
                }
                else
                {
                    mainCamera.TeleportCamera(positionX, cameraPosition.y);
                }

                //Changing rotation of player if portal is unstable
                if(isUnstable)
                {
                    int rotationTrigger = Random.Range(1, 10); 

                    if(rotationTrigger > 6)
                    {
                        player.transform.rotation = new Quaternion(0, 0, Random.rotation.z, Random.rotation.w);                       
                    }
                }
            }
            //Teleporting the projectile
            else if(collision.gameObject.CompareTag("Projectile") && forProjectiles)
            {
                //Assigment of the variables
                GameObject projectile = collision.gameObject;       
                bool objectExist = true;

                //Chance of destroying the projectile if portal's unstable 
                if(isUnstable)
                {
                    int destructionTrigger = Random.Range(1, 10); 

                    if(destructionTrigger > 6)
                    {
                        Destroy(projectile);
                        objectExist = false;
                    }
                }

                //Changing the position of projectile if object didn't get destroyed
                if(objectExist)
                {
                    float positionX = Random.Range(minXPosition, maxXPosition);
                    float positionY = Random.Range(minYPosition, maxYPosition);
                    
                    projectile.transform.position = new Vector3(positionX, positionY, positionZ);
                }               
            }
            //Teleporting the asteroid
            else if(collision.gameObject.CompareTag("Asteroid") && forAsteroids)
            {
                //Assigment of the variables
                GameObject asteroid = collision.gameObject;  

                float positionX = Random.Range(minXPosition, maxXPosition);
                float positionY = Random.Range(minYPosition, maxYPosition);
                    
                //Changing position of the asteroid and increasing its velocity    
                asteroid.transform.position = new Vector3(positionX, positionY, positionZ);

                asteroid.GetComponent<Rigidbody2D>().velocity = new Vector2(asteroid.GetComponent<Rigidbody2D>().velocity.x * asteroidVelocityMultiplicator, asteroid.GetComponent<Rigidbody2D>().velocity.y * asteroidVelocityMultiplicator);

                //Changing direction, in which asteroid is moving (YET TO BE DEVELOPED)
            /*  if(isUnstable)
                {
                    int directionChangeTrigger = Random.Range(1, 10); 

                    if(directionChangeTrigger > 6)
                    {
                        
                    }
                }       */
            }
        }
    }
}
