using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TD.Core
{
    public class WaveSpawner : MonoBehaviour
    {
        [SerializeField] EnemyWave[] wavesToSpawn;
        [SerializeField] float timeBetweenWaves = 30f;
        [SerializeField] float timeBeforeFirstWave = 30f;
        //Transform spawnPoint;
        EnemyWave[] waveInstances;

        private void Awake() 
        {
            //spawnPoint = GameObject.FindWithTag("SpawnPoint").transform;
            BuildWavesToSpawn();    
        }
        private void Start() 
        {
            StartCoroutine(StartSpawningWaves());    
        }

        private void BuildWavesToSpawn()
        {
            waveInstances = new EnemyWave[wavesToSpawn.Length];
            for (int i = 0; i < wavesToSpawn.Length; i++)
            {
                waveInstances[i] = Instantiate(wavesToSpawn[i], transform);
            }
        }

        private IEnumerator StartSpawningWaves()
        {
            yield return new WaitForSeconds(timeBeforeFirstWave);
            StartCoroutine(SpawnWaves());
            yield break;
        }
        
        private IEnumerator SpawnWaves()
        {
            for (int i = 0; i < wavesToSpawn.Length; i++)
            {
                if(i+1 == wavesToSpawn.Length)
                {
                    waveInstances[i].StartSpawning();
                    print("last wave");
                    yield break;
                }
                waveInstances[i].StartSpawning();
                print($"Wave {i}");
                yield return new WaitForSeconds(timeBetweenWaves);
            }
        }
    }
}
