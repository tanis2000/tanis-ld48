using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace App
{
    public class Eyes : MonoBehaviour
    {
        public GameObject eyeL;
        public GameObject eyeR;

        public float blinkLength = .1f;
        public float minBlinkTime = 1;
        public float maxBlinkTime = 5f;
        public int minBlinkTimes = 1;
        public int maxBlinkTimes = 3;
        
        private int blinkTimes = 0;
        private float nextBlinkIn = 0f;
        
        private void Update()
        {
            if (blinkTimes == 0)
            {
                nextBlinkIn = Random.Range(minBlinkTime, maxBlinkTime);
                blinkTimes = Random.Range(minBlinkTimes, maxBlinkTimes);
                StartCoroutine(Blink());
            }
            
            
        }

        private IEnumerator Blink()
        {
            for (var i = blinkTimes; i >= blinkTimes; i--)
            {
                yield return new WaitForSeconds(nextBlinkIn);
                eyeL.SetActive(false);
                eyeR.SetActive(false);
                yield return new WaitForSeconds(blinkLength);
                eyeL.SetActive(true);
                eyeR.SetActive(true);
                yield return new WaitForSeconds(blinkLength);
            }

            blinkTimes = 0;
        }
    }
}
