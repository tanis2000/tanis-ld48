using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace App
{
    public class Controls : MonoBehaviour
    {
        public bool jump;
        public bool wasJump;
        public bool explode;
        public bool wasExplode;
        public bool implode;
        public bool wasImplode;
        public bool left;
        public bool wasLeft;
        public bool right;
        public bool wasRight;

        public bool locked;
        
        private void FixedUpdate()
        {
            wasJump = jump;
            wasLeft = left;
            wasRight = right;
            wasExplode = explode;
            wasImplode = implode;

            jump = Input.GetButton("Jump");
            left = Input.GetAxis("Horizontal") < 0;
            right = Input.GetAxis("Horizontal") > 0;
            implode = Input.GetButton("Fire1");
            explode = Input.GetButton("Fire2");
        }

        public void LockControls()
        {
            locked = true;
        }

        public void UnlockControls()
        {
            locked = false;
        }
    }
}
