using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace App
{
    public class Player : MonoBehaviour
    {
        public CircleCollider2D coll;
        public SpeedLimit limits;
        public Controls controls;
        public Rigidbody2D body;
        public GameObject overhead;
        public TMP_Text overheadText;
        public AudioSource audioSource;
        public List<AudioClip> fallClips = new List<AudioClip>();
        public List<AudioClip> pickupClips = new List<AudioClip>();
        public Health health;
        public float maxVVelocity = 100f;
        public int minionsCollected = 0;
        private Vector3 initialScale;
        private bool firstCollision;

        private void OnEnable()
        {
            initialScale = transform.localScale;
            limits.maxVVelocity = 10f;
        }

        private void FixedUpdate()
        {
            // Update scale
            var scale = Vector3.one * minionsCollected * .1f;
            transform.localScale = initialScale + scale;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!firstCollision)
            {
                // Reset max velocity limit on first collision ever
                limits.maxVVelocity = maxVVelocity;
                // Re-enable controls
                controls.UnlockControls();
                firstCollision = true;
            }
            var minion = other.gameObject.GetComponent<Minion>();
            if (minion != null && minion.pickuppable)
            {
                minionsCollected++;
                var n = (int) (Random.value % pickupClips.Count);
                Debug.Log(n);
                SoundManager.instance.PlayAtPosition(pickupClips[n], other.transform.position);
                Destroy(minion.gameObject);
            }
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            var minion = other.gameObject.GetComponent<Minion>();
            if (minion != null && minion.pickuppable)
            {
                minionsCollected++;
                SoundManager.instance.PlayAtPosition(pickupClips[(int)(Random.value % pickupClips.Count)], other.transform.position);
                Destroy(minion.gameObject);
            }
        }

        public void Reset()
        {
            minionsCollected = 0;
            firstCollision = false;
            body.velocity = Vector2.zero;
            overhead.SetActive(false);
        }

        public void ShowOverhead(string text)
        {
            overheadText.text = text;
            overhead.SetActive(true);
        }

        public void HideOverhead()
        {
            overhead.SetActive(false);
        }

        public void PlayRandomFalling()
        {
            if (audioSource.isPlaying)
            {
                return;
            }
            audioSource.clip = fallClips[(int)(Random.value % fallClips.Count)];
            audioSource.Play();
        }
    }
}
