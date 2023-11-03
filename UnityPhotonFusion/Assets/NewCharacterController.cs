using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;



public class NewCharacterController : NetworkBehaviour
{

    public NetworkCharacterControllerPrototype _ncc;
    public NetworkRigidbody rigidbody;
    public NetworkRigidbody2D rigidbody2D;
    public NetworkTransform networkTransform;
    public NetworkRigidbodyObsolete2D Obsolete2D;
    public float speed = 0.05f;
    public float jump;
    public Animator anim;
 




    public float JumpPower;

    [Networked]
    public Vector3 MovementDirection { get; set; }
    public bool TransformLocal = false;



    


    bool ShowSpeed => this && !TryGetComponent<NetworkCharacterControllerPrototype>(out _);
    [DrawIf(nameof(ShowSpeed), Hide = true)]
    public float Speed = 6f;



    public override void Spawned()
    {
        CacheComponents();
    }

    private void CacheComponents() //GetComponent gibi
    {
        if (!_ncc) _ncc = GetComponent<NetworkCharacterControllerPrototype>();
        if (!rigidbody) rigidbody = GetComponent<NetworkRigidbody>();
        if (!rigidbody2D) rigidbody2D = GetComponent<NetworkRigidbody2D>();
        if (!networkTransform) networkTransform = GetComponent<NetworkTransform>();
    }


    /*
    public Vector3 velocity;
    //public float speed;
    public float jumpForce = 5.0f;
    public float gravity = 9.8f;

    private float verticalVelocity = 0.0f;
    
    */







    public override void FixedUpdateNetwork()
    {
        
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
