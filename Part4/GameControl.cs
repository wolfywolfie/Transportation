using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    public GameObject mainCube;
    public Text scoreText;
    Vector3 cubePosition;
    public static GameObject[,] cubes;

    int maxWidth;
    int maxHeight;
    public static int planeX, planeY;
    static bool activePlane;
    double gameTime;
    //starting location
    int planeHomeX;
    int planeHomeY;
    int cargo; //amount of cargo applied each turn
    int currentCargo; //amount of cargo on plane
    int maxCargo; //max amount of cargo plane can hold
    int playerScore;
    int dropOffX, dropOffY;
    public static int planeOldX, planeOldY; //previous position of plane

    // Use this for initialization
    void Start()
    {
        maxWidth = 16;
        maxHeight = 9;
        cubes = new GameObject[maxWidth, maxHeight];
        activePlane = false;
        gameTime = 1.5;

        planeHomeX = 0;
        planeHomeY = 8;
        playerScore = 0;
        cargo = 10;
        currentCargo = 0;
        maxCargo = 90;

        //spawns cubes
        for (int y = 0; y < maxHeight; y++)
        {
            for (int x = 0; x < maxWidth; x++)
            {
                cubePosition = new Vector3(x*6, y*6, 0);
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

        //delivery depot positions
        dropOffX = 15;
        dropOffY = 0;
        cubes[dropOffX, dropOffY].GetComponent<Renderer>().material.color = Color.black;
    }

    public static void ProcessClick(GameObject clickedCube, int x, int y)
    {
        //selects plane
        if (x == planeX && y == planeY)
        {
            if (!activePlane) //activates plane
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
    }

    public void planeMove ()
    {
            if (planeY > 0 && Input.GetButtonDown("moveDown"))
            {
                //airplane moves down one square
                planeOldY = planeY;
                planeOldX = planeX;
                planeY--;
            cubes[planeOldX,planeOldY].transform.localScale /= 1.5f;
            cubes[planeX, planeY].transform.localScale *= 1.5f;
            return;
            }
            else if (GameControl.planeY < 8 && Input.GetButtonDown("moveUp"))
            {
                //airplane moves up one square
                planeOldY = planeY;
                planeOldX = planeX;
                planeY++;
            cubes[planeOldX, planeOldY].transform.localScale /= 1.5f;
            cubes[planeX, planeY].transform.localScale *= 1.5f;
            return;
        }
            if (planeX > 0 && Input.GetButtonDown("moveLeft"))
            {
                //airplane moves left one square
                planeOldX = planeX;
                planeOldY = planeY;
                planeX--;
            cubes[planeOldX, planeOldY].transform.localScale /= 1.5f;
            cubes[planeX, planeY].transform.localScale *= 1.5f;
            return;
        }
            else if (planeX < 15 && Input.GetButtonDown("moveRight"))
            {
                //airplane moves right one square
                planeOldX = planeX;
                planeOldY = planeY;
                planeX++;
            cubes[planeOldX, planeOldY].transform.localScale /= 1.5f;
            cubes[planeX, planeY].transform.localScale *= 1.5f;
            return;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (Time.time >= gameTime)
        {
            gameTime += 1.5;

            if (planeX == planeHomeX && planeY == planeHomeY)
            {
                //add 10 cargo to the plane if below 90
                if (currentCargo < maxCargo)
                {
                    currentCargo += cargo;
                }

            }

            if (planeX == dropOffX && planeY == dropOffY)
            {
                if (currentCargo != 0)
                {
                    playerScore = currentCargo;

                    //remove cargo from plane
                    currentCargo -= currentCargo;
                }
            }

            scoreText.text = "Current Cargo: " + currentCargo + System.Environment.NewLine +
                System.Environment.NewLine + "Current Score: " + playerScore;      
        }
        if (activePlane)
        {
            planeMove();
            cubes[planeOldX, planeOldY].GetComponent<Renderer>().material.color = Color.white;
            cubes[planeX, planeY].GetComponent<Renderer>().material.color = Color.red;
        }
        
        cubes[dropOffX, dropOffY].GetComponent<Renderer>().material.color = Color.black;
        
    } 
}