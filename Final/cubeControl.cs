using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeControl : MonoBehaviour
{
    GameControl myGameController;
    public GameObject noisePlane;
    public GameObject boatNoise;
    public GameObject trainNoise;
    public int myX, myY;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        if (myX == GameControl.planeX && myY == GameControl.planeY)
        {
            Instantiate(noisePlane);
        }
        if (myX == GameControl.boatX && myY == GameControl.boatY)
        {
            Instantiate(boatNoise);
        }
        if (myX == GameControl.trainX && myY == GameControl.trainY)
        {
            Instantiate(trainNoise);
        }
        GameControl.ProcessClick(gameObject, myX, myY);
    }
}
