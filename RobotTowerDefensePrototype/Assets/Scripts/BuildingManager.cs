using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour {
    public GameObject[] turrets = null;
    public Camera curCamera = null;
    public ResourceManager resourceManager = null;

    private bool _placingTurret;
    private GameObject _curTurret;
    private Ray _ray;
    private RaycastHit _mousePos;

	void Start () {
		
	}

    void Update() {
        if (_curTurret != null)
        {
            if (_placingTurret && Input.GetMouseButtonDown(0))
            {
                _curTurret.transform.Translate(new Vector3(0, 0.5f, 0));
                resourceManager.ChangeMoney(-50);
                resourceManager.ChangeElectricity(-_curTurret.GetComponentInChildren<Turret>().electricityCost);
                _curTurret.GetComponentInChildren<Turret>().Activate = true;
                _curTurret = null;
                _placingTurret = false;
            }
            else if(_placingTurret && Input.GetMouseButton(1))
            {
                Destroy(_curTurret);
                _curTurret = null;
                _placingTurret = false;
            }
            else
            {
                _ray = curCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(_ray, out _mousePos))
                {
                    _curTurret.transform.position = _mousePos.point;
                }
            }
        }
        
    }

    public void TurretCreate(int turretID)
    {
        if (Turret.cost[turretID] > resourceManager.money)
        {
            return;
        }
        else
        {
            _curTurret = Instantiate(turrets[turretID], new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
            _placingTurret = true;
        }
    }
}
