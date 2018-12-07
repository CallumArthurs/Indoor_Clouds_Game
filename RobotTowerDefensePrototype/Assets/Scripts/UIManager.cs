using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UImanager : MonoBehaviour {

    public GameObject cameraHolder;
//in the inspector, put the object containing the camera script in here
    public CameraMove cameraMover;
    // this is the cache for the camera mover script
    private int currentLookInt;
    //this is also a cache, but for the int from the camera mover script
    public GameObject govUI;
    public GameObject sciUI;
    public GameObject marUI;
    

	// Use this for initialization
	void Start () {
        cameraMover = cameraMover.GetComponent<CameraMove>();
        //this is where we do the cache
        
        
	}
	
	// Update is called once per frame
	void Update () {
		currentLookInt = cameraMover.currentLoc;
        //now we have the current location int from the script updating in this script every frame and we can do like, wever we please
        if (currentLookInt == 0)
        {
            govUI.SetActive(true);
        }
        else
        {
            govUI.SetActive(false);
        }

        if (currentLookInt == 1)
        {
            marUI.SetActive(true);
        }
        else
        {
            marUI.SetActive(false);
        }

        if (currentLookInt == 2)
        {
            sciUI.SetActive(true);
        }
        else
        {
            sciUI.SetActive(false);
        }

    }
}
