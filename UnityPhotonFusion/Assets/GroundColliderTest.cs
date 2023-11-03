using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class GroundColliderTest : NetworkBehaviour
{
    [SerializeField] private LayerMask character;
    [SerializeField] Rigidbody2D rigidbody;
    private List<LagCompensatedHit> _lagCompensatedHits = new List<LagCompensatedHit>();
    [SerializeField] private float _characterDamageRadius = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool hasHitCharacter()
    {
        _lagCompensatedHits.Clear();

        var count = Runner.LagCompensation.OverlapSphere(rigidbody.position, _characterDamageRadius, Object.InputAuthority, _lagCompensatedHits, character.value);


        if (count <= 0)
        {
            return false;
        }

        _lagCompensatedHits.SortDistance();

        

        return true;




    }
}
