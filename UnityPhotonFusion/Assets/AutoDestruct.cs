using UnityEngine;
using Fusion;

public class AutoDestruct : NetworkBehaviour
{
    private ParticleSystem ps;
    private NetworkObject network;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        network = GetComponent<NetworkObject>();
    }

    void Update()
    {
        if (ps)
        {
            if (!ps.IsAlive())
            {
                Runner.Despawn(network);
            }
        }
    }
}
