using UnityEngine;

public class CameraManager : MonoBehaviour
{

    [SerializeField] float minXPosition = 10;
    [SerializeField] float maxXPosition = 10;
    [SerializeField] float minYPosition = 10;
    [SerializeField] float maxYPosition = 10;

    bool canMoveX = true;
    bool canMoveY = true;

    GameObject player;
    Vector3 previousPlayerPosition;

    void Awake()
    {
        //Accessing the player and assigning values of variables
        player = GameObject.FindGameObjectWithTag("Player");
        if(player != null)
        {
            previousPlayerPosition = player.transform.position;
        }
    }

    void Update()
    {
        //Extra checking if player is still assigned and assigning him if he's not
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        //Moving the camera after the player
        if(previousPlayerPosition != player.transform.position)
        {
            //Checking if camera can move on x axis
            if(player.transform.position.x > minXPosition && player.transform.position.x < maxXPosition)
            {
                canMoveX = true;
            }
            else
            {
                canMoveX = false;
            }

            //Checking if camera can move on y axis
            if(player.transform.position.y > minYPosition && player.transform.position.y < maxYPosition)
            {
                canMoveY = true;
            }
            else
            {
                canMoveY = false;
            }

            //Moving camera after the player in specific direction if possible
            if(canMoveX && canMoveY)
            {
                gameObject.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
            }
            else if(canMoveX)
            {
                gameObject.transform.position = new Vector3(player.transform.position.x, gameObject.transform.position.y, -10);
            }
            else if(canMoveY)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, player.transform.position.y, -10);
            }
            
            //Changing the previous player position in order to detect player movement
            previousPlayerPosition = player.transform.position;
        }
    }
    
    //Teleporting the camera to given location
    public void TeleportCamera(float positionX, float positionY)
    {
        gameObject.transform.position = new Vector3(positionX, positionY, -10);
    }
}
