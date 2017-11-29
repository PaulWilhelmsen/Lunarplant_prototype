using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{

    public GameObject selectedTile;
    public GameObject airShip;
    public static bool tileSelected;
    public GameObject plantPrefab; //The plants that can be planted.
    private GameObject plantObject; //Every plant started at the same size it the plant before it have reached. So i needed a plant object
    private SeedManager SM;

    // Use this for initialization
    void Start()
    {
        SM = GetComponent<SeedManager>();
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
            if (SM.getGreenSeed() > 0)
            {
                //make a copy of the prefab over the selected tile
                plantObject = Instantiate(plantPrefab, selectedTile.transform.position, Quaternion.identity) as GameObject;
                plantObject.transform.SetParent(selectedTile.transform, true);
                plantObject.transform.rotation = plantObject.transform.parent.rotation;
                selectedTile.GetComponent<TileMouseOver>().hasPlant = true;
                SM.subSeed((int)Plant.PlantType.GREEN, 1);
            }

        }
    }

    public void waterTile() //Waters the tile.
    {
        selectedTile.GetComponent<TileMouseOver>().tileWatered();
    }

    public void harvestPlant()
    {
        if (selectedTile.GetComponent<TileMouseOver>().harvestable)
        {
            selectedTile.GetComponentInChildren<Plant>().HarvestPlant();
            SM.addSeed((int)Plant.PlantType.GREEN, 2);
        }

    }

    public void WaterandSeedEverything()
    {
        TileMouseOver[] targets = FindObjectsOfType(typeof(TileMouseOver)) as TileMouseOver[];
        SM.addSeed((int)Plant.PlantType.GREEN, targets.Length);
        foreach (TileMouseOver tile in targets)
        {
            selectedTile = tile.gameObject;
            plantPlant();
            tile.tileWatered();
        }
    }

    public void WaterAllTiles()
    {
        TileMouseOver[] targets = FindObjectsOfType(typeof(TileMouseOver)) as TileMouseOver[];
        foreach (TileMouseOver tile in targets)
        {
            selectedTile = tile.gameObject;
            tile.tileWatered();
        }
    }

    public void spawnParticleFromTile()
    {
        GameObject lol = FindObjectOfType<PathScript>().gameObject;
        lol.GetComponent<PathScript>().newPath(selectedTile, airShip);
    }
}