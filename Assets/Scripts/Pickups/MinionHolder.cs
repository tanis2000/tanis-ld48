using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App
{
    public class MinionHolder : MonoBehaviour
    {
        public Minion minionPrefab;
        public Transform pickups;
        public Transform spawnSocket;
        private void OnEnable()
        {
            SpawnMinion();
        }

        private void SpawnMinion()
        {
            var minion = Instantiate(minionPrefab, spawnSocket.position, Quaternion.identity);
            minion.transform.parent = pickups;
        }
    }
}
