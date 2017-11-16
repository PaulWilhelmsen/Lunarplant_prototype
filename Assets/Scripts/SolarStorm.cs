using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarStorm : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void InvokeSolarStorm()
    {

        Mutate[] targets = FindObjectsOfType(typeof(Mutate)) as Mutate[];
        foreach(Mutate plant in targets)
        {
            plant.TryToMutate();
        }
    }
}
