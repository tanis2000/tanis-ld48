using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App
{
    public class EnemySpawner : MonoBehaviour
    {
        public GameObject enemyPrefab;
        public Transform enemies;
        public Transform spawnSocket;
        private void OnEnable()
        {
            Spawn();
        }

        private void Spawn()
        {
            var enemy = Instantiate(enemyPrefab, spawnSocket.position, Quaternion.identity);
            enemy.transform.parent = enemies;
        }
    }
}
