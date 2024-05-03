using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookApps.Game
{
    public class Billboarding : MonoBehaviour
    {
        Transform cam;

        void Start()
        {
            cam = Camera.main.transform;
            transform.LookAt(transform.position + cam.rotation * Vector3.forward, cam.rotation * Vector3.up);
        }

        void Update()
        {
            
        }
    }
}