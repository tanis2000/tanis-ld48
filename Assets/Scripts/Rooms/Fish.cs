using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace App
{
    public class Fish : MonoBehaviour
    {
        public float direction = 1f;
        public float minAcceleration = .7f;
        public float maxAcceleration = 1.2f;
        public float minScale = .3f;
        public float maxScale = 1.2f;

        private float acceleration;
        private float scale;
        private void Start()
        {
            acceleration = Random.Range(minAcceleration, maxAcceleration);
            scale = Random.Range(minScale, maxScale);
            transform.localScale = new Vector3(scale * direction, scale, 1);
        }

        private void Update()
        {
            transform.position += Vector3.right * direction * acceleration * Time.deltaTime;
        }
    }
}
