using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Kodfreedom
{
    public class BallController : MonoBehaviour
    {
        [SerializeField] Slider powerSilider;
        [SerializeField] Text accUi;
        private Rigidbody rigidBody;
        private Vector3 origin;
        private Vector3 acc_origin;

        private void Start()
        {
            rigidBody = GetComponent<Rigidbody>();
            origin = transform.position;
            acc_origin = Input.acceleration;
        }

        private void Update()
        {
            var gravity_acc = Input.acceleration;
            accUi.text = (1f / Time.deltaTime) + "\n" +
                acc_origin.ToString() + gravity_acc.ToString();
            gravity_acc.z = gravity_acc.y - acc_origin.y;
            gravity_acc.y = 0f;
            rigidBody.AddForce(gravity_acc * powerSilider.value);
        }

        private void OnTriggerEnter(Collider other)
        {
            transform.position = origin;
            transform.rotation = Quaternion.identity;
            rigidBody.velocity = Vector3.zero;
        }
    }
}