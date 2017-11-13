using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public float maxSize = 3;
    [Range(0f, 100f)]
    public float growthSpeed = 8;
    public Renderer rend;
    public float water = 0;
    public float waterUsage = 2;
    public float waterGain = 5;
    public ParticleSystem fullGrownParticleSystem;

    private float growthSpeedOriginal;
    private float growthSpeedChanged;
    private Vector3 growthSpeedVec;
    private Vector3 startSize;
    private bool growing = true;
    private bool fullgrown = false;
    private bool fullGrownParticleBool= false;
    private bool waterbonusbool = false;


    // Use this for initialization
    void Start()
    {
        startSize = gameObject.transform.localScale;
        growthSpeedVec = SetGrowRate(growthSpeed);
        growthSpeedChanged = growthSpeed;
        growthSpeedOriginal = growthSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        //It can be harvested
        if (!fullgrown)
        {
            //drain water if tile is not watered.
            if (!gameObject.GetComponentInParent<TileMouseOver>().watered)
            {
                waterDrain();
            }
            else
            {
                if (water < 100)
                {
                    water += waterGain * Time.deltaTime;
                }
            }
            waterBonus();//Gives a growrate bonus if it has enough water, also kills the plant if it looses all its power.
            Growing();//Resizes the plant
        }
        else
        {
            //Creates a particle system when the plant is fullgrown. its to give feedback that this shit is ripe for plucking bitch. 
            if(!fullGrownParticleBool)
            {
                ParticleSystem ps = Instantiate(fullGrownParticleSystem, gameObject.transform.position, Quaternion.identity) as ParticleSystem;
                ps.transform.Rotate(-90, 0, 0);
                ps.transform.SetParent(gameObject.transform);
                ps.transform.localScale = Vector3.one * 5;  //Since parent resizes the child, I used this horrible hack to resize to a size i wanted.
                fullGrownParticleBool = true;
            }

        }

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

        if (growing && !fullgrown)  //fullgrown isnt really needed anymore here
        {
            if (gameObject.transform.localScale.x < maxSize)
                gameObject.transform.localScale += growthSpeedVec * Time.deltaTime;
        }
        if(gameObject.transform.localScale.x >= maxSize)    //if its fullgrown its ready for harvest.
        {
            fullgrown = true;
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
        else if (water < 40 && water > 10)
            growthSpeed = 0;
        else if (water < 10)
        {
            rend.material.color = Color.gray;
        }
    }

    public void setWaterUsage(float _waterUsage)
    {
        waterUsage = _waterUsage;
    }

    public void waterDrain()
    {
        water -= waterUsage * Time.deltaTime;
    }

    //Returns a vector with grow rate of the plant.
    public Vector3 SetGrowRate(float growRate)
    {
        float growRateSlowed = growRate / 10;
        Vector3 v = new Vector3(growRateSlowed, growRateSlowed, growRateSlowed);
        return v;
    }


}
