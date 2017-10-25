using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public GameObject mainCube;
    public GameObject currentCube;
    public static GameObject oldCube;
    Vector3 cubePosition;

    // Use this for initialization
    void Start()
    {
        //spawns cubes
            for (int x = -11; x < 20; x=x+2)
            {
                cubePosition = new Vector3(x, 1, 0);
                Instantiate(mainCube, cubePosition, Quaternion.identity);
            }

    }



    public static void ProcessClick(GameObject currentCube)
    {

    }

    

    // Update is called once per frame
    void Update()
    {

    }
}
