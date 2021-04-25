using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App
{
    public class Exit : MonoBehaviour
    {
        public TriggerArea trigger;
        public bool reached;
        
        private void Start()
        {
            reached = false;
        }

        private void FixedUpdate()
        {
            if (trigger.Triggered)
            {
                foreach (var triggerCollider in trigger.Colliders)
                {
                    if (triggerCollider.GetComponent<Player>() != null)
                    {
                        Debug.Log("Exit reached");
                        reached = true;
                    }
                }
            }
        }
    }
}
