using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App
{
    public class WorldController : MonoBehaviour
    {
        public List<RoomController> rooms = new List<RoomController>();
        public Player player;
        
        public bool controlsLocked;
        public float acceleration = 10f;
        public float maxAngle = 45f;
        
        private Transform _world;
        private float angle = 0f;
        
        private int roomIndex;
        private int loop;

        public RoomController currentRoom => rooms[roomIndex];

        private void Awake()
        {
            _world = transform;
        }

        private void Start()
        {
            StartCoroutine(MainLoop());
        }
        
        private IEnumerator MainLoop()
        {
            yield return new WaitForFixedUpdate();
            Debug.Log("Starting new world");
            while (true)
            {
                currentRoom.gameObject.SetActive(true);
                yield return currentRoom.Activate();
                yield return currentRoom.PlayRoom();
                Debug.Log("Room finished");
                if (player.health.amount <= 0)
                {
                    break;
                } 
                currentRoom.Deactivate();
                yield return new WaitForSeconds(1.0f);
                roomIndex++;
                if (roomIndex == rooms.Count)
                {
                    roomIndex = 0;
                    loop++;
                }
                GC.Collect();
            }
            Debug.Log("Player died");
            roomIndex = 0;
            loop = 0;
            player.health.amount = player.health.maxAmount;
            StartCoroutine(MainLoop());
        }
    }
}
