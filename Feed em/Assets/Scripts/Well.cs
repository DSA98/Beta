using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Well : Building {

    private Dictionary<CurrenciesEnum, float> resources;
    private int waterGiven = 10;
    private int costForActivation = 10;

    [SerializeField] private float grid = 0.5f;
    private float reciprocalGrid;

	// Use this for initialization
	protected override void Awake () {
        base.Awake();
        mRender.material.color = Color.green;
        isBuilt = true;
        canBePlaced = true;
        isPlaced = true;
        reciprocalGrid = 1f / grid;
        transform.position = new Vector3(Mathf.Round(transform.position.x * reciprocalGrid) / reciprocalGrid, transform.position.y, Mathf.Round(transform.position.z * reciprocalGrid) / reciprocalGrid);
        resources = new Dictionary<CurrenciesEnum, float>();
        resources.Add(CurrenciesEnum.water, 10);
        resources.Add(CurrenciesEnum.money, 10);
    }

    public void GetWater()
    {
        if(Inventory.ResourcesInstance.VerifyWaterCapacity(resources))
        {
            Inventory.ResourcesInstance.IncreaseWater(resources);
        }
        else
        {
            print("You have plenty of water");
        }
    }
}
