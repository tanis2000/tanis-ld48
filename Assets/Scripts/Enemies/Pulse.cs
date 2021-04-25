using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App
{
    public class Pulse : MonoBehaviour
    {
        public Vector3 minScale = Vector3.one;
        public Vector3 maxScale = Vector3.one * 2f;
        public float waitTime = 2f;
        public float speed = .5f;

        private float cooldown;

        private void Start()
        {
            cooldown = waitTime;
        }

        private void Update()
        {
            cooldown -= Time.deltaTime;
            if (cooldown <= 0)
            {
                StartCoroutine(Scale());
                cooldown = waitTime;
            }
        }

        private IEnumerator Scale()
        {
            var currentTime = 0f;
            do
            {
                transform.localScale = Vector3.Lerp(minScale, maxScale, currentTime / speed);
                currentTime += Time.deltaTime;
                yield return null;
            } while (currentTime <= speed);
        }
    }
}
