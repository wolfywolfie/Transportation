using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public GameObject mainCube;
    public static GameObject oldCube;
    public GameObject airplane;
    Vector3 cubePosition;
    public static GameObject[,] cubes;

    int maxWidth;
    int maxHeight;
    public static int planeX, planeY;
    static bool activePlane;

    // Use this for initialization
    void Start()
    {
        maxWidth = 16;
        maxHeight = 9;
        cubes = new GameObject[maxWidth, maxHeight];
        activePlane = false;

        //spawns cubes
        for (int y = 0; y < maxHeight; y++)
        {
            for (int x = 0; x < maxWidth; x++)
            {
                cubePosition = new Vector3(x*2, y*2, 0);
                cubes[x,y] = Instantiate(mainCube, cubePosition, Quaternion.identity);
                mainCube.GetComponent<Renderer>().sharedMaterial.color = Color.white;
                cubes[x, y].GetComponent<cubeControl>().myX = x;
                cubes[x, y].GetComponent<cubeControl>().myY = y;
            }   
        }
        //plane start positions
        planeX = 0;
        planeY = 8;
        cubes[planeX, planeY].GetComponent<Renderer>().material.color = Color.red;
    }

    public static void ProcessClick(GameObject clickedCube, int x, int y)
    {
        //selects plane
        if (x == planeX && y == planeY)
        {
            if (activePlane == false) //activates plane
            {
                clickedCube.transform.localScale *= 1.5f;
                activePlane = true;
            }
            else //deactivates plane
            {
                clickedCube.transform.localScale /= 1.5f;
                activePlane = false;
            }
        }
        else if (x != planeX && y != planeY)
        {
            if (activePlane == true)
            { //removes old plane
                cubes[planeX, planeY].GetComponent<Renderer>().material.color = Color.white;
                cubes[planeX, planeY].transform.localScale /= 1.5f;

                //moves plane
                planeX = x;
                planeY = y;
                cubes[x, y].GetComponent<Renderer>().material.color = Color.red;
                cubes[x, y].transform.localScale *= 1.5f;
            }
        }
    }
       
       //otherwise does nothing

    // Update is called once per frame
    void Update()
    {

    }
}
