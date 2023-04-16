using System.Collections;
using System.Collections.Generic;
using TD.Control;
using UnityEngine;
using UnityEngine.AI;

namespace TD.AI
{
    public class MoveAction : MonoBehaviour, IAction
    {
        List<Vector3> waypoints = null;
        Path path;
        [SerializeField] float waypointTolerance = 2f;
        NavMeshAgent enemyNavMesh;
        int currentWaypointIndex = 0;
        ActionScheduler actionScheduler;
        bool isActiveAction = false;

        //Private//
        
        private void Awake() 
        {
            enemyNavMesh = GetComponent<NavMeshAgent>();
        }

        public IEnumerator MoveRoutine()
        {
            while(true)
            {
                if(isActiveAction == false) 
                {
                    print("breaking move routine");
                    yield break;
                }
                if (waypoints != null)
                {
                    enemyNavMesh.isStopped = false;
                    if (currentWaypointIndex == waypoints.Count)
                    {
                        waypoints = null;
                        yield break;
                    }

                    enemyNavMesh.SetDestination(waypoints[currentWaypointIndex]);

                    if (AtWaypoint())
                    {
                        currentWaypointIndex += 1;
                        yield return null;
                    }
                }
                yield return null;
            }
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

        //Public//

        public void MoveTo(Vector3 position)
        {
            enemyNavMesh.isStopped = false;
            enemyNavMesh.destination = position;
        }

        public void Stop()
        {
            enemyNavMesh.isStopped = true;
        }

        public void SetPath(Path path)
        {
            this.path = path;
            waypoints = path.GetPath();
        }

        public void Cancel()
        {
            isActiveAction = false;
        }

        public void StartAction()
        {
            isActiveAction = true;
            StartCoroutine(MoveRoutine());
        }
    }    
}
