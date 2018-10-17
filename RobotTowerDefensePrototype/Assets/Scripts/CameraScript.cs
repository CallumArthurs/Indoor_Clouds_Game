using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {
    Transform CamTransform;
    public GameObject target;
    bool option = true;
	void Start () {
        CamTransform = GetComponent<Transform>();

        CamTransform.transform.position = new Vector3(-4, 9, -14);
        CamTransform.rotation = new Quaternion(20, 0, 0, 0);

    }

    void Update () {
        if (Input.GetKeyDown("q"))
        {
            option = !option;
            if (option)
            {
                //looking at HQ
                CamTransform.transform.position = new Vector3(-4, 9, -14);
            }
            else
            {
                //on top of HQ
                CamTransform.transform.position = new Vector3(target.transform.position.x, target.transform.position.y + 5f, target.transform.position.z);
                CamTransform.Rotate(new Vector3(30.0f, 0));
            }
        }

        if (option)
        {
            CamControls1();
        }
        else {
            CamControls2();
        }
    }
    void CamControls1()
    {
        CamTransform.Translate(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        CamTransform.LookAt(target.transform);
    }
    void CamControls2()
    {
        CamTransform.Rotate(new Vector3(0, Input.GetAxis("Horizontal"),0),Space.World);
    }

}
