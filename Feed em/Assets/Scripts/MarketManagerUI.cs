using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketManagerUI : MonoBehaviour {

	public void SellWheat()
    {
        InventoryMarketManager.MarketManager.SellingWheat();
    }
    public void SellMilk()
    {
        InventoryMarketManager.MarketManager.SellingMilk();
    }
    public void SellEggs()
    {
        InventoryMarketManager.MarketManager.SellingEggs();
    }
    public void DonateWheat()
    {
        InventoryMarketManager.MarketManager.DonatingWheat();
    }
    public void DonateMilk()
    {
        InventoryMarketManager.MarketManager.DonatingMilk();
    }
    public void DonateEggs()
    {
        InventoryMarketManager.MarketManager.SellingEggs();
    }
}
