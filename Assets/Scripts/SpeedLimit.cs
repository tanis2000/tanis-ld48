using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App
{
    public class SpeedLimit : MonoBehaviour
    {
        public Rigidbody2D body;
        public float maxHVelocity = 100f; 
        public float maxVVelocity = 100f;
        public float drag = 5f;
        private void FixedUpdate()
        {
            var velocity = body.velocity;
            velocity.x = Mathf.Clamp(velocity.x, -maxHVelocity, maxHVelocity);
            velocity.y = Mathf.Clamp(velocity.y, -maxVVelocity, maxVVelocity);
            if (Mathf.Abs(velocity.x) > 0f)
            {
                velocity.x -= velocity.x * drag * Time.fixedDeltaTime;
            }
            body.velocity = velocity;
        }
    }
}
