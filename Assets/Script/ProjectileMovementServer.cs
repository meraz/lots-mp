using Unity.Netcode;
using UnityEngine;
using Unity.Netcode.Components;

[RequireComponent(typeof(NetworkTransform))]
public class ProjectileMovementServer : NetworkBehaviour
{
    public Vector2 veloctity = Vector2.zero;

    public override void OnNetworkSpawn()
    {
        enabled = IsOwner;
    }

    void Update()
    {
        transform.position += new Vector3(veloctity.x, veloctity.y, 0) * Time.deltaTime;
    }
}
