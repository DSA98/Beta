using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingController : MonoBehaviour {

    #region Variables
    //Global instace of the class
    private static BuildingController buildingControl = null;
    public static BuildingController BuildingControl
    {
        get
        {
            return buildingControl;
        }
    }

    //Building chosen for construction
    private Building buildingForConstruction = null;
    public Building BuildingForConstruction
    {
        get
        {
            return buildingForConstruction;
        }

        set
        {
            buildingForConstruction = value;
        }
    }

    //Ray variables for placing the building
    Ray mousePositionOverMap;
    RaycastHit RayInfo;

    //LayerMask for Raycasting
    [SerializeField]
    LayerMask maskLayerFloor;

    //Camera for RayCasting
    [SerializeField] Camera mainCamera;

    [SerializeField] private float grid = 0.5f;

    private float reciprocalGrid;
    #endregion

    private void Awake()
    {
        //Creation of instance
        if (buildingControl == null)
        {
            buildingControl = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (buildingForConstruction != null)
        {
            MoveBuilding();
            if (Input.GetMouseButtonDown(1))
            {
                if (!buildingForConstruction.IsBuilt)
                {
                    DisbandCurrentBuilding();
                }
            }
            else if (Input.GetMouseButtonDown(0))
            {
                if (buildingForConstruction.CanBePlaced)
                {
                    StartBuilding();
                }
                else
                {
                    print("Building is blocked");
                }
            }
        }
    }

    private void MoveBuilding()
    {
        mousePositionOverMap = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(mousePositionOverMap, out RayInfo, 500, maskLayerFloor))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                if (grid > 0)
                {
                    reciprocalGrid = 1f / grid;
                    buildingForConstruction.transform.position = new Vector3(Mathf.Round(RayInfo.point.x * reciprocalGrid) / reciprocalGrid, 
                    RayInfo.point.y, Mathf.Round(RayInfo.point.z * reciprocalGrid) / reciprocalGrid);
                }
            }
        }
    }

    private void StartBuilding()
    {
        mousePositionOverMap = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(mousePositionOverMap, out RayInfo, 500, maskLayerFloor))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                if (grid > 0)
                {
                    reciprocalGrid = 1f / grid;
                    buildingForConstruction.transform.position = new Vector3(Mathf.Round(RayInfo.point.x * reciprocalGrid) / reciprocalGrid,
                    RayInfo.point.y, Mathf.Round(RayInfo.point.z * reciprocalGrid) / reciprocalGrid);
                }
                if (!buildingForConstruction.IsBuilt)
                {
                    if (buildingForConstruction is BuildingsProducers)
                    {
                        buildingForConstruction.StartCoroutine(buildingForConstruction.GetComponent<BuildingsProducers>().StartProducing());
                    }
                    if(buildingForConstruction is Barn)
                    {
                        buildingForConstruction.StartCoroutine(buildingForConstruction.GetComponent<Barn>().RaiseStorageCapacity());
                    }

                    Inventory.ResourcesInstance.ReduceCurrencies(buildingForConstruction.BuildingCost);
                    buildingForConstruction.StartCoroutine(buildingForConstruction.Build());
                }
                if (!buildingForConstruction.IsPlaced)
                {
                    buildingForConstruction.StartCoroutine(buildingForConstruction.Place());
                }
                buildingForConstruction = null;
            }
        }
    }

    private void DisbandCurrentBuilding()
    {
        if (buildingForConstruction != null)
        {
            //It reads the type of pool for return
            if (buildingForConstruction is Farm)
            {
                DynamicPooling.PoolInstance.ReturnGameObjectToList(buildingForConstruction.gameObject, 0);
            }
            if (buildingForConstruction is MilkFactory)
            {
                DynamicPooling.PoolInstance.ReturnGameObjectToList(buildingForConstruction.gameObject, 1);
            }
            if (buildingForConstruction is Barn)
            {
                DynamicPooling.PoolInstance.ReturnGameObjectToList(buildingForConstruction.gameObject, 2);
            }
            if (buildingForConstruction is EggsFactory)
            {
                DynamicPooling.PoolInstance.ReturnGameObjectToList(buildingForConstruction.gameObject, 3);
            }
            if (buildingForConstruction is FarmLand)
            {
                DynamicPooling.PoolInstance.ReturnGameObjectToList(buildingForConstruction.gameObject, 4);
            }
            //We delete the current building selected to avoid null references
            buildingForConstruction = null;
        }
    }
}
