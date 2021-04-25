using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace App
{
    public class FishSpawner : MonoBehaviour
    {
        public Fish prefab;
        public Transform fishHolder;
        public int minAmount = 3;
        public int maxAmount = 15;
        public float minVDist = 0.5f;
        public float maxVDist = 3f;
        public float minHDist = 0.75f;
        public float maxHDist = 2f;
        public float direction = 1f;
        
        private void OnEnable()
        {
            var num = Random.Range(minAmount, maxAmount);
            for (var i = 0; i < num; i++)
            {
                var pos = transform.position + new Vector3(Random.Range(minHDist, maxHDist), Random.Range(minVDist, maxVDist), 0);
                var fish = Instantiate(prefab, pos, Quaternion.identity);
                fish.transform.parent = fishHolder;
                fish.direction = direction;
            }
        }
    }
}
