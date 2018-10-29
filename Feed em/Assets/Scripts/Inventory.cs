using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour{

    private static Inventory resourcesInstance = null;
    public static Inventory ResourcesInstance
    {
        get
        {
            /*if (resourcesInstance == null)
            {
                resourcesInstance = new Inventory();
            }*/
            return resourcesInstance;
        }
    }

    //Attributes
    private Dictionary<CurrenciesEnum, float> currencies = null;
    private Dictionary<ResourcesEnum, int> resources = null;
    private Dictionary<StorageEnum, int> storage = null;

    private float maxWaterCapacity = 100f;
    private int maxStorageCapacity = 50;
    
    // Properties
    public float MaxWaterCapacity
    {
        get
        {
            return maxWaterCapacity;
        }

        set
        {
            maxWaterCapacity = value;
        }
    }
    public int MaxStorageCapacity
    {
        get
        {
            return maxStorageCapacity;
        }

        set
        {
            maxStorageCapacity = value;
        }
    }
    public Dictionary<ResourcesEnum, int> Resources
    {
        get
        {
            return resources;
        }

        set
        {
            resources = value;
        }
    }
    public Dictionary<StorageEnum, int> Storage
    {
        get
        {
            return storage;
        }

        set
        {
            storage = value;
        }
    }
    public Dictionary<CurrenciesEnum, float> Currencies
    {
        get
        {
            return currencies;
        }

        set
        {
            currencies = value;
        }
    }

    private void Awake()
    {
        if (resourcesInstance == null)
        {
            resourcesInstance = this;
        }
        else
        {
            Destroy(this);
        }
        Resources = new Dictionary<ResourcesEnum, int>();
        Storage = new Dictionary<StorageEnum, int>();
        Currencies = new Dictionary<CurrenciesEnum, float>();
        currencies.Add(CurrenciesEnum.money, 2000);
        currencies.Add(CurrenciesEnum.water, 100);
        resources.Add(ResourcesEnum.eggs, 0);
        resources.Add(ResourcesEnum.milk, 0);
        resources.Add(ResourcesEnum.seeds, 5);
        resources.Add(ResourcesEnum.wheat, 0);
        storage.Add(StorageEnum.storage, 0);
        storage.Add(StorageEnum.animalsSupplies, 100);
    }

    /*public Inventory()
    {
        currencies.Add(CurrenciesEnum.money, 2000);
        currencies.Add(CurrenciesEnum.water, 100);
        resources.Add(ResourcesEnum.eggs, 0);
        resources.Add(ResourcesEnum.milk, 0);
        resources.Add(ResourcesEnum.seeds, 5);
        resources.Add(ResourcesEnum.wheat, 0);
        storage.Add(StorageEnum.storage, 0);
        storage.Add(StorageEnum.animalsSupplies, 100);
    }*/

    public bool VerifyCurrencies(Dictionary<CurrenciesEnum, float> _costo)
    {
        bool canPurchase = false;
        if (_costo[CurrenciesEnum.money] <= currencies[CurrenciesEnum.money] && _costo[CurrenciesEnum.water] <= currencies[CurrenciesEnum.water]) 
        {
            canPurchase = true;
        }
        else
        {
            canPurchase = false;
        }
        return canPurchase;
    }

    public bool VerifyWaterCapacity(Dictionary<CurrenciesEnum, float> _quantity)
    {
        bool capacityIsAlright = false;
        if (currencies[CurrenciesEnum.water] < maxWaterCapacity)
        {
            capacityIsAlright = true;
        }
        return capacityIsAlright;
    }

    public bool VerifyStorageCapacity()
    {
        bool theresStorage = false;
        if (storage[StorageEnum.storage] < maxStorageCapacity)
        {
            theresStorage = true;
        }
        else
        {
            theresStorage = false;
        }
        return theresStorage;
    }

    public void IncreaseWater(Dictionary<CurrenciesEnum, float> _resources)
    {
        if (currencies[CurrenciesEnum.money] - _resources[CurrenciesEnum.money] >= 0)
        {
            currencies[CurrenciesEnum.money] -= _resources[CurrenciesEnum.money];
            currencies[CurrenciesEnum.water] += _resources[CurrenciesEnum.water];
        }
    }

    public void ReduceCurrencies(Dictionary<CurrenciesEnum, float> _cost)
    {
        currencies[CurrenciesEnum.money] -= _cost[CurrenciesEnum.money];
        currencies[CurrenciesEnum.water] -= _cost[CurrenciesEnum.water];
    }

    public void IncreaseCurrencies(Dictionary<CurrenciesEnum, float> _cost)
    {
        currencies[CurrenciesEnum.money] += _cost[CurrenciesEnum.money];
        currencies[CurrenciesEnum.water] += _cost[CurrenciesEnum.water];
    }

    public void DecreaseStorageOnBarnDestroy()
    {
        storage[StorageEnum.storage] -= 20;
    }
}
