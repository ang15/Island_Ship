using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoints : MonoBehaviour
{
    [Range(0f,2f)]
    [SerializeField] private float waipoints=1f;
    int index = 0;
    
    private void OnDrawGizmos()
    {
        foreach (Transform t in transform)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(t.position, waipoints);
        }
        Gizmos.color = Color.red;
        for (int i = 0; i < transform.childCount - 1; i++)
        {
            Gizmos.DrawLine(transform.GetChild(i).position, transform.GetChild(i + 1).position);
        }
        Gizmos.DrawLine(transform.GetChild(transform.childCount - 1).position, transform.GetChild(0).position);
    }

    public Transform GetNextWayPointNull(Transform currentWayPoint)
    {

        if (currentWayPoint == null)
        {
            index = 0;
        }
        return transform.GetChild(index);

    }

    public Transform GetNextWayPoint()
    {
        if (index<transform.childCount-1)
        {
            index++;
        }
        else
        {
            index =0;
        }
        return transform.GetChild(index);
    }
}