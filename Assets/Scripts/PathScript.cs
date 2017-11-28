using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathScript : MonoBehaviour {
    public GameObject spawnPoint;
    public GameObject endPoint;
    public Vector3[] waypoints;
    public float speed;
    public Vector3 moveFromThePlanet;
    public Vector3 startPoint;
    private Vector3 privEndPoint;
    private Vector3 planetCore;

    public GameObject locationTest;
	// Use this for initialization
	void Start () {
        startPoint = spawnPoint.transform.position;
        privEndPoint = endPoint.transform.position;
        SetFirstPoint();
        SetLastPoint();
        CreatePath();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void SetFirstPoint()
    {
        Vector3 firstPoint = NormalizeFromCore(startPoint);
        firstPoint += moveFromThePlanet;
        if (startPoint.x < 0) //starts on the "west" side
        {

        }
        waypoints[0] = firstPoint;
    }

    void SetLastPoint()
    {
        Vector3 lastPoint = NormalizeFromCore(privEndPoint);
        waypoints[waypoints.Length - 1] = lastPoint += moveFromThePlanet;
    }

    void CreatePath()
    {
        float radius = (startPoint - planetCore).magnitude;
        Vector3 newPoint = Divided(startPoint, privEndPoint);
        newPoint = NormalizeFromCore(newPoint) * (radius * 1.2f);
        locationTest = new GameObject("testObject");
        locationTest.transform.position = newPoint;
        for(int i = 1;i < waypoints.Length - 2;i++)
        {

        }
    }

    public Vector3 NormalizeFromCore(Vector3 inPoint)
    {
        Vector3 normalizedVector = (inPoint - planetCore).normalized;
        return normalizedVector;
    }

    Vector3 Divided(Vector3 a, Vector3 b)
    {
        Vector3 ab = b - a;
        float vecLength = ab.magnitude;
        Debug.Log(vecLength);
        ab.Normalize();
        Vector3 sum = a + (ab * vecLength/2);
        return sum;
    }
}
