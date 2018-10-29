using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PanelsManagerUI : MonoBehaviour {

    [SerializeField]
    GameObject[] panels;

	// Use this for initialization
	void Start () {
		for(int i=0; i<panels.Length; i++)
        {
            panels[i].SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                DeactivateAllPanels();
            }
        }
	}

    public void ActivateMarketPanel()
    {
        Time.timeScale = 0;
        for (int i = 0; i < panels.Length; i++)
        {
            if (panels[i].CompareTag("MarketPanel"))
            {
                panels[i].SetActive(true);
            }
            else
            {
                panels[i].SetActive(false);
            }
        }
    }

    public void ActivateBuildingPanel()
    {
        for (int i = 0; i < panels.Length; i++)
        {
            if (panels[i].CompareTag("BuildingPanel"))
            {
                panels[i].SetActive(true);
            }
            else
            {
                panels[i].SetActive(false);
            }
        }
    }

    public void ExitMarket()
    {
        foreach(GameObject panel in panels)
        {
            panel.SetActive(false);
        }
        Time.timeScale = 1;
    }

    public void DeactivateAllPanels()
    {
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);
        }
    }
}
