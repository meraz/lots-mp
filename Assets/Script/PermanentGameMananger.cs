using System.Collections.Generic;
using BossRoom.Scripts.Shared.Net.NetworkObjectPool;
using UnityEngine;
using UnityEngine.Assertions;

namespace Lots
{
    [RequireComponent(typeof(NetworkObjectPool))]
    [RequireComponent(typeof(NetworkManagerWrapper))]
    public class PermanentGameMananger : MonoBehaviour
    {
        private static PermanentGameMananger _instance;
        public static PermanentGameMananger Singleton { get { return _instance; } }

        [SerializeField]
        private List<IGameManager> GameManagers; // TODO meraz this didn't work as I hoped, consider removing

        public void Awake()
        {
            Assert.IsTrue(_instance == null);
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}