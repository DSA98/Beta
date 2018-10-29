using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesUI : MonoBehaviour {

    [SerializeField]
    private Text eggsTxt, moneyTxt, seedsTxt, wheatTxt, milkTxt;
    [SerializeField]
    private Slider waterSlider, animalsSuppliesSlider, storageSlider;

    private bool lostCondition = false;

	void Awake () {
        eggsTxt.text = Inventory.ResourcesInstance.Resources[ResourcesEnum.eggs].ToString();
        seedsTxt.text = Inventory.ResourcesInstance.Resources[ResourcesEnum.seeds].ToString();
        wheatTxt.text = Inventory.ResourcesInstance.Resources[ResourcesEnum.wheat].ToString();
        milkTxt.text = Inventory.ResourcesInstance.Resources[ResourcesEnum.milk].ToString();
        waterSlider.maxValue = Inventory.ResourcesInstance.MaxWaterCapacity;
        waterSlider.value = Inventory.ResourcesInstance.Currencies[CurrenciesEnum.water];
        moneyTxt.text = Inventory.ResourcesInstance.Currencies[CurrenciesEnum.money].ToString();
        animalsSuppliesSlider.maxValue = 100;
        animalsSuppliesSlider.value = Inventory.ResourcesInstance.Storage[StorageEnum.animalsSupplies];
        storageSlider.maxValue = Inventory.ResourcesInstance.MaxStorageCapacity;
        storageSlider.value = Inventory.ResourcesInstance.Storage[StorageEnum.storage];

        StartCoroutine(AnimalsHunger());
    }
	
	// Update is called once per frame
	void Update () {
        Inventory.ResourcesInstance.Storage[StorageEnum.storage] = Inventory.ResourcesInstance.Resources[ResourcesEnum.milk] + Inventory.ResourcesInstance.Resources[ResourcesEnum.eggs] + Inventory.ResourcesInstance.Resources[ResourcesEnum.wheat];

        eggsTxt.text = Inventory.ResourcesInstance.Resources[ResourcesEnum.eggs].ToString();
        moneyTxt.text = Inventory.ResourcesInstance.Currencies[CurrenciesEnum.money].ToString();
        seedsTxt.text = Inventory.ResourcesInstance.Resources[ResourcesEnum.seeds].ToString();
        wheatTxt.text = Inventory.ResourcesInstance.Resources[ResourcesEnum.wheat].ToString();
        milkTxt.text = Inventory.ResourcesInstance.Resources[ResourcesEnum.milk].ToString();
        waterSlider.maxValue = Inventory.ResourcesInstance.MaxWaterCapacity;
        waterSlider.value = Inventory.ResourcesInstance.Currencies[CurrenciesEnum.water];
        animalsSuppliesSlider.value = Inventory.ResourcesInstance.Storage[StorageEnum.animalsSupplies];
        storageSlider.maxValue = Inventory.ResourcesInstance.MaxStorageCapacity;
        storageSlider.value = Inventory.ResourcesInstance.Storage[StorageEnum.storage];

    }

    public IEnumerator AnimalsHunger()
    {
        while (!lostCondition)
        {
            yield return new WaitForSeconds(5f);
            Inventory.ResourcesInstance.Storage[StorageEnum.animalsSupplies] -= 5;
            if (Inventory.ResourcesInstance.Storage[StorageEnum.animalsSupplies] <= 0)
            {
                Time.timeScale = 0;
                print("Game Over");
            }
        }
    }
}
