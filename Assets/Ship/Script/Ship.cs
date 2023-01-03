using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    [SerializeField] private WayPoints wayPoints;

    [SerializeField] private float spead;
    [SerializeField] private Transform targetDestroy;
    [SerializeField] private float distanceTheFloat = 0.1f;
    [SerializeField] private GameObject gameObjectShip;
    public GameObject prefabShip;

    public int Health;
    public float moveSpead;

    private bool onRotationDestroy = true;
    private bool onRotation = true;
    private Transform currentWayPoint;
    private Quaternion quaternioncurrentWayPoint;


    private void Start()
    {
        currentWayPoint = wayPoints.GetNextWayPointNull(currentWayPoint);

        quaternioncurrentWayPoint = currentWayPoint.rotation;
        currentWayPoint = wayPoints.GetNextWayPoint();
    }

    private void Update()
    {
        if (onRotationDestroy && Health == 0)
        {
            StartCoroutine(HealthShip());
        }

        if (onRotation)
        {
            if (Vector3.Distance(transform.position, currentWayPoint.position) < distanceTheFloat)
            {
                quaternioncurrentWayPoint = currentWayPoint.rotation;
                currentWayPoint = wayPoints.GetNextWayPoint();
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, currentWayPoint.position, moveSpead * Time.deltaTime);
                transform.rotation = Quaternion.Lerp(transform.rotation, quaternioncurrentWayPoint, spead * Time.deltaTime);
            }
        }

    }

    IEnumerator HealthShip()
    {
        float angle = 1f;
        onRotation = false;
        onRotationDestroy = false;
        targetDestroy.eulerAngles = new Vector3(targetDestroy.eulerAngles.x, transform.eulerAngles.y, targetDestroy.eulerAngles.z);
        while (transform.eulerAngles.x < targetDestroy.eulerAngles.x - 1)
        {
            yield return new WaitForSeconds(0.000001f);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetDestroy.rotation, angle * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, 1, transform.position.z), 20 * Time.deltaTime);
        }
        InstantiatePrefab();
        Destroy(gameObjectShip);
    }

    private void InstantiatePrefab()
    {
        GameObject shipPref = Instantiate(prefabShip);
        shipPref.transform.eulerAngles = new Vector3(gameObjectShip.transform.eulerAngles.x, gameObjectShip.transform.eulerAngles.y, gameObjectShip.transform.eulerAngles.z);
        shipPref.transform.position = new Vector3(gameObjectShip.transform.position.x, gameObjectShip.transform.position.y, gameObjectShip.transform.position.z);
        shipPref.GetComponentInChildren<Ship>().prefabShip = prefabShip;
    }
}