using UnityEngine;
using System.Collections;

namespace CompleteProject
{
    public class CameraFollow : MonoBehaviour
    {
        public Transform target;            // The position that that camera will be following.
        public float smoothing = 5f;        // The speed with which the camera will be following.

        Vector3 offset;                     // The initial offset from the target.

        void Start()
        {
            offset = transform.position - target.position;
        }

        void FixedUpdate()
        {
            if (target != null && target.position != null)
            {
                Vector3 targetCamPos = target.position + offset;
                transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
            }
        }

        public void setTarget(Transform newTarget)
        {
            target = newTarget;
        }

    }

}