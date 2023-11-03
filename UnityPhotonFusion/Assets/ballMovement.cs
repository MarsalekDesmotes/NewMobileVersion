using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;


public class ballMovement : Fusion.NetworkBehaviour
{
    Rigidbody2D rigidbody;
    public float shotPower;
    protected NetworkCharacterControllerPrototype _ncc;



    private void CacheComponents()
    {
        GetInput(out NetworkInputPrototype input);
        if (!_ncc) _ncc = GetComponent<NetworkCharacterControllerPrototype>();
        
    }

            
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    

    public override void FixedUpdateNetwork()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shot();
            Debug.Log("SHOT");
        }
    }

    public void Shot()
    {
        rigidbody.velocity = new Vector2(0.5f*shotPower, 10f);
    }
}
