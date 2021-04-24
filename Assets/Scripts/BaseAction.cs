using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App
{
    public class BaseAction : MonoBehaviour
    {
        public virtual void UpdateAction() {}
        public virtual void ResetAction() {}

        public void FixedUpdate()
        {
            UpdateAction();
        }

    }
}
