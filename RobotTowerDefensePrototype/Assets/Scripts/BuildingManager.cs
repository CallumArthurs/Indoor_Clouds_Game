using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour {
    public GameObject[] turrets;
    public Camera curCamera;

    private bool placingTurret;
    private GameObject curTurret;
    private Ray ray;
    private RaycastHit mousePos;

	void Start () {
		
	}

    void Update() {
        if (curTurret != null)
        {
            if (placingTurret && Input.GetMouseButtonDown(0))
            {
                curTurret.transform.Translate(new Vector3(0, 0.5f, 0));
                curTurret = null;
                placingTurret = false;
            }
            else if(placingTurret && Input.GetMouseButton(1))
            {

            }
            else
            {
                ray = curCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out mousePos))
                {
                    curTurret.transform.position = mousePos.point;
                }
            }
        }
        
    }

    public void TurretCreate(int turretID)
    {
        curTurret = Instantiate(turrets[turretID], new Vector3(0,0,0), new Quaternion(0,0,0,0));
        placingTurret = true;
    }
}
