using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseBuildingInteraction : MonoBehaviour {

    private static MouseBuildingInteraction interactionInstance = null;
    public static MouseBuildingInteraction InteractionInstance
    {
        get
        {
            return interactionInstance;
        }

        set
        {
            interactionInstance = value;
        }
    }

    public delegate void DeactivateBuildingUI();
    public event DeactivateBuildingUI OnDeactivation;

    Ray mousePosRay;
    RaycastHit rayInfo;

    [SerializeField]
    Camera mainCamera;

    Canvas currentGameObjectCanvas;

    private float timer = 0f;
    private bool canExecuteButtonUp = true;

    private void Awake()
    {
        if (interactionInstance == null)
        {
            interactionInstance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Update () {
        //Deselection system if there's an input outside the canvas or on another object
        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                mousePosRay = mainCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(mousePosRay, out rayInfo, 500))
                {
                    if (currentGameObjectCanvas != null)
                    {
                        if (OnDeactivation != null)
                        {
                            OnDeactivation();
                        }
                        currentGameObjectCanvas.transform.GetChild(0).gameObject.SetActive(false);
                        currentGameObjectCanvas.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(false);
                        currentGameObjectCanvas.transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(false);
                        currentGameObjectCanvas = null;
                    }
                }
            }
        }
        //If mouse input up execute functions
        else if (Input.GetMouseButtonUp(0))
        {
            if (canExecuteButtonUp)
            {
                mousePosRay = mainCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(mousePosRay, out rayInfo, 500))
                {
                    if (rayInfo.collider.GetComponent<BuildingsProducers>() != null)
                    {
                        if (rayInfo.collider.GetComponent<BuildingsProducers>().ProductProduced >= 5)
                        {
                            rayInfo.collider.GetComponent<BuildingsProducers>().HarvestResources();
                        }
                    }
                    else if (rayInfo.collider.GetComponent<Well>() != null)
                    {
                        rayInfo.collider.GetComponent<Well>().GetWater();
                    }
                }
            }
            canExecuteButtonUp = true;
            timer = 0f;
        }
        //If input stay show a UI element
        else if (Input.GetMouseButton(0))
        {
            timer += Time.deltaTime;
            if (timer >= 0.3f)
            {
                mousePosRay = mainCamera.ScreenPointToRay(Input.mousePosition);
                canExecuteButtonUp = false;
                if (Physics.Raycast(mousePosRay, out rayInfo, 500))
                {
                    if (rayInfo.collider.GetComponent<Building>() != null)
                    {
                        if(rayInfo.collider.GetComponent<BuildingsProducers>() != null)
                        {
                            rayInfo.collider.GetComponent<BuildingsProducers>().UiPanelIsActive = true;
                        }
                        currentGameObjectCanvas = rayInfo.collider.GetComponentInChildren<Canvas>();
                        if (currentGameObjectCanvas != null)
                        {
                            if (!currentGameObjectCanvas.transform.GetChild(0).gameObject.activeInHierarchy)
                            {
                                currentGameObjectCanvas.transform.GetChild(0).gameObject.SetActive(true);
                                currentGameObjectCanvas.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(true);
                                currentGameObjectCanvas.transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(true);
                            }
                        }
                    }
                }
            }
        }
	}
}
