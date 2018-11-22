using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingManager : MonoBehaviour
{
    public GameObject[] Buildings = null;
    public GameObject connector = null, buildingInfoUI = null;
    public Text[] buildingUI = null;
    public Camera curCamera = null;
    public ResourceManager resourceManager = null;
    public Canvas canvas = null;
    public List<Connector> connectors = new List<Connector>();

    private enum enumBuildingID { turretID, powerPlantID, TransmitterID };
    private bool _placingBuilding, _placingConnector, buildingMode;
    private GameObject _curBuilding, _buildinginfoUIText;
    private Ray _ray;
    private RaycastHit _mousePos;
    private Building _connection, _selectedBuilding;
    private Transmitter _connector;


    void Start()
    {

    }

    void Update()
    {
        //pressing "B" will take you into building mode as to not accidently open up info panels
        if (Input.GetKeyDown(KeyCode.B))
        {
            buildingMode = !buildingMode;
            if (_buildinginfoUIText != null)
            {
                _buildinginfoUIText.gameObject.SetActive(false);
            }
        }
        //run the different controls depending on if you are making a building or a connector
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
            //if you click on a building bring up the info panel on it
            if (Input.GetMouseButtonDown(0))
            {
                _ray = curCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(_ray, out _mousePos))
                {
                    _selectedBuilding = _mousePos.collider.gameObject.GetComponent<Building>();
                    if (_selectedBuilding != null)
                    {
                        UpdateInfoUI();
                    }
                }
            }

        }
    }

    //check if the player has enough money for the building make the building and make it follow the mouse postion in the world
    public void CreateBuilding(int BuildingID)
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
    //make the connector run the building controls for it
    public void ConnectorCreate()
    {
        _placingConnector = true;
        UpdateBuildCosts();
    }
    //make sure the turret costs are up to date
    public void UpdateBuildCosts()
    {
        for (int i = 0; i < buildingUI.Length; i++)
        {
            buildingUI[i].text = "Turret" + i.ToString() + " $ " + Turret.cost[i + 1];
        }
    }
    //building controls for all regular buildings
    private void BuildingControls()
    {
        // ~(1 << LayerMask.NameToLayer("Building non-collidables"))
        //left clicking will place down the building but only if the raycast hits something collideable
        if (Input.GetMouseButtonDown(0) && Physics.Raycast(_ray, out _mousePos, Mathf.Infinity, 1 << LayerMask.NameToLayer("BuildableLayer")))
        {
            //take away the cost from the money of the player
            Building _curBuildingScript = _curBuilding.GetComponentInChildren<Building>();
            resourceManager.ChangeMoney(-Building.cost[_curBuildingScript.buildingID]);
            if (_curBuildingScript.buildingID == (int)enumBuildingID.turretID)
            {
                resourceManager.turrets.Add((Turret)_curBuildingScript);
            }
            //add the building to the blackboard and turn off placement mode
            _curBuilding = null;
            _placingBuilding = false;
            BlackBoard.buildings.Add(_curBuildingScript.gameObject);
            return;
        }
        //get rid of the building if the player right clicks
        else if (Input.GetMouseButton(1))
        {
            Destroy(_curBuilding);
            _curBuilding = null;
            _placingBuilding = false;
            return;
        }
        //move the building to the players mouse position
        _ray = curCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(_ray, out _mousePos, Mathf.Infinity, 1 << LayerMask.NameToLayer("BuildableLayer")))
        {
            _curBuilding.transform.position = _mousePos.point;
            _curBuilding.transform.Translate(new Vector3(0, 0.5f, 0));
        }
    }
    //building controls for the connector tool
    private void BuildingConnector()
    {
        //if there are connectors already make them visable to the player
        for (int i = 0; i < connectors.Count; i++)
        {
            connectors[i].ActivateRenderer(true);
        }
        if (Input.GetMouseButtonDown(0))
        {
            _ray = curCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(_ray, out _mousePos))
            {
                //selects one transmittor and a building has to be one transmittor to a building, transmittors are also buildings so you can connect one transmittor to another
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
                    //connect the buildings and make a linerender object to make a line between
                    Connector tempConnector = Instantiate(connector, _connector.transform.position, _connector.transform.rotation).GetComponent<Connector>();
                    tempConnector.SetConnections(_connector, _connection);
                    _connector.Connection(_connection);
                    connectors.Add(tempConnector);
                    tempConnector.buildingManager = this;
                    _connection = null;
                    _connector = null;
                }
            }
        }
        //cancel making a connector
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
    //InfoUI update or make a new infopanel
    private void UpdateInfoUI()
    {
        //if the object doesn't exist make it
        if (_buildinginfoUIText == null)
        {
            _buildinginfoUIText = Instantiate(buildingInfoUI, canvas.transform);
        }
        //make it active if it does exist
        _buildinginfoUIText.gameObject.SetActive(true);
        _ray = curCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(_ray, out _mousePos))
        {
            //get the building at the raycast position
            _selectedBuilding = _mousePos.collider.gameObject.GetComponent<Building>();
            _buildinginfoUIText.transform.position = _selectedBuilding.transform.position;
            _buildinginfoUIText.transform.Translate(new Vector3(1.0f, 2.0f));
        }
        //set the text inside to give information about the building
        buildingInfoUI.GetComponentInChildren<Text>().text = "Powered " + _selectedBuilding.powered + "\n avaliable electricty";
    }
}
