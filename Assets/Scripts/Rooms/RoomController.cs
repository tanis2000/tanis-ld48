using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace App
{
    public class RoomController : MonoBehaviour
    {
        public Controls controls;
        public AudioClip music;
        public Player player;
        public Transform playerSpawn;
        public Exit exit;
        public Transform pickupsHolder;

        public float acceleration = 10f;
        public float maxAngle = 45f;
        
        private Transform _room;
        private float angle = 0f;
        private Bounds bounds;
        private Vector3 initialVector;
        private Vector3 initialPosition;
        private Quaternion initialRotation;
        public int totalMinions;

        private void Awake()
        {
            _room = transform;
            initialPosition = _room.localPosition;
            initialRotation = _room.localRotation;
            var colliders = transform.GetComponentsInChildren<Collider2D>();
            bounds = new Bounds(transform.position, Vector3.zero);
            foreach (var c in colliders)
            {
                bounds.Encapsulate(c.bounds);
            }

            initialVector = transform.position - bounds.center;
            initialVector.z = 0;
        }

        private void OnEnable()
        {
            CountMinions();
        }

        void FixedUpdate()
        {
            if (controls.locked)
            {
                return;
            }
            
            var dir = 0;
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                dir = 1;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                dir = -1;
            }

            angle = dir * acceleration * Time.fixedDeltaTime;
            var currentVector = transform.position - bounds.center;
            currentVector.z = 0;
            var angleBetween = Vector3.Angle(initialVector, currentVector) * (Vector3.Cross(initialVector, currentVector).z > 0 ? 1 : -1);            
            float newAngle = Mathf.Clamp(angleBetween + angle, -maxAngle, maxAngle);
            angle = newAngle - angleBetween;
            _room.RotateAround(bounds.center, Vector3.forward, angle);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(bounds.center, 1f);
            Gizmos.DrawWireCube(bounds.center, bounds.size);
        }

        private void CountMinions()
        {
            var minions = transform.GetComponentsInChildren<MinionHolder>();
            totalMinions = minions.Length;
        }

        public void ResetOrientation()
        {
            _room.localPosition = initialPosition;
            _room.localRotation = initialRotation;
        }

        public IEnumerator Activate()
        {
            Debug.Log("Activating level");
            MusicManager.instance.CrossFade(music, true);
            ResetOrientation();
            yield return null;
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }

        public IEnumerator PlayRoom()
        {
            player.body.bodyType = RigidbodyType2D.Kinematic;
            for (int j = 0; j < 10; j++)
            {
                yield return new WaitForFixedUpdate();
                player.body.velocity = Vector2.zero;
                player.body.Sleep();
                player.body.position = playerSpawn.position;
            }
            for (int j = 0; j < 10; j++)
            {
                yield return new WaitForFixedUpdate();
                player.body.velocity = Vector2.zero;
                player.body.position = playerSpawn.position;
                player.body.bodyType = RigidbodyType2D.Dynamic;
                player.body.WakeUp();
            }
            yield return new WaitForFixedUpdate();
            while (player.health.amount > 0 && !exit.trigger.Triggered)
            {
                yield return new WaitForFixedUpdate();
            }
            controls.LockControls();
            ResetOrientation();
            player.ShowOverhead($"{player.minionsCollected} / {totalMinions} SAVED!"); 
            yield return new WaitForSeconds(5.0f);
            player.Reset();
        }
    }
}
