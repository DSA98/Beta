using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BuildingsProducers : Building {

    [SerializeField]
    protected Image resourceImage, uiPanel;
    protected int productProduced = 0;
    protected bool uiPanelIsActive = false;
    protected Dictionary<CurrenciesEnum, float> productionCost;

    public int ProductProduced
    {
        get
        {
            return productProduced;
        }
        set
        {
            productProduced = value;
        }
    }
    public bool UiPanelIsActive
    {
        get { return uiPanelIsActive; }
        set { uiPanelIsActive = value; }
    }

    protected override void Awake()
    {
        base.Awake();
        productionCost = new Dictionary<CurrenciesEnum, float>();
    }

    protected void Start()
    {
        MouseBuildingInteraction.InteractionInstance.OnDeactivation += DeactivateUi;
    }

    protected virtual void Update()
    {
        if (productProduced >= 5)
        {
            if (!uiPanelIsActive)
            {
                uiPanel.gameObject.SetActive(true);
            }
            resourceImage.gameObject.SetActive(true);
        }
        else
        {
            resourceImage.gameObject.SetActive(false);
        }
    }

    public IEnumerator StartProducing()
    {
        yield return new WaitForSeconds(buildingTime);
        while (isBuilt)
        {
            yield return new WaitForSeconds(3f);
            ProduceGoods();
        }
        //InvokeRepeating("ProduceGoods", buildingTime + 3f, 3f);
    }

    private void DeactivateUi()
    {
        uiPanelIsActive = false;
        if (productProduced >= 5)
        {
            uiPanel.gameObject.SetActive(false);
        }
    }

    public abstract void ProduceGoods();
    public abstract void HarvestResources();
}
