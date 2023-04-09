using System;
using System.Collections;
using System.Collections.Generic;
using TD.Combat;
using UnityEngine;

namespace TD.Core
{
    public class EnemyWave : MonoBehaviour
    {
        [SerializeField] Target[] targets;
        Transform spawnPoint = null;
        Target[] targetInstances;
        
        private void Awake() 
        {
            spawnPoint = FindObjectOfType<SpawnPoint>().transform;
            BuildWave();
        }

        private void BuildWave()
        {
            targetInstances = new Target[targets.Length];
            for (int i = 0; i < targetInstances.Length; i++)
            {
                targetInstances[i] = Instantiate(targets[i], spawnPoint);
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
                yield return new WaitForSeconds(1);
            }
            gameObject.SetActive(false);
            Destroy(gameObject, 5f);
            yield break;
        }
    }
}
