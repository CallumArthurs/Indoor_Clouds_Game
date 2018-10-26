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

    private bool _placingTurret;
    private bool _placingPowerPlant;
    private GameObject _curBuilding;
    private Ray _ray;
    private RaycastHit _mousePos;

    void Start () {

    }

    void Update() {
        if (_curBuilding != null)
        {
            UpdateBuildCosts();
            if (_placingTurret)
            {
                if (_placingTurret && Input.GetMouseButtonDown(0))
                {
                    Turret _curTurretScript = _curBuilding.GetComponentInChildren<Turret>();
                    resourceManager.ChangeMoney(-Building.cost[_curTurretScript.turretID]);
                    resourceManager.ChangeElectricity(-_curTurretScript.electricityCost);
                    _curTurretScript.activate = true;
                    resourceManager.turrets.Add(_curTurretScript);
                    _curBuilding = null;
                    _placingTurret = false;
                    return;
                }
                else if (_placingTurret && Input.GetMouseButton(1))
                {
                    Destroy(_curBuilding);
                    _curBuilding = null;
                    _placingTurret = false;
                    return;
                }
            }
            else if (_placingPowerPlant)
            {
                if (_placingPowerPlant && Input.GetMouseButtonDown(0))
                {
                    PowerPlant _curPowerPlantScript = powerPlant.GetComponent<PowerPlant>();
                    resourceManager.ChangeElectricity(_curPowerPlantScript.generatedPower);
                    resourceManager.ChangeMoney(-PowerPlant.cost);
                    _curBuilding = null;
                    _placingPowerPlant = false;
                    return;
                }
                else if (_placingTurret && Input.GetMouseButton(1))
                {
                    Destroy(_curBuilding);
                    _curBuilding = null;
                    _placingPowerPlant = false;
                    return;
                }
            }
            _ray = curCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(_ray, out _mousePos))
            {
                _curBuilding.transform.position = _mousePos.point;
                _curBuilding.transform.Translate(new Vector3(0,0.5f,0));
            }
        }
        
    }

    public void TurretCreate(int turretID)
    {
        if (Turret.cost[turretID] < resourceManager.money)
        {
            _curBuilding = Instantiate(turrets[turretID], new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
            _placingTurret = true;
            _curBuilding.layer = 0;
        }
    }
    public void PowerPlantCreate()
    {
        if (PowerPlant.cost < resourceManager.money)
        {
            _curBuilding = Instantiate(powerPlant, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
            _placingPowerPlant = true;
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
