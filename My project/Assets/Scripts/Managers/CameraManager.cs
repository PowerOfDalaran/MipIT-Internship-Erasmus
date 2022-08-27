using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    GameObject player;
    Vector3 currentPlayerPosition;

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
            currentPlayerPosition = player.transform.position;
            gameObject.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
        }
    }
}
