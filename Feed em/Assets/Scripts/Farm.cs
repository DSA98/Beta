using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : BuildingsProducers
{
    protected override void Awake() {
        base.Awake();
        mRender.material.color = Color.red;
        buildingCost.Add(CurrenciesEnum.money, 50);
        buildingCost.Add(CurrenciesEnum.water, 10);
        productionCost.Add(CurrenciesEnum.money, 0);
        productionCost.Add(CurrenciesEnum.water, 2);
       /* buildingCost = 50;
        buildingWaterCost = 10;*/
        buildingTime = 10f;
        canBePlaced = false;
    }

    public override void ProduceGoods()
    {
        if (Inventory.ResourcesInstance.VerifyCurrencies(productionCost))
        {
            productProduced += 1;
            Inventory.ResourcesInstance.VerifyCurrencies(productionCost);
            print("Producing Wheat");
        }
        else
        {
            print("No water for producing");
        }
    }

    public override void HarvestResources()
    {
        if (Inventory.ResourcesInstance.VerifyStorageCapacity())
        {
            while (Inventory.ResourcesInstance.Storage[StorageEnum.storage] < Inventory.ResourcesInstance.MaxStorageCapacity + 1)
            {
                if (productProduced > 0)
                {
                    Inventory.ResourcesInstance.Resources[ResourcesEnum.eggs] += 1;
                    productProduced -= 1;
                }
                if (productProduced <= 0)
                {
                    uiPanel.gameObject.SetActive(false);
                    break;
                }
            }
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (!isPlaced)
        {
            if (other.gameObject.GetComponent<FarmLand>() != null)
            {
                canBePlaced = true;
                mRender.material.color = Color.magenta;
            }
        }
    }

    protected override void OnTriggerExit(Collider other)
    {
        if (!isBuilt)
        {
            if (other.gameObject.GetComponent<FarmLand>() != null)
            {
                canBePlaced = false;
                mRender.material.color = Color.red;
            }
        }
    }
}
