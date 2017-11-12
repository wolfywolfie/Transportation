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
    public GameObject[,] cubes;
    public GameObject depotNoise;

    int maxWidth;
    int maxHeight;
    public static int planeX, planeY;
    public static int boatX, boatY;
    public static int trainX, trainY;
    static bool activePlane;
    static bool activeBoat;
    static bool activeTrain;
    double gameTime;
    double planeTime;
    double boatTime;
    double trainTime;
    //starting location
    int planeHomeX, planeHomeY;
    int boatHomeX, boatHomeY;
    int trainHomeX, trainHomeY;
    int cargo; //amount of cargo applied each turn
    public int[] currentCargo; //amount of cargo on vehicle
    public int[] maxCargo; //max amount of cargo vehicle can hold
    int playerScore;
    int dropOffX, dropOffY;
    public static int planeOldX, planeOldY; //previous position of plane
    public static int boatOldX, boatOldY; //previous position of boat
    public static int trainOldX, trainOldY; //previous position of train
    public static int newX, newY; //new plane location
    public static int boatNewX, boatNewY; //new boat location
    public static int trainNewX, trainNewY;

    // Use this for initialization
    void Start()
    {
        maxWidth = 16;
        maxHeight = 9;
        cubes = new GameObject[maxWidth, maxHeight];
        activePlane = false;
        activeBoat = false;
        activeTrain = false;
        gameTime = 1.5;
        planeTime = 1.5;
        boatTime = 4.5;
        trainTime = 3;

        planeHomeX = 0;
        planeHomeY = 8;
        boatHomeX = 15;
        boatHomeY = 8;
        trainHomeX = 1;
        trainHomeY = 0;
        playerScore = 0;
        cargo = 10;
        currentCargo = new int[3];
        maxCargo = new int[3];
        //zero is plane cargo, one is boat cargo, two is train cargo
        maxCargo[0] = 90;
        maxCargo[1] = 550;
        maxCargo[2] = 200;

        //spawns cubes
        for (int y = 0; y < maxHeight; y++)
        {
            for (int x = 0; x < maxWidth; x++)
            {
                cubePosition = new Vector3(x * 6, y * 6, 0);
                cubes[x, y] = Instantiate(mainCube, cubePosition, Quaternion.identity);
                cubes[x, y].GetComponent<cubeControl>().myX = x;
                cubes[x, y].GetComponent<cubeControl>().myY = y;
            }
        }

        //plane start positions
        planeX = 0;
        planeY = 8;
        cubes[planeX, planeY].GetComponent<Renderer>().material.color = Color.red;

        //boat start positions
        boatX = 15;
        boatY = 8;
        cubes[boatX, boatY].GetComponent<Renderer>().material.color = Color.blue;

        //delivery depot positions
        dropOffX = 15;
        dropOffY = 0;
        cubes[dropOffX, dropOffY].GetComponent<Renderer>().material.color = Color.black;

        //train start positions
        trainX = 1;
        trainY = 0;
        cubes[trainX, trainY].GetComponent<Renderer>().material.color = Color.green;
    }

    public static void ProcessClick(GameObject clickedCube, int x, int y)
    {
        //selects plane
        if (!activeBoat && !activeTrain)
        {
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
            if (activePlane)
            {
                newX = x;
                newY = y;
            }
        }

        //selects boat
        if (!activePlane && !activeTrain)
        {
            if (x == boatX && y == boatY)
            {
                if (!activeBoat) //activates boat
                {
                    clickedCube.transform.localScale *= 1.5f;
                    activeBoat = true;
                }
                else //deactivates boat
                {
                    clickedCube.transform.localScale /= 1.5f;
                    activeBoat = false;
                }
            }
            if (activeBoat)
            {
                boatNewX = x;
                boatNewY = y;
            }
        }

        //selects train
        if (!activeBoat && !activePlane)
        {
            if (x == trainX && y == trainY)
            {
                if (!activeTrain) //activates train
                {
                    clickedCube.transform.localScale *= 1.5f;
                    activeTrain = true;
                }
                else //deactivates train
                {
                    clickedCube.transform.localScale /= 1.5f;
                    activeTrain = false;
                }
            }
            if (activeTrain)
            {
                trainNewX = x;
                trainNewY = y;
            }
        }
    }

    public void PlaneMove()
    {
        if (newX > planeX)
        { //moves plane forward
            planeOldX = planeX;
            planeOldY = planeY;
            planeX++;
            cubes[planeOldX, planeOldY].transform.localScale /= 1.5f;
            cubes[planeX, planeY].transform.localScale *= 1.5f;
            return;
        }
        else if (newX < planeX)
        {
            //moves plane backward
            planeOldX = planeX;
            planeOldY = planeY;
            planeX--;
            cubes[planeOldX, planeOldY].transform.localScale /= 1.5f;
            cubes[planeX, planeY].transform.localScale *= 1.5f;
            return;
        }
        if (newY > planeY)
        { //moves plane up
            planeOldX = planeX;
            planeOldY = planeY;
            planeY++;
            cubes[planeOldX, planeOldY].transform.localScale /= 1.5f;
            cubes[planeX, planeY].transform.localScale *= 1.5f;
            return;
        }
        else if (newY < planeY)
        {
            //moves plane down
            planeOldX = planeX;
            planeOldY = planeY;
            planeY--;
            cubes[planeOldX, planeOldY].transform.localScale /= 1.5f;
            cubes[planeX, planeY].transform.localScale *= 1.5f;
            return;
        }
    }

    public void BoatMove()
    {
        if (boatNewX > boatX)
        { //moves boat forward
            boatOldX = boatX;
            boatOldY = boatY;
            boatX++;
            cubes[boatOldX, boatOldY].transform.localScale /= 1.5f;
            cubes[boatX, boatY].transform.localScale *= 1.5f;
            return;
        }
        else if (boatNewX < boatX)
        {
            //moves boat backward
            boatOldX = boatX;
            boatOldY = boatY;
            boatX--;
            cubes[boatOldX, boatOldY].transform.localScale /= 1.5f;
            cubes[boatX, boatY].transform.localScale *= 1.5f;
            return;
        }
        if (boatNewY > boatY)
        { //moves boat up
            boatOldX = boatX;
            boatOldY = boatY;
            boatY++;
            cubes[boatOldX, boatOldY].transform.localScale /= 1.5f;
            cubes[boatX, boatY].transform.localScale *= 1.5f;
            return;
        }
        else if (boatNewY < boatY)
        {
            //moves plane down
            boatOldX = boatX;
            boatOldY = boatY;
            boatY--;
            cubes[boatOldX, boatOldY].transform.localScale /= 1.5f;
            cubes[boatX, boatY].transform.localScale *= 1.5f;
            return;
        }
    }

    public void TrainMove()
    {
        if (trainNewX > trainX)
        { //moves train forward
            trainOldX = trainX;
            trainOldY = trainY;
            trainX++;
            cubes[trainOldX, trainOldY].transform.localScale /= 1.5f;
            cubes[trainX, trainY].transform.localScale *= 1.5f;
            return;
        }
        else if (trainNewX < trainX)
        {
            //moves train backward
            trainOldX = trainX;
            trainOldY = trainY;
            trainX--;
            cubes[trainOldX, trainOldY].transform.localScale /= 1.5f;
            cubes[trainX, trainY].transform.localScale *= 1.5f;
            return;
        }
        if (trainNewY > trainY)
        { //moves train up
            trainOldX = trainX;
            trainOldY = trainY;
            trainY++;
            cubes[trainOldX, trainOldY].transform.localScale /= 1.5f;
            cubes[trainX, trainY].transform.localScale *= 1.5f;
            return;
        }
        else if (trainNewY < trainY)
        {
            //moves train down
            trainOldX = trainX;
            trainOldY = trainY;
            trainY--;
            cubes[trainOldX, trainOldY].transform.localScale /= 1.5f;
            cubes[trainX, trainY].transform.localScale *= 1.5f;
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {

        //plane cargo
        if (Time.time >= planeTime)
        {
            planeTime += 1.5;

            if (planeX == planeHomeX && planeY == planeHomeY)
            {
                //add 10 cargo to the plane if below 90
                if (currentCargo[0] < maxCargo[0])
                {
                    currentCargo[0] = cargo + currentCargo[0];
                }

            }

            if (planeX == dropOffX && planeY == dropOffY)
            {
                if (currentCargo[0] != 0)
                {
                    Instantiate(depotNoise);
                    playerScore = currentCargo[0];

                    //remove cargo from plane
                    currentCargo[0] -= currentCargo[0];
                }
            }
            if (activePlane)
            {
                PlaneMove();
            }


        }
        //boat cargo
        if (Time.time >= boatTime)
        {
            boatTime += 4.5;

            if (boatX == boatHomeX && boatY == boatHomeY)
            {
                //add 10 cargo to the boat if below 90
                if (currentCargo[1] < maxCargo[1])
                {
                    currentCargo[1] += cargo;
                }

            }

            if (boatX == dropOffX && boatY == dropOffY)
            {
                if (currentCargo[1] != 0)
                {
                    Instantiate(depotNoise);
                    playerScore = currentCargo[1];

                    //remove cargo from boat
                    currentCargo[1] -= currentCargo[1];
                }
            }

            if (activeBoat)
            {
                BoatMove();
            }
        }

        //train cargo
        if (Time.time >= trainTime)
        {
            trainTime += 3;

            if (trainX == trainHomeX && trainY == trainHomeY)
            {
                //add 10 cargo to the train if below 90
                if (currentCargo[2] < maxCargo[2])
                {
                    currentCargo[2] += cargo;
                }

            }

            if (trainX == dropOffX && trainY == dropOffY)
            {
                if (currentCargo[2] != 0)
                {
                    Instantiate(depotNoise);
                    playerScore = currentCargo[2];

                    //remove cargo from train
                    currentCargo[2] -= currentCargo[2];
                }
            }

            if (activeTrain)
            {
                TrainMove();
            }
        }


        //stat updates
        if (Time.time >= gameTime)
        {
            gameTime += 1.5;

            scoreText.text = "Plane Cargo: " + currentCargo[0] + System.Environment.NewLine +
                System.Environment.NewLine + "Boat Cargo: " + currentCargo[1] + System.Environment.NewLine +
                System.Environment.NewLine + "Train Cargo: " + currentCargo[2] + System.Environment.NewLine +
                System.Environment.NewLine + "Score: " + playerScore;
        }


            cubes[planeX, planeY].GetComponent<Renderer>().material.color = Color.red;
            cubes[planeOldX, planeOldY].GetComponent<Renderer>().material.color = Color.white;

            cubes[boatX, boatY].GetComponent<Renderer>().material.color = Color.blue;
            cubes[boatOldX, boatOldY].GetComponent<Renderer>().material.color = Color.white;

            cubes[trainX, trainY].GetComponent<Renderer>().material.color = Color.green;
            cubes[trainOldX, trainOldY].GetComponent<Renderer>().material.color = Color.white;

            cubes[dropOffX, dropOffY].GetComponent<Renderer>().material.color = Color.black;


    }
}