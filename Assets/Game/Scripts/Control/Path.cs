using System.Collections.Generic;
using UnityEngine;

namespace TD.Control
{
    public class Path : MonoBehaviour
    {
        const float waypointGizmoRadius = 0.3f;
        
        private void OnDrawGizmos() 
        {
           for (int i = 0; i < transform.childCount; i++)
            {
                int j = GetNextIndex(i);
                Gizmos.DrawSphere(GetWaypoint(i), waypointGizmoRadius);
                Gizmos.DrawLine(GetWaypoint(i), GetWaypoint(j));
            }
        }

        public List<Vector3> GetPath()
        {
            List<Vector3> path = new List<Vector3>();
            for (int i = 0; i < transform.childCount; i++)
            {
                path.Add(GetWaypoint(i));
            }
            return path;
        }

        public int GetNextIndex(int i)
        {
            if(i + 1 == transform.childCount)
            {
                return i;
            } 
            return i + 1;
        }

        public Vector3 GetWaypoint(int i)
        {
            return transform.GetChild(i).position;
        }
    }
}
