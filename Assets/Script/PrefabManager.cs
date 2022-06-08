using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

// TODO meraz this could perhaps be registered as a prefabhandler
public class PrefabManager : NetworkBehaviour // TODO meraz this only needed to be NetworkBehaviour for the spawnPrefab
{
    // TODO meraz use some singleton pattern with game-singleton
    private Dictionary<string, GameObject> prefabs_found = new Dictionary<string, GameObject>();

    public List<GameObject> prefabs = new List<GameObject>();

    public GameObject GetPrefab(string name) // TODO meraz find some better approach that does not require each prefab to be uniquely names in the list
    {
        Debug.Log($"Attempting to find Prefab with name={name}");
        if(prefabs_found.ContainsKey(name))
        {
            return prefabs_found[name];
        }

        foreach(var prefab in prefabs)
        {
            if(prefab.name == name)
            {
                prefabs_found.Add(name, prefab);
                return prefab;
            }
        }
        throw new System.Exception($"Attempting to find Prefab with name={name} failed");
    }

    [ServerRpc]
    public void SpawnPrefabServerRpc(string name, Vector3 position)
    {
        var prefab = GetPrefab(name);
        GameObject gameObject = Instantiate(prefab, position, Quaternion.identity);
        gameObject.GetComponent<NetworkObject>().Spawn();
    }
}
