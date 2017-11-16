using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Mutate : MonoBehaviour {

    public GameObject[] mutationsPrefabs;
    public float[] mutationChance;
    private GameObject mutationObject;
    //public ParticleSystem plantMutated;
    //public ParticleSystem plantDied;

    void Awake()
    {
        //Gives a warning if a field is not filled.

        //Assert.IsNotNull(plantMutated);
        //Assert.IsNotNull(plantDied);
        Assert.IsNotNull(mutationsPrefabs);
        Assert.IsNotNull(mutationObject);
    }


    public void TryToMutate()
    {
        float roll = MutationRoll();    //Rolls a random number between 0-100 and then tweets about it.

        if (roll >= 100 - mutationChance[0])  //Checks if the mutation is higher than the mutation number. 
            startMutation();
        else
            GetComponent<Plant>().killPlant(5);
    }

    private void startMutation()
    {
        mutationObject = Instantiate(mutationsPrefabs[0], transform.parent.position, Quaternion.identity);
        mutationObject.transform.SetParent(transform.parent);
        GetComponent<Plant>().killPlant(0);
        
    }

    float MutationRoll()
    {
        float D100D10 = UnityEngine.Random.Range(0f, 100f); //Complained because it didnt know if i wanted system or unityengine Random value. 
        Debug.Log("Mutation Roll: " + D100D10);
        return D100D10;
    }
}
