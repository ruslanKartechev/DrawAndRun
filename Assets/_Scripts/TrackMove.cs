using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DrawAndRun {
    public class TrackMove : MonoBehaviour
    {
        [SerializeField] private bool doMove;
        private float velocity;
        [SerializeField] private Vector3 startPosition = new Vector3();
        private void Awake()
        {
            SubEvents();
        }
        private void SubEvents()
        {
            GameManager.instance.eventsManager.GameOver.AddListener(StopMoving);
            GameManager.instance.eventsManager.LevelEnd.AddListener(StopMoving);
        }
        public void Init(float speed, bool start = true)
        {
            velocity = speed;
            doMove = start;
            transform.position = startPosition;
            if (start)
            {
                StartMoving();
            }
        }
        private void StartMoving()
        {
            doMove = true;
            StartCoroutine(MovementHandler());
        }
        private void StopMoving()
        {
            doMove = false;
        }
        private IEnumerator MovementHandler()
        {
            Time.timeScale = 1f;
            while (doMove)
            {
                transform.localPosition += new Vector3(0, 0, -velocity)*Time.deltaTime;
                yield return null;
            }
        }
    }
}