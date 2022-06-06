using UnityEngine;
using UnityEngine.Assertions;

public class PlayerSpawnProjectile : MonoBehaviour
{
    // TODO meraz this make been made more generic if the action is registered as a callback/lambda
    static float DefaultCooldown = 1; // TODO meraz use some recipie/blueprint for projectiles instead

    float currentTimer = 0;
    PrefabManager prefabManager;

    void Awake()
    {
        prefabManager = FindObjectOfType<PrefabManager>();
        Assert.IsNotNull(prefabManager, "PrefabManager cannot be null");
    }

    void FixedUpdate()
    {
        if (isActionAvailable())
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                spawnProjectile();
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

    void spawnProjectile()
    {
        Debug.Log("spawnProjectile");
        // Even if this worked, it doesn't, then one would like to get the gameobject back so it's possible to track it, but is it required?
        // It's not possible to send a GameObject over an RPC call.... so how should I design it?
        prefabManager.SpawnPrefabServerRpc("Projectile", gameObject.transform.position);
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
