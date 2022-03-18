using Unity.Netcode;
using UnityEngine;

public class PlayerSpawnProjectile : NetworkBehaviour
{
    public GameObject projectilePrefab; // TODO meraz move out

    static float DefaultCooldown = 1; // TODO meraz use some recipie/blueprint for projectiles instead

    float currentTimer = 0;

    public override void OnNetworkSpawn()
    {
        enabled = IsOwner;
    }

    void FixedUpdate()
    {
        if(isActionAvailable())
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                spawnProjectileRequestServerRpc();
                setCooldown();
            }
        }
        else
        {
            reduceCooldown();
        }
    }

    bool isActionAvailable()
    {
        return currentTimer <= 0.0f;
    }

    [ServerRpc]
    void spawnProjectileRequestServerRpc()
    {
        GameObject projectile =  Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        ProjectileMovementServer projectileMovementServer = projectile.GetComponent<ProjectileMovementServer>();
        projectileMovementServer.veloctity = new Vector2(1,1);
        projectile.GetComponent<NetworkObject>().Spawn();
    }

    void reduceCooldown()
    {
        currentTimer -= Time.deltaTime;
    }

    void setCooldown()
    {
        currentTimer = computeCooldown();
    }

    float computeCooldown()
    {
        return DefaultCooldown;
    }
}
