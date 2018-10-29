using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBuildingsButtons : MonoBehaviour {

    //This Id must be the same number as the position of the pool gameObject array
    [SerializeField]
    private int id;

    //Get building is called from the building buttons to pull an object from a pool
    public void GetBuilding()
    {
        //GameObject pulled from the pool
        GameObject buildingPoolObject = DynamicPooling.PoolInstance.GetGameObjectFromPool(id);

        //Checks whether or not theres enough resources for building
        if (!Inventory.ResourcesInstance.VerifyCurrencies(buildingPoolObject.GetComponent<Building>().BuildingCost))
        {
            DynamicPooling.PoolInstance.ReturnGameObjectToList(buildingPoolObject, id);
            print("You dont have enough resources");
        }
        else
        {
            BuildingController.BuildingControl.BuildingForConstruction = buildingPoolObject.GetComponent<Building>();
            buildingPoolObject.SetActive(true);
            buildingPoolObject.transform.rotation = Quaternion.identity;
        }
    }
}
