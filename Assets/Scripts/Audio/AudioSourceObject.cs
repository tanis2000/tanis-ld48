using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App
{
    public class AudioSourceObject : MonoBehaviour
    {
        public AudioSource audioSource;

        private void Update()
        {
            if (!audioSource.isPlaying)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
