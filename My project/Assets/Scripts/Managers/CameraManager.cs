using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    GameObject player;
    Vector3 currentPlayerPosition;

    [SerializeField]
    float minXPosition = 10;
    [SerializeField]
    float maxXPosition = 10;
    [SerializeField]
    float minYPosition = 10;
    [SerializeField]
    float maxYPosition = 10;

    bool canMoveX = true;
    bool canMoveY = true;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if(player != null)
        {
            currentPlayerPosition = player.transform.position;
        }
    }

    void Update()
    {
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        if(currentPlayerPosition != player.transform.position)
        {
            
            if(player.transform.position.x > minXPosition && player.transform.position.x < maxXPosition)
            {
                canMoveX = true;
            }
            else
            {
                canMoveX = false;
            }

            if(player.transform.position.y > minYPosition && player.transform.position.y < maxYPosition)
            {
                canMoveY = true;
            }
            else
            {
                canMoveY = false;
            }

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
            
            currentPlayerPosition = player.transform.position;
        }
    }
    
    public void TeleportCamera(float positionX, float positionY)
    {
        gameObject.transform.position = new Vector3(positionX, positionY, -10);
    }
}
