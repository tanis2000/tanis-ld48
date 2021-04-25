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
        public Transform enemiesHolder;
        public Transform fishHolder;
        public Rigidbody2D body;

        public float acceleration = 10f;
        public float maxAngle = 45f;
        public int totalMinions;
        public bool userRigidBodyRotation = false;
        
        private Transform _room;
        private float angle = 0f;
        private Bounds bounds;
        private Vector3 initialVector;
        private Vector3 initialPosition;
        private Quaternion initialRotation;
        
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

            if (dir != 0)
            {
                if (userRigidBodyRotation)
                {
                    angle += dir * acceleration * Time.fixedDeltaTime;
                    body.MoveRotation(angle);
                }
                else
                {
                     angle = dir * acceleration * Time.fixedDeltaTime;
                     var currentVector = transform.position - bounds.center;
                     currentVector.z = 0;
                     var angleBetween = Vector3.Angle(initialVector, currentVector) * (Vector3.Cross(initialVector, currentVector).z > 0 ? 1 : -1);            
                     float newAngle = Mathf.Clamp(angleBetween + angle, -maxAngle, maxAngle);
                     angle = newAngle - angleBetween;
                    _room.RotateAround(bounds.center, Vector3.forward, angle);
                }
            }
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

        public IEnumerator ResetOrientation()
        {
            while (_room.localPosition != initialPosition && _room.localRotation != initialRotation)
            {
                _room.localPosition = Vector3.Lerp(_room.localPosition, initialPosition, Time.deltaTime);
                _room.localRotation = Quaternion.Slerp(_room.localRotation, initialRotation, Time.deltaTime);
                yield return null;
                //_room.localPosition = initialPosition;
                //_room.localRotation = initialRotation;
            }
        }

        public IEnumerator Activate()
        {
            Debug.Log("Activating level");
            MusicManager.instance.CrossFade(music, false);
            exit.reached = false;
            yield return ResetOrientation();
            yield return null;
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
            foreach (var minion in pickupsHolder.GetComponentsInChildren<Minion>())
            {
                Destroy(minion.gameObject);
            }
            foreach (var enemy in enemiesHolder.GetComponentsInChildren<Enemy>())
            {
                Destroy(enemy.gameObject);
            }
            foreach (var fish in fishHolder.GetComponentsInChildren<Fish>())
            {
                Destroy(fish.gameObject);
            }

            DisableExit();
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
            player.body.MovePosition(playerSpawn.position);
            player.body.bodyType = RigidbodyType2D.Dynamic;
            //player.body.simulated = true;
            yield return new WaitForFixedUpdate();
            EnableExit();
            while (player.health.amount > 0 && !exit.reached)
            {
                yield return new WaitForFixedUpdate();
            }
            controls.LockControls();
            yield return new WaitForFixedUpdate();
            if (player.health.amount > 0)
            {
                player.ShowOverhead($"{player.minionsCollected} / {totalMinions} SAVED!");
            }
            else
            {
                player.ShowOverhead($"You died!");
            }
            yield return ResetOrientation();
            yield return new WaitForSeconds(5.0f);
            player.Reset();
        }

        public void DisableExit()
        {
            exit.gameObject.SetActive(false);
        }

        public void EnableExit()
        {
            exit.gameObject.SetActive(true);
        }
    }
}
