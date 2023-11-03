using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;


[RequireComponent(typeof(CharacterController))]
[OrderBefore(typeof(NetworkTransform))]
[DisallowMultipleComponent]
public class CharacterMovement : NetworkBehaviour
{
    private CharacterController _controller;

    public float PlayerSpeed = 2f;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    public override void FixedUpdateNetwork()
    {


        /*// Only move own player and not every other player. Each player controls its own player object.
        if (HasStateAuthority == false)
        {
            return;
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * Runner.DeltaTime * PlayerSpeed;


        _controller.Move(move);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }
        */
        if (HasStateAuthority == true) //Burada sadece host olanı tespit et 
        {
            if (Input.GetKey(KeyCode.D))
            {
                transform.position += new Vector3(0.5f, 0, 0);
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.position += new Vector3(-0.5f, 0, 0);
            }
        }

        else
        {
            //Host olduğum için diğer oyuncuları hareket ettiremem
        }
        
        
        

    }

}
