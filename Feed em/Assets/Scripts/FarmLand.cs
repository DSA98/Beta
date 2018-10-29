using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmLand : Building {

    protected override void Awake()
    {
        base.Awake();
        buildingCost.Add(CurrenciesEnum.money, 5);
        buildingCost.Add(CurrenciesEnum.water, 20);
        /*buildingCost = 5;
        buildingWaterCost = 20;*/
        buildingTime = 0f;
	}
}
