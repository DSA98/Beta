using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingUIButtons : MonoBehaviour {

    [SerializeField]
    private Building mBuilding;
    [SerializeField]
    private int id = 0;

    public void MoveBuilding()
    {
        if (mBuilding.IsBuilt)
        {
            InactivateBuildingUi();
            mBuilding.IsPlaced = false;
            mBuilding.PlaceTime = 0.1f;
            BuildingController.BuildingControl.BuildingForConstruction = mBuilding;
        }
    }

    public void TearBuildingDown()
    {
        InactivateBuildingUi();
        mBuilding.IsPlaced = false;
        mBuilding.PlaceTime = 0.1f;
        mBuilding.IsBuilt = false;
        mBuilding.TimerBuilding = 0;
        if(mBuilding is Barn)
        {
            Inventory.ResourcesInstance.DecreaseStorageOnBarnDestroy();
        }
        if(mBuilding is BuildingsProducers)
        {
            mBuilding.GetComponent<BuildingsProducers>().ProductProduced = 0;
            mBuilding.StopAllCoroutines();
        }
        DynamicPooling.PoolInstance.ReturnGameObjectToList(mBuilding.gameObject, id);
    }

    private void InactivateBuildingUi()
    {
        Canvas objectCanvas = GetComponentInParent<Canvas>();
        Image[] uiElements = objectCanvas.GetComponentsInChildren<Image>();
        foreach(Image uiElement in uiElements)
        {
            uiElement.gameObject.SetActive(false);
        }
    }
}
