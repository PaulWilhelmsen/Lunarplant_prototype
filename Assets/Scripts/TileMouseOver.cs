using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Would be nice to just call it Tile, since it actually holds all the tile variables as well.
public class TileMouseOver : MonoBehaviour
{
    public Color highLightColor;
    public Camera cam;

    public bool hasPlant = false;
    public bool harvestable = false;
    public bool watered = false;
    public bool clicked;    //So the hover highlight color won't bother the selected highlight color. 
    private Color normalColor;
    private Color lerpedColor;
    [SerializeField]
    private float waterTimer;
    private Collider col;
    private Renderer r;

    // Use this for initialization
    void Start()
    {
        //To change the color of the tile
        r = GetComponent<Renderer>();
        //To use raycast
        cam = FindObjectOfType<Camera>();
        //To have a collider for the raycast
        col = GetComponent<Collider>();
        //yeah, normalcolor yeah!!
        normalColor = r.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        MouseInteractions();

        //If the tile is watered
        if(watered)
        {
            waterTimer -= Time.deltaTime;
            if (waterTimer < 0)
                watered = false;
        }

    }

    //Interact with the mouse. Hover over a tile, click on a tile.
    private void MouseInteractions()
    {
        //sends a ray from the camera to the object.
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        //To be able to highlight the tile we use a ray. This can go through objects since
        //it uses the specific collider from the tile

        //Highlights the 
        if (!clicked)
        {
            if (col.Raycast(ray, out hitInfo, Mathf.Infinity))
                r.material.color = highLightColor;
            else
            {
                if (watered)
                    r.material.color = Color.blue;
                else
                    r.material.color = normalColor;
            }

        }


        //Highlights a clicked object with color red. 
        if (Input.GetMouseButtonDown(0) && col.Raycast(ray, out hitInfo, Mathf.Infinity))
        {
            r.material.color = Color.red;
            clicked = true;

            //sets the selected gameobject in SceneManager, so its easy accesable if another script or nightmarefuel needs it. 
            GameObject.Find("SceneManager").GetComponent<SceneManager>().selectedTile = gameObject;
        }
        else if (Input.GetMouseButtonDown(0) && !col.Raycast(ray, out hitInfo, Mathf.Infinity))
        {
            r.material.color = normalColor;
            clicked = false;
        }


    }

    public void tileWatered()
    {
        watered = true;
        waterTimer = 10;
    }


    //---------------------------------------------------------TestCoded/ThrowAwayCode-------------------------------------------------

    //With mouseover a tile won't highlight if an object is on the tile
    //That is because the mouse will be over the object and not over a tile.
    //void OnMouseOver()
    //{
    //    r.material.color = highLightColor;
    //}

    //void OnMouseExit()
    //{
    //    r.material.color = normalColor;
    //} 

    //To smoothly change the color.. goes to fast so I need to research it.
    //But then again this is polish, not needed in a prototype
    //lerpedColor = Color.Lerp(Color.blue, Color.white, Mathf.PingPong(Time.time, 1));
    //r.material.color = lerpedColor;
    //Debug.Log(Mathf.Lerp(0, 1, 1));
}
