using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour {
    public float maxSize = 3;
    [Range(0f, 100f)]
    public float growthSpeed = 8;
    private float growthSpeedChanged;

    private Vector3 growthSpeedVec;
    private Vector3 startSize;
    private bool growing = true;
    private bool fullgrown = false;
    private int water = 0;

	// Use this for initialization
	void Start () {
        startSize = gameObject.transform.localScale;
        growthSpeedVec = SetGrowRate(growthSpeed);
        growthSpeedChanged = growthSpeed;
	}
	
	// Update is called once per frame
	void Update () {
        Growing();
	}

    //Should be made in a own class. Could be good for reused code.
    private void Growing()
    {
        if(growing && !fullgrown)
        {
            if (gameObject.transform.localScale.x < maxSize)
            gameObject.transform.localScale += growthSpeedVec * Time.deltaTime;
        }

        //the growing speed is affected when changed. 
        if(growthSpeed != growthSpeedChanged)
        {
            growthSpeedVec = SetGrowRate(growthSpeed);
            growthSpeedChanged = growthSpeed;
        }
    }

    //Returns a vector with grow rate of the plant.
    public Vector3 SetGrowRate(float growRate)
    {
        float growRateSlowed = growRate/10;
        Vector3 v = new Vector3(growRateSlowed, growRateSlowed, growRateSlowed);
        return v;
    }
}
