using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace TD.Control
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] Path path;
        [SerializeField] float waypointTolerance = 2f;
        List<Vector3> waypoints = null;
        int currentWaypointIndex = 0;
        NavMeshAgent enemyNavMesh;
        Vector3 startingPosition;

        private void Awake() 
        {
            enemyNavMesh = GetComponent<NavMeshAgent>();
            path = FindObjectOfType<Path>();
        }

        private void Update()
        {
            MovementBehaviour();
        }

        private void MovementBehaviour()
        {
            if (waypoints != null)
            {
                if (currentWaypointIndex == waypoints.Count)
                {
                    enemyNavMesh.isStopped = true;
                    return;
                }

                enemyNavMesh.SetDestination(waypoints[currentWaypointIndex]);

                if (AtWaypoint())
                {
                    currentWaypointIndex += 1;
                    return;
                }
            }
            else return;
        }

        private void OnEnable() 
        {
            startingPosition = transform.position;
            waypoints = path.GetPath();
        }

        public void OnDeath() 
        {
            enemyNavMesh.enabled = false;
            transform.position = startingPosition;
            waypoints = null;
            currentWaypointIndex = 0;
            GetComponent<EnemyController>().enabled = false;
            gameObject.SetActive(false); 
        }
        
        private Vector3 GetCurrentWaypoint()
        {
            return path.GetWaypoint(currentWaypointIndex);
        }
        
        private bool AtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
            return distanceToWaypoint < waypointTolerance;
        }
    }
}
