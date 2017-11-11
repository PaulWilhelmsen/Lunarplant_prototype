using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{

    public GameObject selectedTile;
    public static bool tileSelected;
    public GameObject plantPrefab; //The plants that can be planted.
    private GameObject plantObject; //Every plant started at the same size it the plant before it have reached. So i needed a plant object

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
            //make a copy of the prefab over the selected tile
            plantObject = Instantiate(plantPrefab, selectedTile.transform.position, Quaternion.identity) as GameObject;
            plantObject.transform.SetParent(selectedTile.transform, true);
            selectedTile.GetComponent<TileMouseOver>().hasPlant = true;
        }
    }

    public void waterTile()
    {
        selectedTile.GetComponent<TileMouseOver>().tileWatered();
    }
}