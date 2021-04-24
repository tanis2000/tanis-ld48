using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App
{
    public class Minion : MonoBehaviour
    {
        public Transform followTarget;
        public Rigidbody2D body;
        public bool pickuppable = true; 
        
        public void EnableMovement()
        {
            body.velocity = Vector3.zero;
            body.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        public void StopPickup()
        {
            pickuppable = false;
        }

        public void StartPickup()
        {
            pickuppable = true;
        }
    }
}
