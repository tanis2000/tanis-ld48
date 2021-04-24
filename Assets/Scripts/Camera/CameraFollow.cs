using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

namespace App
{
    public class CameraFollow : MonoBehaviour
    {
        public Transform target;
        public AnimationCurve followCurve;
        public float followDamp = 10f;
        public Vector3 followOffset = Vector3.up * 3f;
        public float Width = 10f;
        public float Height = 10f;
        public float MoveSpeed = 10f;

        private Vector3 position = Vector3.zero;
        private Vector3 velocity = Vector3.zero;

        private void LateUpdate()
        {
            if (target != null)
            {
                position = target.position;
            }

            var delta = position - (transform.position + followOffset);
            velocity -= velocity * followDamp * Time.deltaTime;

            if (Mathf.Abs(delta.x) > Width || Mathf.Abs(delta.y) > Height)
            {
                velocity += new Vector3(delta.x, delta.y) * MoveSpeed * Time.deltaTime;
            }

            transform.position += velocity * Time.deltaTime;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(new Vector3(transform.position.x, transform.position.y, 0) + followOffset, new Vector3(Width * 2f, Height * 2f, 0));
        }
    }
}
