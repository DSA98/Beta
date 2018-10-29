using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggsFactory : BuildingsProducers
{
    protected override void Awake() {
        base.Awake();
        buildingCost.Add(CurrenciesEnum.money, 300);
        buildingCost.Add(CurrenciesEnum.water, 0);
        productionCost.Add(CurrenciesEnum.money, 0);
        productionCost.Add(CurrenciesEnum.water, 2);
        //buildingCost = 300;
        buildingTime = 5f;
	}

    public override void ProduceGoods()
    {
        if (Inventory.ResourcesInstance.VerifyCurrencies(productionCost))
        {
            productProduced += 1;
            Inventory.ResourcesInstance.VerifyCurrencies(productionCost);
            print("Producing Eggs");
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
