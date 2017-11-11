using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour {
    public float maxSize = 3;
    [Range(0f, 100f)]
    public float growthSpeed = 8;
    public Renderer rend;
    public int water = 0;

    private float growthSpeedOriginal;
    private float growthSpeedChanged;
    private Vector3 growthSpeedVec;
    private Vector3 startSize;
    private bool growing = true;
    private bool fullgrown = false;
    private bool waterbonusbool = false;


	// Use this for initialization
	void Start () {
        startSize = gameObject.transform.localScale;
        growthSpeedVec = SetGrowRate(growthSpeed);
        growthSpeedChanged = growthSpeed;
        growthSpeedOriginal = growthSpeed;
	}
	
	// Update is called once per frame
	void Update () {
        waterBonus();
        Growing();

	}

    //Should be made in a own class. Could be good for reused code.
    private void Growing()
    {
        //the growing speed is affected when changed. 
        if (growthSpeed != growthSpeedChanged)
        {
            growthSpeedVec = SetGrowRate(growthSpeed);
            growthSpeedChanged = growthSpeed;
        }

        if (growing && !fullgrown)
        {
            if (gameObject.transform.localScale.x < maxSize)
            gameObject.transform.localScale += growthSpeedVec * Time.deltaTime;
        }


    }

    public void waterBonus()
    {
        if (water > 60 && !waterbonusbool)
        {
            growthSpeed = growthSpeedOriginal + 10;
        }
        else if (water > 40)
        {
            growthSpeed = growthSpeedOriginal;
            waterbonusbool = false;
        }
        else if (water < 40)
            growthSpeed = 0;
        else if (water < 10)
        {
            rend.material.color = Color.gray;
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
