using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{

    public GameObject selectedTile;
    public static bool tileSelected;
    public GameObject plant; //The plants that can be planted.

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //To be able to plant seeds etc. 
        if (selectedTile != null)
        {
            tileSelected = true;
        }
    }

    public void plantPlant()
    {
        if (!selectedTile.GetComponent<TileMouseOver>().hasPlant)
        {
            Instantiate(plant, selectedTile.transform.position, Quaternion.identity);
            selectedTile.GetComponent<TileMouseOver>().hasPlant = true;
        }
    }
}