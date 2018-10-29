using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMarketManager : MonoBehaviour {

    private static InventoryMarketManager marketManager = null;
    public static InventoryMarketManager MarketManager
    {
        get
        {
            return marketManager;
        }
    }

	// Use this for initialization
	void Awake () {
        if (marketManager == null)
        {
            marketManager = this;
        }
        else
        {
            Destroy(this);
        }
    }
    
    public void SellingWheat()
    {
        if (Inventory.ResourcesInstance.Resources[ResourcesEnum.wheat] >= 5)
        {
            Inventory.ResourcesInstance.Resources[ResourcesEnum.wheat] -= 5;
            Inventory.ResourcesInstance.Currencies[CurrenciesEnum.money] += 30;
        }
        else
        {
            print("You dont have enough wheat for selling");
        }
    }

    public void SellingMilk()
    {
        if (Inventory.ResourcesInstance.Resources[ResourcesEnum.milk] >= 5)
        {
            Inventory.ResourcesInstance.Resources[ResourcesEnum.milk] -= 5;
            Inventory.ResourcesInstance.Currencies[CurrenciesEnum.money] += 20;
        }
        else
        {
            print("You dont have enough milk for selling");
        }
    }

    public void SellingEggs()
    {
        if (Inventory.ResourcesInstance.Resources[ResourcesEnum.eggs] >= 5)
        {
            Inventory.ResourcesInstance.Resources[ResourcesEnum.eggs] -= 5;
            Inventory.ResourcesInstance.Currencies[CurrenciesEnum.money] += 25;
        }
        else
        {
            print("You dont have enough eggs for selling");
        }
    }

    public void DonatingWheat()
    {
        if (Inventory.ResourcesInstance.Resources[ResourcesEnum.wheat] >= 5)
        {
            if (Inventory.ResourcesInstance.Storage[StorageEnum.animalsSupplies] + 5 < 100)
            {
                Inventory.ResourcesInstance.Resources[ResourcesEnum.wheat] -= 5;
                Inventory.ResourcesInstance.Storage[StorageEnum.animalsSupplies] += 5;
            }
            else
            {
                print("The animals have plenty of food thx to you");
            }
        }
        else
        {
            print("You Dont have enough wheat for selling");
        }
    }

    public void DonatingMilk()
    {
        if (Inventory.ResourcesInstance.Resources[ResourcesEnum.milk] >= 5)
        {
            if (Inventory.ResourcesInstance.Storage[StorageEnum.animalsSupplies] + 5 < 100)
            {
                Inventory.ResourcesInstance.Resources[ResourcesEnum.milk] -= 5;
                Inventory.ResourcesInstance.Storage[StorageEnum.animalsSupplies] += 5;
            }
            else
            {
                print("The animals have plenty of food thx to you");
            }
        }
        else
        {
            print("You Dont have enough wheat for selling");
        }
    }

    public void DonatingEggs()
    {
        if (Inventory.ResourcesInstance.Resources[ResourcesEnum.eggs] >= 5)
        {
            if (Inventory.ResourcesInstance.Storage[StorageEnum.animalsSupplies] + 5 < 100)
            {
                Inventory.ResourcesInstance.Resources[ResourcesEnum.eggs] -= 5;
                Inventory.ResourcesInstance.Storage[StorageEnum.animalsSupplies] += 5;
            }
            else
            {
                print("The animals have plenty of food thx to you");
            }
        }
        else
        {
            print("You Dont have enough wheat for selling");
        }
    }
}
