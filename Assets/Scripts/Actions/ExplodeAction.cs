using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App
{
    public class ExplodeAction : BaseAction
    {
        public Player player;
        public Minion minionPrefab;
        public WorldController worldController;
        public float acceleration = 1000f;
        
        public override void UpdateAction()
        {
            base.UpdateAction();
            if (Input.GetKeyDown(KeyCode.C))
            {
                Explode();
            }
        }

        private void Explode()
        {
            SpawnMinions();
        }

        private void SpawnMinions()
        {
            for (var i = 0; i < player.minionsCollected ; i++)
            {
                var minion = Instantiate(minionPrefab, transform.position + transform.up * 2f, Quaternion.identity);
                minion.transform.parent = worldController.currentRoom.pickupsHolder;
                minion.EnableMovement();
                minion.StopPickup();
                minion.followTarget = player.transform;
                var velocity = Vector2.zero;
                velocity.x = Random.Range(-1, 1) * acceleration;
                velocity.y = Random.Range(.5f, 1) * acceleration;
                minion.body.AddForce(velocity, ForceMode2D.Force);
            }

            player.minionsCollected = 0;
        }
    }
}
