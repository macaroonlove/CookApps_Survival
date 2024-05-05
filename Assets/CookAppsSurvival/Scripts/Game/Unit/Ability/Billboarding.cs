using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookApps.Game
{
    public class Billboarding : MonoBehaviour
    {
        Transform cam;
        Transform body;

        Quaternion offset = Quaternion.Euler(-1, 180, 0);

        void Start()
        {
            cam = Camera.main.transform;
            body = transform.GetChild(1);
        }

        void Update()
        {
            body.rotation = Quaternion.LookRotation(cam.position - transform.position) * offset;
        }
    }
}