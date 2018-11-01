using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingManager : MonoBehaviour {
    public GameObject[] turrets = null;
    public GameObject transmitter = null;
    public GameObject powerPlant = null;
    public Text[] buildingUI = null;
    public Camera curCamera = null;
    public ResourceManager resourceManager = null;

    private enum enumBuildingID {turretID,powerPlantID,TransmitterID};
    private bool _placingBuilding, _placingConnector;
    private GameObject _curBuilding;
    private Ray _ray;
    private RaycastHit _mousePos;
    private Building _connection;
    private Transmitter _connector;

    void Start () {

    }

    void Update() {
        if (_curBuilding != null && _placingBuilding)
        {
            BuildingControls();
        }
        else if(_placingConnector)
        {
            BuildingConnector();
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

        UpdateBuildCosts();
    }
    public void PowerPlantCreate()
    {
        if (Building.cost[0] < resourceManager.money)
        {
            _curBuilding = Instantiate(powerPlant, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
            _curBuilding.GetComponentInChildren<Building>().buildingID = (int)enumBuildingID.powerPlantID;
            _placingBuilding = true;
        }
        UpdateBuildCosts();
    }
    public void TransmittorCreate()
    {
        if (Building.cost[0] < resourceManager.money)
        {
            _curBuilding = Instantiate(transmitter, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
            _curBuilding.GetComponent<Building>().buildingID = (int)enumBuildingID.TransmitterID;
            _placingBuilding = true;
        }

        UpdateBuildCosts();
    }
    public void ConnectorCreate()
    {
        _placingConnector = true;
        UpdateBuildCosts();
    }
    public void UpdateBuildCosts()
    {
        for(int i = 0; i < buildingUI.Length; i++)
        {
            buildingUI[i].text ="Turret" + i.ToString() + " $" + Turret.cost[i];
        }
    }
    private void BuildingControls()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            Building _curBuildingScript = _curBuilding.GetComponentInChildren<Building>();
            resourceManager.ChangeMoney(-Building.cost[_curBuildingScript.buildingID]);
            resourceManager.ChangeElectricity(Building.electricityCost[_curBuildingScript.buildingID]);

            if (_curBuildingScript.buildingID == (int)enumBuildingID.turretID)
            {
                resourceManager.turrets.Add((Turret)_curBuildingScript);
            }

            _curBuilding = null;
            _placingBuilding = false;
            return;
        }
        else if (Input.GetMouseButton(1))
        {
            Destroy(_curBuilding);
            _curBuilding = null;
            _placingBuilding = false;
            return;
        }

        _ray = curCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(_ray, out _mousePos, Mathf.Infinity, ~(1 << LayerMask.NameToLayer("Building non-collidables"))))
        {
            _curBuilding.transform.position = _mousePos.point;
            _curBuilding.transform.Translate(new Vector3(0, 0.5f, 0));
        }
    }
    private void BuildingConnector()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _ray = curCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(_ray, out _mousePos))
            {
                if (_mousePos.collider.GetComponent<Transmitter>() != null)
                {
                    _connector = _mousePos.collider.GetComponent<Transmitter>();
                }
                else if (_mousePos.collider.GetComponent<Building>() != null)
                {
                    _connection = _mousePos.collider.GetComponent<Building>();
                }

                if (_connection != null && _connector != null)
                {
                    _connector.Connection(_connection);
                    _connection = null;
                    _connector = null;
                    _placingConnector = false;
                }
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            _connection = null;
            _connector = null;
            _placingConnector = false;
        }
    }
}
