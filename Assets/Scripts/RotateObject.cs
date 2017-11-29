using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour {
    [Range(0,100)]
    public float rotateX;
    [Range(0, 100)]
    public float rotateY;
    [Range(0, 100)]
    public float rotateZ;
    [Range(0, 100)]
    public float speed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(rotateX, rotateY * speed * Time.deltaTime, rotateZ * speed * Time.deltaTime));
	}
}
