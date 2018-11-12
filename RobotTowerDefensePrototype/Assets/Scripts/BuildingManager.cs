using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingManager : MonoBehaviour {
    public GameObject[] Buildings = null;
    public GameObject connector = null, buildingInfoUI = null;
    public Text[] buildingUI = null;
    public Camera curCamera = null;
    public ResourceManager resourceManager = null;
    public Canvas canvas = null;
    public List<Connector> connectors = new List<Connector>();

    private enum enumBuildingID {turretID,powerPlantID,TransmitterID};
    private bool _placingBuilding, _placingConnector, buildingMode;
    private GameObject _curBuilding, _buildinginfoUIText;
    private Ray _ray;
    private RaycastHit _mousePos;
    private Building _connection, _selectedBuilding;
    private Transmitter _connector;


    void Start () {

    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.B))
        {
            buildingMode = !buildingMode;
            _buildinginfoUIText.gameObject.SetActive(false);
        }

        if (buildingMode)
        {
            if (_curBuilding != null && _placingBuilding)
            {
                BuildingControls();
            }
            else if (_placingConnector)
            {
                BuildingConnector();
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                _ray = curCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(_ray, out _mousePos))
                {
                    _selectedBuilding = _mousePos.collider.gameObject.GetComponent<Building>();
                    
                }
                UpdateInfoUI();
            }
            
        }
    }

    public void CreateBuilding (int BuildingID)
    {
        if (Building.cost[BuildingID] < resourceManager.money)
        {
            _selectedBuilding = null;
            _curBuilding = Instantiate(Buildings[BuildingID], new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
            _curBuilding.GetComponentInChildren<Building>().buildingID = BuildingID;
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
            //resourceManager.ChangeElectricity(Building.electricityCost[_curBuildingScript.buildingID]);

            if (_curBuildingScript.buildingID == (int)enumBuildingID.turretID)
            {
                resourceManager.turrets.Add((Turret)_curBuildingScript);
            }

            _curBuilding = null;
            _placingBuilding = false;
            BlackBoard.buildings.Add(_curBuildingScript.gameObject);
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
        for (int i = 0; i < connectors.Count; i++)
        {
            connectors[i].ActivateRenderer(true);
        }
        if (Input.GetMouseButtonDown(0))
        {
            _ray = curCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(_ray, out _mousePos))
            {
                if (_mousePos.collider.GetComponent<Transmitter>() != null && _connector == null)
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
                    Connector tempConnector = Instantiate(connector, _connector.transform.position, _connector.transform.rotation).GetComponent<Connector>();
                    tempConnector.SetConnections(_connector, _connection);
                    connectors.Add(tempConnector);
                    tempConnector.buildingManager = this;
                    _connection = null;
                    _connector = null;
                }
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            _connection = null;
            _connector = null;
            _placingConnector = false;

            for (int i = 0; i < connectors.Count; i++)
            {
                connectors[i].ActivateRenderer(false);
            }
        }
    }
    private void UpdateInfoUI()
    {
        if (_buildinginfoUIText == null)
        {
            _buildinginfoUIText = Instantiate(buildingInfoUI, canvas.transform);
        }
        _buildinginfoUIText.gameObject.SetActive(true);
        _ray = curCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(_ray, out _mousePos))
        {
            _selectedBuilding = _mousePos.collider.gameObject.GetComponent<Building>();
            _buildinginfoUIText.transform.position = _selectedBuilding.transform.position;
            _buildinginfoUIText.transform.Translate(new Vector3(1.0f, 2.0f));
        }
        buildingInfoUI.GetComponentInChildren<Text>().text = "Powered " + _selectedBuilding.powered + "\n avaliable electricty";
    }
}
