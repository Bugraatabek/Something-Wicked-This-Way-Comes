using System.Collections;
using TD.Control;
using UnityEngine;

namespace TD.Core
{
    public class SpawnPoint : MonoBehaviour 
    {
        [SerializeField] Path path;
        [SerializeField] EnemyWave[] enemyWaves;
        [SerializeField] float timeBetweenWaves;
        [SerializeField] float timeBeforeFirstWave;

        EnemyWave[] waveInstances;
        
        private void Awake() 
        {
            BuildEnemyWaves();
        }
        
        private void BuildEnemyWaves()
        {
            waveInstances = new EnemyWave[enemyWaves.Length];
            for (int i = 0; i < enemyWaves.Length; i++)
            {
                waveInstances[i] = Instantiate(enemyWaves[i], transform);
            }
        }

        private void Start() 
        {
            StartCoroutine(StartSpawningWaves());    
        }

        private IEnumerator StartSpawningWaves()
        {
            yield return new WaitForSeconds(timeBeforeFirstWave);
            StartCoroutine(SpawnWaves());
            yield break;
        }

        private IEnumerator SpawnWaves()
        {
            for (int i = 0; i < waveInstances.Length; i++)
            {
                if(i+1 == waveInstances.Length)
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

        public Path GetPath()
        {
            return path;
        }
    }
}