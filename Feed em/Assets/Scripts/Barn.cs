using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barn : Building {

    // Use this for initialization
    protected override void Awake() {
        base.Awake();
        buildingCost.Add(CurrenciesEnum.money, 300);
        buildingCost.Add(CurrenciesEnum.water, 0);
        //buildingCost = 300;
        buildingTime = 5f;
    }

    public IEnumerator RaiseStorageCapacity()
    {
        yield return new WaitForSeconds(buildingTime);
        Inventory.ResourcesInstance.MaxStorageCapacity += 20;
        print("INcrease storage capacity");
    }
}
