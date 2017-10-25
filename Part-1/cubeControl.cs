using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeControl : MonoBehaviour {


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseDown()
    {
        this.gameObject.GetComponent<Renderer>().material.color = Color.red;

        if (GameControl.oldCube != null)
        {
            GameControl.oldCube.GetComponent<Renderer>().material.color = Color.white;
        }
        
        GameControl.oldCube = this.gameObject;

    }
}
