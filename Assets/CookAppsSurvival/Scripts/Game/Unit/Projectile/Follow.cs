using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookApps.Game
{
    public class Follow : MonoBehaviour
    {
        private Transform target;
        private Vector3 offset;

        public void SetTarget(Transform target, Vector3 offset)
        {
            this.target = target;
            this.offset = offset;
        }

        private void Update()
        {
            if (target == null) return;

            transform.position = target.position + offset;
        }
    }
}
