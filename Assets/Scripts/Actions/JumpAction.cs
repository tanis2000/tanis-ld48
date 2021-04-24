using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace App
{
    public class JumpAction : BaseAction
    {
        public TriggerArea grounding;
        public Rigidbody2D body;
        public float initialForce = 1000f;
        public float maxHeldTime = 1f;
        public float timeThreshold = .2f;
        private float heldTime = 0f;
        private bool jumpPressed;
        
        public override void UpdateAction()
        {
            base.UpdateAction();
            var onGround = grounding.Triggered;

            if (Input.GetKey(KeyCode.Space))
            {
                jumpPressed = true;
            }
            else
            {
                jumpPressed = false;
            }

            if (jumpPressed)
            {
                heldTime += Time.fixedDeltaTime;
                heldTime = Mathf.Clamp(heldTime, 0, maxHeldTime);
            }
            
            if (!jumpPressed && onGround && heldTime > 0)
            {
                if (heldTime < timeThreshold)
                {
                    heldTime = 0.5f;
                }
                Jump();
            }
        }

        private void Jump()
        {
            body.velocity = transform.right * body.velocity.x;
            body.AddForce(transform.up * initialForce * heldTime);
            
            // Make minions jump too
            var minions = FindObjectsOfType<Minion>();
            foreach (var minion in minions)
            {
                minion.body.velocity = transform.right * body.velocity.x;
                minion.body.AddForce(transform.up * initialForce * heldTime);
            }
            
            heldTime = 0;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, transform.up);
        }
    }
}
