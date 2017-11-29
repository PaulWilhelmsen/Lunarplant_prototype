using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathScript : MonoBehaviour
{
    public GameObject spawnPoint;   //where the path spawns and start
    public GameObject endPoint;     //where the path ends
    public GameObject[] waypoints;  //The waypoints for the path
    public float speed;

    [Range(1f, 2f)]
    public float moveFromThePlanet; //How far the path should move away from the planet.

    private bool destroyThisObject = false;
    private Vector3 startPoint;      //A vector of the startpoint. not really needed, but there it is.
    private Vector3 privEndPoint;   //same as above, but where it ends.
    private Vector3 planetCore;     //The point the object should move away from.
    private GameObject targetWaypoint;  //Current waypoint the target should go to
    public bool newPathBool;
    private int currentTarget;


    void Start()
    {
        currentTarget = 0;  //Sets target to waypoint 0
        startPoint = spawnPoint.transform.position; //set the startpoint to a vector
        transform.position = startPoint;    //transform this object to the startpoint.
        privEndPoint = endPoint.transform.position; //sets the endpoint to a vector
        CreatePath();   //Creates a bent path around a object (asteroid)
        targetWaypoint = waypoints[currentTarget];  //initiate the first waypoint
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.transform.position, Time.deltaTime * speed);    //moves the object to the waypoint
        Target();   //sets a new target if the target is close enough.
    }

    void CreatePath()   //creates the path between start and endpoint.
    {
        float radius = (startPoint - planetCore).magnitude; //gets the radius from the asteroid.
        for (int i = waypoints.Length - 1; i >= 0; i--)
        {
            Vector3 newPoint = Divided(startPoint, privEndPoint, i);    //Divides the path into smaller pieces by how big the waypoint array is.
            newPoint = NormalizeFromCore(newPoint) * (radius * moveFromThePlanet);   //adds an offset from to make the point away longer than the radius of the circle.
            waypoints[i] = new GameObject("Waypoint" + i);  //creates the new waypoint. Maybe a transform or vectors might be better, so it dosent fill up the game with new gameobjects. 
            waypoints[i].transform.position = newPoint;     //creates the path of the new waypoint.
        }
    }

    void CreateNewPath()    //same as over, just changes the waypoints instead.
    {
        float radius = (startPoint - planetCore).magnitude;
        for (int i = waypoints.Length - 1; i >= 0; i--)
        {
            Vector3 newPoint = Divided(startPoint, privEndPoint, i);
            newPoint = NormalizeFromCore(newPoint) * (radius * moveFromThePlanet);
            waypoints[i].transform.position = newPoint;
        }
    }

    void Target()
    {
        if (!destroyThisObject)
        {
            float lengthToTarget = (targetWaypoint.transform.position - transform.position).magnitude;  //gets the length to the target
            if (lengthToTarget < 3) //if its close to the target it changes to the next waypoint
                targetWaypoint = waypoints[currentTarget++];
            if (waypoints.Length == currentTarget && lengthToTarget < 3) //changes to the endpoint
                targetWaypoint = endPoint;
            if (targetWaypoint == endPoint && lengthToTarget < 3)
            {
                clearArray();
                StartCoroutine(DestroyObject(5));
                destroyThisObject = true;
            }
        }

        //IMPLEMENT SELFDESTRUCTION
    }

    public Vector3 NormalizeFromCore(Vector3 inPoint)   //Returns a normalized vector from the core
    {
        Vector3 normalizedVector = (inPoint - planetCore).normalized;
        return normalizedVector;
    }

    Vector3 Divided(Vector3 a, Vector3 b, float point)  //Divedes the path to smaller pieces
    {
        Vector3 ab = b - a; //Gets the AB vector (start to end)
        float vecLength = ab.magnitude; //gets the magnitude of it.
        ab.Normalize();
        Vector3 sum = a + (ab * vecLength * (point / waypoints.Length));   //adds from the startpoint, then multiplies it from with a partion of the length between start and end. 
        return sum;
    }

    public IEnumerator DestroyObject(float timer)
    {
        yield return new WaitForSeconds(timer);
        Destroy(gameObject);
    }

    //Gets set in another script (scenemanager)
    public void newPath(GameObject newStartPoint, GameObject newEndPoint)
    {
        spawnPoint = newStartPoint;
        endPoint = newEndPoint;
        currentTarget = 0;  //Sets target to waypoint 0
        startPoint = spawnPoint.transform.position; //set the startpoint to a vector
        transform.position = startPoint;    //transform this object to the startpoint.
        privEndPoint = endPoint.transform.position; //sets the endpoint to a vector
        if (!waypoints[0] == null)
            CreateNewPath();   //Creates a bent path around a object (asteroid)
        else
            CreatePath();

        targetWaypoint = waypoints[currentTarget];  //initiate the first waypoint
        transform.position = spawnPoint.transform.position;
    }

    public void clearArray()
    {
        foreach (GameObject waypoint in waypoints)
        {
            Destroy(waypoint.gameObject);
        }
    }
}
