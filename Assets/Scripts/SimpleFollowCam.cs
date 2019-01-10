using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kodfreedom
{
    public class SimpleFollowCam : MonoBehaviour
    {
        [SerializeField] Transform target;
        private Vector3 offset;

        private void Start()
        {
            offset = transform.position;
        }

        private void Update()
        {
            if(target)
            {
                transform.position = target.position + offset;
            }
        }
    }
}