using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DrawAndRun
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform followTarget;
        [SerializeField] private bool doFollow;
        [SerializeField] private Vector3 defaultRot = new Vector3();
        [SerializeField] private Vector3 defaultPosOffset = new Vector3();


        void Start()
        {
            if (followTarget == null) doFollow = false;

        }

        public void Init(Transform target, bool start = true)
        {
            followTarget = target;
            if (start)
                StartFollowing();
        }
        public void StartFollowing()
        {
            doFollow = true;
            StartCoroutine(Follow());
        }
        public void StopFollowing()
        {
            doFollow = false;
        }
        private IEnumerator Follow()
        {
            while (doFollow)
            {
                transform.position = followTarget.position + defaultPosOffset;
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, followTarget.eulerAngles.y, transform.eulerAngles.z);
                yield return null;
            }
        }

    }
}