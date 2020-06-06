using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class PatrolPath : MonoBehaviour
    {
        private void OnDrawGizmos()
        {
            const float waypointGizmoRadius = 0.3f;

            for (int i = 0; i < transform.childCount ; i++)
            {
                Vector3 waypoint = GetWaypoiny(i);
                Vector3 nextWaypoint = GetWaypoiny(GetNextIndex(i));

                Gizmos.DrawSphere(waypoint, waypointGizmoRadius);
                Gizmos.DrawLine(waypoint, nextWaypoint);
            }
        }

        public int GetNextIndex(int i)
        {
            return i + 1 == transform.childCount ? 0 : i + 1;
        }

        public Vector3 GetWaypoiny(int i)
        {
            return transform.GetChild(i).position;
        }
    }
}