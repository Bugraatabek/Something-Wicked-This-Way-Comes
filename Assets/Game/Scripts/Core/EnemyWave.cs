using System;
using System.Collections;
using System.Collections.Generic;
using TD.AI;
using TD.Combat;
using TD.Control;
using UnityEngine;

namespace TD.Core
{
    public class EnemyWave : MonoBehaviour
    {
        [SerializeField] Target[] targets;
        SpawnPoint spawnPoint = null;
        Target[] targetInstances;
        
        private void Awake() 
        {
            spawnPoint = GetComponentInParent<SpawnPoint>();
            BuildWave();
        }

        private void BuildWave()
        {
            targetInstances = new Target[targets.Length];
            for (int i = 0; i < targetInstances.Length; i++)
            {
                targetInstances[i] = Instantiate(targets[i], spawnPoint.transform);
                targetInstances[i].gameObject.SetActive(false);
            }
        }

        public void StartSpawning() 
        {
            StartCoroutine(SpawnWave());    
        }

        private IEnumerator SpawnWave()
        {
            List<GameObject> enemiesToCheck = new List<GameObject>();
            for (int i = 0; i < targets.Length; i++)
            {
                targetInstances[i].gameObject.SetActive(true);
                targetInstances[i].GetComponent<MoveAction>().SetPath(spawnPoint.GetPath());
                yield return new WaitForSeconds(1);
            }
            gameObject.SetActive(false);
            Destroy(gameObject, 5f);
            yield break;
        }
    }
}
