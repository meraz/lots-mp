using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using BossRoom.Scripts.Shared.Net.NetworkObjectPool;
using Unity.Netcode;
using System.IO;

public class BuildPrefabManager : EditorWindow
{
    [MenuItem("Custom/BuildPrefabManager")]
    static void Init()
    {
        // TODO fetch current manager without overwriting it
        GameObject prefabManager = new GameObject();
        prefabManager.name = "PrefabManager";
        NetworkObjectPool networkObjectPool = prefabManager.AddComponent<NetworkObjectPool>();

        foreach (string prefabPath in GetAllPrefabNetworkPrefabPaths())
        {
            if (prefabPath.Contains("PrefabManager"))
            {
                continue;
            }
            Debug.Log(prefabPath);

            GameObject prefab = Resources.Load<GameObject>(prefabPath);
            if (prefab.GetComponent<NetworkObject>())
            {
                networkObjectPool.RegisterPrefabEditor(prefab);
            }
        }
        bool success = false;
        PrefabUtility.SaveAsPrefabAsset(prefabManager, "Assets/Prefabs/PrefabManager.prefab", out success);
        if(!success)
        {
            Debug.LogError("Failed to save prefabManager.");
        }
        DestroyImmediate(prefabManager);
    }

    public static string[] GetAllPrefabNetworkPrefabPaths()
    {
        string[] allAssetPaths = AssetDatabase.GetAllAssetPaths();
        List<string> result = new List<string>();

        foreach (string assetPath in allAssetPaths)
        {
            if (assetPath.Contains(".prefab") && assetPath.Contains("Assets/Resources/NetworkPrefabs"))
            {
                string rightSide = assetPath.Split("Assets/Resources/")[1];
                result.Add(Path.ChangeExtension(rightSide, null));
            }
        }
        return result.ToArray();
    }



}
