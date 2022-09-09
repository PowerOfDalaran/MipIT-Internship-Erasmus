using System.Collections;
using System.Collections.Generic;
using RiptideNetworking;
using UnityEngine;

//[RequireComponent(typeof(CharacterController))]
public class CharacterControllerServer : MonoBehaviour
{
    [SerializeField] private PlayerServer player;
    [SerializeField] private CharacterController controller;
    [SerializeField] private Transform campProxy;


    [SerializeField] float speed = 1;
    [SerializeField] float rotationSpeed = 1;

    
    float forceAmount = -1.0f;
    float torqueDirection = 0.0f;
    float torqueAmount = 0.2f;

    bool fireWeapon = false;

    Weapon currentWeapon;
    Animator characterAnimator;
    Rigidbody2D rigidBody2D;
    GameObject flame;

    private bool[] inputs;

     void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        currentWeapon = gameObject.GetComponent<Weapon>();

        flame = GameObject.Find("flame");
        characterAnimator = gameObject.GetComponent<Animator>();

        //impact02 = GameObject.Find("Impact02");
        //impact02.SetActive(false);
    }

    private void Start()
    {
        inputs = new bool[4];
    }

    private void FixedUpdate()
    {
        if (inputs[0])
        {
            flame.SetActive(true);
            rigidBody2D.AddForce(-transform.up * forceAmount * speed);
        }
        else 
        {
            flame.SetActive(false);
        }

        if (inputs[1])
            torqueDirection = 0.2f;

        if (inputs[2])
            torqueDirection = -0.2f;

        if (inputs[3])
            fireWeapon = true;
        
        if (torqueDirection != 0)
        {
            rigidBody2D.AddTorque(torqueDirection * torqueAmount );
        }
        //Debug.Log("Dupsko Rasza");
        SendMovement();
    }

    public void SetInput(bool[] inputs)
    {
        Debug.Log("Dupsko Rasza1");
        this.inputs = inputs;
    }

    private void SendMovement()
    {
        //Debug.Log("Dupsko Rasza2");
        Message message = Message.Create(MessageSendMode.unreliable, ServerToClientId.playerMovement);
        message.AddUShort(player.Id);
        message.AddVector2(rigidBody2D.transform.position);
        NetworkManagerServer.Singleton.Server.SendToAll(message);
    }
}
