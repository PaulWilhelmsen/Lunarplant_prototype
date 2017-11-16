using System;
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

    protected float growthSpeedOriginal;
    protected float growthSpeedChanged;
    protected Vector3 growthSpeedVec;
    protected Vector3 startSize;
    protected bool growing = true;
    protected bool fullgrown = false;
    protected bool fullGrownParticleBool = false;
    protected bool waterbonusbool = false;
    protected bool died;


    // Use this for initialization
    protected void Start()
    {
        startSize = gameObject.transform.localScale;
        growthSpeedVec = SetGrowRate(growthSpeed);
        growthSpeedChanged = growthSpeed;
        growthSpeedOriginal = growthSpeed;
    }

    // Update is called once per frame
    protected void Update()
    {
        //It can be harvested
        if (!fullgrown)
        {
            TileWatered(); //drain water if tile is not watered.
            waterBonus();//Gives a growrate bonus if it has enough water, also kills the plant if it looses all its power.
            Growing();//Resizes the plant
        }
        else
        {
            //Creates a particle system when the plant is fullgrown. its to give feedback that this shit is ripe for plucking bitch. 
            if (!fullGrownParticleBool)
            {
                spawnGrownParticles();   //Spawns the particle system for a fullgrown plant.
            }
        }
    }

    //checks if the tile the plant is watered on is used.
    private void TileWatered()
    {
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
    }

    //Adds a bonus for if the plant have a high waterlevel.
    protected virtual void waterBonus()
    {
        if (water > 60 && !waterbonusbool)  //Gains a growthspeed of +10
        {
            growthSpeed = growthSpeedOriginal + 10;
        }
        else if (water > 40)    //Grows in a normal rate
        {
            growthSpeed = growthSpeedOriginal;
            waterbonusbool = false;
        }
        else if (water < 40 && water > 10)  //stop growing
            growthSpeed = 0;
        else if (water < 10)    //Plant dies
        {
            if (!died)
            {
                killPlant(5);   //Plant turns gray and is removed in x seconds.
            }

        }
    }


    //Should be made in a own class. Could be good for reused code.
    protected virtual void Growing()
    {
        //the growing speed is affected when changed. 
        if (growthSpeed != growthSpeedChanged)
        {
            growthSpeedVec = SetGrowRate(growthSpeed);
            growthSpeedChanged = growthSpeed;
        }

        if (growing && !fullgrown)  //plant is growing. fullgrown isnt really needed anymore here
        {
            if (gameObject.transform.localScale.x < maxSize)
                gameObject.transform.localScale += growthSpeedVec * Time.deltaTime;
        }
        if (gameObject.transform.localScale.x >= maxSize)    //if its fullgrown its ready for harvest.
        {
            fullgrown = true;
        }
    }


    //The amount a water a plant uses.
    public virtual void waterDrain()
    {
        water -= waterUsage * Time.deltaTime;
    }

    //Returns a vector with grow rate of the plant.
    public virtual Vector3 SetGrowRate(float growRate)
    {
        float growRateSlowed = growRate / 10;
        Vector3 v = new Vector3(growRateSlowed, growRateSlowed, growRateSlowed);
        return v;
    }

    //Spawns particles for a fullgrown plant ^^
    protected virtual void spawnGrownParticles()
    {
        ParticleSystem ps = Instantiate(fullGrownParticleSystem, gameObject.transform.position, Quaternion.identity) as ParticleSystem;
        ps.transform.Rotate(-90, 0, 0);
        ps.transform.SetParent(gameObject.transform);
        ps.transform.localScale = Vector3.one * 5;  //Since parent resizes the child, I used this horrible hack to resize to a size i wanted.
        fullGrownParticleBool = true;
    }

    //not actually used, but if another class wants to change the
    //waterusage a plant have. Add original waterUsage if needed.
    public virtual void setWaterUsage(float _waterUsage)
    {
        waterUsage = _waterUsage;
    }

    public void killPlant(float timeToKillPlant)    //Kills a plant and removes it in x seconds
    {
        StartCoroutine(PlantDying(timeToKillPlant));
    }

    public IEnumerator PlantDying(float waitTime)   //plants turn gray, tile can be replanted and plant is removed
    {
        while (true)
        {
            rend.material.color = Color.gray;
            died = true;
            yield return new WaitForSeconds(waitTime);
            gameObject.GetComponentInParent<TileMouseOver>().hasPlant = false;  //tile can be replanted.
            Destroy(gameObject);
        }
    }
}