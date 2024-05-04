using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookApps.Game
{
    public class Billboarding : MonoBehaviour
    {
        Transform cam;

        Quaternion offset = Quaternion.Euler(-1, 180, 0);

        void Start()
        {
            cam = Camera.main.transform;
        }

        void Update()
        {
            transform.rotation = Quaternion.LookRotation(cam.position - transform.position) * offset;
        }
    }
}