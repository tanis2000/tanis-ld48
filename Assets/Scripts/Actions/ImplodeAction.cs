using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App
{
    public class ImplodeAction : BaseAction
    {
        public float acceleration = 50f;
        private bool implodePressed; 
        private bool wasImplodePressed; 
        public override void UpdateAction()
        {
            base.UpdateAction();
            wasImplodePressed = implodePressed;
            
            implodePressed = Input.GetKey(KeyCode.V);

            if (wasImplodePressed)
            {
                Implode();
            }
        }

        private void Implode()
        {
            var minions = FindObjectsOfType<Minion>();
            foreach (var minion in minions)
            {
                if (minion.followTarget == transform)
                {
                    var delta = minion.transform.position - transform.position;
                    minion.body.AddForce(-delta.normalized * acceleration, ForceMode2D.Force);
                    minion.StartPickup();
                }
            }
        }
    }
}
