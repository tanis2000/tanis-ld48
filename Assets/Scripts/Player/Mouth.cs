using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App
{
    public class Mouth : MonoBehaviour
    {
        public Transform mouth;
        public Rigidbody2D body;
        public Player player;
        
        public Vector3 closedSize;
        public Vector3 openedSize;
        public float speedForWow = 10f;
        public float wowDuration = .5f;

        private void Awake()
        {
            player = GameObject.Find("Player").GetComponent<Player>();
        }

        private void FixedUpdate()
        {
            if (Mathf.Abs(body.velocity.magnitude) >= speedForWow)
            {
                StartCoroutine(OpenMouth());
            }
        }

        private IEnumerator OpenMouth()
        {
            mouth.localScale = openedSize;
            player.PlayRandomFalling();
            yield return new WaitForSeconds(wowDuration);
            mouth.localScale = closedSize;
        }
    }
}
