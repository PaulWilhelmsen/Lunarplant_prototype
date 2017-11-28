using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathScript : MonoBehaviour {
    public GameObject spawnPoint;
    public GameObject endPoint;
    public GameObject[] waypoints;
    public float speed;
    [Range(1f,2f)]
    public float moveFromThePlanet;
    public Vector3 startPoint;

    private Vector3 privEndPoint;
    private Vector3 planetCore;
    private GameObject targetWaypoint;
    private int currentTarget;
	// Use this for initialization
	void Start () {
        currentTarget = 0;
        startPoint = spawnPoint.transform.position;
        transform.position = startPoint;
        privEndPoint = endPoint.transform.position;
        CreatePath();
        targetWaypoint = waypoints[currentTarget];
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.transform.position, Time.deltaTime * speed);
        Target();
	}

    void CreatePath()
    {
        float radius = (startPoint - planetCore).magnitude;
        for (int i = waypoints.Length - 1;i >= 0 ;i--)
        {
            Vector3 newPoint = Divided(startPoint, privEndPoint, i);
            newPoint = NormalizeFromCore(newPoint) * (radius * 1.3f);
            waypoints[i] = new GameObject("Waypoint" + i);
            waypoints[i].transform.position = newPoint;
        }
    }

    void Target()
    {
        float lengthToTarget = (targetWaypoint.transform.position - transform.position).magnitude;
        Debug.Log(lengthToTarget);
        if (lengthToTarget < 3)
        targetWaypoint = waypoints[currentTarget++];
        if(waypoints.Length == currentTarget && lengthToTarget < 3)
        {
            targetWaypoint = endPoint;
        }
    }

    public Vector3 NormalizeFromCore(Vector3 inPoint)
    {
        Vector3 normalizedVector = (inPoint - planetCore).normalized;
        return normalizedVector;
    }

    Vector3 Divided(Vector3 a, Vector3 b, float point)
    {
        Vector3 ab = b - a;
        float vecLength = ab.magnitude;
        ab.Normalize();
        Vector3 sum = a + (ab * vecLength* (point/waypoints.Length));
        return sum;
    }
}
