
using UnityEngine;

namespace DrawAndRun
{
    public class CloneCollisionManager : MonoBehaviour
    {
        private SingleClone controller;
        private Collider _collider;
        private void Awake()
        {
            _collider = GetComponentInChildren<Collider>();
        }
        private void Start()
        {
            if (_collider == null) _collider = GetComponentInChildren<Collider>();

        }
        public void Init(SingleClone controller)
        {
            this.controller = controller;
        }
        public void SetToTrigger(bool trigger)
        {
            if (trigger)
                _collider.isTrigger = true;
            else
                _collider.isTrigger = false;
        }
        private void OnTriggerEnter(Collider other)
        {
            switch(other.gameObject.tag){
                case "Consumable":
                    GameManager.instance.playerData.AddScore(GameData.instance.scorePerCrystal);
                    other.gameObject.SetActive(false);
                    break;
                case "Obstacle":
                    StartCoroutine(controller.Die());
                    break;
                case "LevelFinish":
                    GameManager.instance.eventsManager.LevelEnd.Invoke();
                    break;
                case "Dummy":
                    Debug.Log("Dummy");
                  //  GameManager.instance.clonesSpawner.AddToCrue(other.GetComponent<SingleClone>());
                    break;
            }
        }
    }
}