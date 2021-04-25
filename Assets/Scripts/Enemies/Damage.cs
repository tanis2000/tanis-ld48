using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App
{
    public class Damage : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            var player = other.GetComponent<Player>();
            if (player != null)
            {
                player.health.Damage(1);
            }

            var minion = other.GetComponent<Minion>();
            if (minion != null)
            {
                // TODO: damage minions
            }
        }
    }
}
