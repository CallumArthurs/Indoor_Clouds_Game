using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingManager : MonoBehaviour {
    public GameObject[] turrets = null;
    public GameObject powerPlant = null;
    public Camera curCamera = null;
    public ResourceManager resourceManager = null;
    public Text[] buildingUI = null;

    private enum enumBuildingID {turretID,powerPlantID};
    private bool _placingBuilding;
    private GameObject _curBuilding;
    private Ray _ray;
    private RaycastHit _mousePos;

    void Start () {

    }

    void Update() {
        if (_curBuilding != null)
        {
            UpdateBuildCosts();
            if (_placingBuilding && Input.GetMouseButtonDown(0))
            {
                Building _curBuildingScript = _curBuilding.GetComponentInChildren<Building>();
                resourceManager.ChangeMoney(-Building.cost[_curBuildingScript.buildingID]);
                resourceManager.ChangeElectricity(Building.electricityCost[_curBuildingScript.buildingID]);
                _curBuildingScript.active = true;

                if (_curBuildingScript.buildingID == (int)enumBuildingID.turretID)
                {
                    resourceManager.turrets.Add((Turret)_curBuildingScript);
                }

                _curBuilding = null;
                _placingBuilding = false;
                return;
            }
            else if (_placingBuilding && Input.GetMouseButton(1))
            {
                Destroy(_curBuilding);
                _curBuilding = null;
                _placingBuilding = false;
                return;
            }
            
            _ray = curCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(_ray, out _mousePos, Mathf.Infinity, ~(1 << LayerMask.NameToLayer("Saucer"))))
            {
                _curBuilding.transform.position = _mousePos.point;
                _curBuilding.transform.Translate(new Vector3(0,0.5f,0));
            }
        }
        
    }

    public void TurretCreate(int turretID)
    {
        if (Building.cost[turretID] < resourceManager.money)
        {
            _curBuilding = Instantiate(turrets[turretID], new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
            _curBuilding.GetComponentInChildren<Building>().buildingID = (int)enumBuildingID.turretID;
            _placingBuilding = true;
            _curBuilding.layer = 0;
        }
    }
    public void PowerPlantCreate()
    {
        if (Building.cost[0] < resourceManager.money)
        {
            _curBuilding = Instantiate(powerPlant, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
            _curBuilding.GetComponentInChildren<Building>().buildingID = (int)enumBuildingID.powerPlantID;
            _placingBuilding = true;
        }
    }
    public void UpdateBuildCosts()
    {
        for(int i = 0; i < buildingUI.Length; i++)
        {
            buildingUI[i].text ="Turret" + i.ToString() + " $" + Turret.cost[i];
        }
    }
}
