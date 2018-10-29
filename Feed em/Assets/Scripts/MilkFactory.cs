using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilkFactory : BuildingsProducers
{

    protected override void Awake() {
        base.Awake();
        buildingCost.Add(CurrenciesEnum.money, 200);
        buildingCost.Add(CurrenciesEnum.water, 0);
        productionCost.Add(CurrenciesEnum.money, 0);
        productionCost.Add(CurrenciesEnum.water, 2);
        //buildingCost = 200;
        buildingTime = 5f;
    }

    public override void ProduceGoods()
    {
        if (Inventory.ResourcesInstance.VerifyCurrencies(productionCost))
        {
            productProduced += 1;
            Inventory.ResourcesInstance.ReduceCurrencies(productionCost);
            print("Producing Milk");
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
}
