using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DrawAndRun
{

    public class ClonesSpawner : MonoBehaviour
    {
        [SerializeField] private Transform clonePlace;
        [SerializeField] private List<SingleClone> clones = new List<SingleClone>();
        [SerializeField] private GameObject clonePF;
        private bool levelStarted = false;
        public int currentClonesCount { get { return clones.Count; } }
        private void Awake()
        {
            clones = new List<SingleClone>();
            SubEvents();
        }

        private void SubEvents()
        {
            GameManager.instance.eventsManager.LevelInit.AddListener(OnLevelInit);
            GameManager.instance.eventsManager.LevelEnd.AddListener(OnLevelEnd);
            GameManager.instance.eventsManager.GameOver.AddListener(OnGameOver);
        }

        public void AddToCrue(SingleClone unit)
        {
            clones.Add(unit);
            unit.gameObject.transform.parent = clonePlace;

        }
        public void DeleteFromCrue(SingleClone unit)
        {
            clones.Remove(unit);
            if (clones.Count == 0)
                GameManager.instance.eventsManager.GameOver.Invoke();
        }

        private void OnLevelEnd()
        {
            clones.Clear();
            levelStarted = false;
        }
        private void OnGameOver()
        {
            clones.Clear();
            levelStarted = false;
        }
        private void OnLevelInit()
        {
            levelStarted = false;
            for (int i = 0; i < GameData.instance.clonesStartNum; i++)
            {
               GameObject unit = Instantiate(clonePF);
               SingleClone clone = unit.GetComponent<SingleClone>();
               AddToCrue(clone);
                
            }
            ReArrangeClones( ClonesAlignment.instance.GetDefaultAlignment(clones.Count),true);
        }

        public void ReArrangeClones(List<Vector3> newPositions, bool isLocal = false)
        {
            if(clones.Count != newPositions.Count)
            {
                Debug.LogWarning("wrong positions list");
                return;
            }
            for(int i=0; i < newPositions.Count; i++)
            {
                if(isLocal)
                    clones[i].transform.localPosition = newPositions[i];
                else
                    clones[i].transform.position = newPositions[i];
            }
        }
    }
}