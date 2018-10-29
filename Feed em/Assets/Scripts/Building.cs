using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public abstract class Building : MonoBehaviour
{
    //Hierarchy attributes
    protected Renderer mRender;
    protected Dictionary<CurrenciesEnum, float> buildingCost;
    protected float buildingTime;
    protected float timerBuilding;
    protected bool isBuilt = false;
    protected bool isPlaced = false;
    protected bool canBePlaced = true;
    protected Rigidbody mBody;
    protected BoxCollider mCollider;
    protected float placeTime = 0.1f;

    //Class properties
    public bool IsBuilt
    {
        get
        {
            return isBuilt;
        }
        set
        {
            isBuilt = value;
        }
    }
    public bool IsPlaced
    {
        get { return isPlaced; }
        set { isPlaced = value; }
    }
    public bool CanBePlaced
    {
        get { return canBePlaced; }
    }
    public float PlaceTime
    {
        get { return placeTime; }
        set { placeTime = value; }
    }
    public float TimerBuilding
    {
        get { return timerBuilding; }
        set { timerBuilding = value; }
    }
    public Dictionary<CurrenciesEnum, float> BuildingCost
    {
        get
        {
            return buildingCost;
        }

        set
        {
            buildingCost = value;
        }
    }

    // Use this for initialization
    protected virtual void Awake()
    {
        mRender = GetComponent<Renderer>();
        mBody = GetComponent<Rigidbody>();
        mCollider = GetComponent<BoxCollider>();
        mRender.material.color = Color.magenta;
        mBody.useGravity = false;
        mCollider.isTrigger = true;
        buildingCost = new Dictionary<CurrenciesEnum, float>();
    }

    public virtual IEnumerator Build()
    {
        while (!isBuilt)
        {
            timerBuilding += Time.deltaTime;
            if (timerBuilding >= buildingTime)
            {
                isBuilt = true;
                mRender.material.color = Color.green;
                StopCoroutine(Build());
            }
            yield return null;
        }
    }

    public virtual IEnumerator Place()
    {
        yield return new WaitForSeconds(placeTime);
        isPlaced = true;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (!isPlaced)
        {
            if (other.gameObject.GetComponent<Building>() != null)
            {
                canBePlaced = false;
                mRender.material.color = Color.red;
            }
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (!isPlaced)
        {
            if (other.gameObject.GetComponent<Building>() != null)
            {
                canBePlaced = true;
                if (!isBuilt)
                {
                    mRender.material.color = Color.magenta;
                }
                else
                {
                    mRender.material.color = Color.green;
                }
            }
        }
    }
}
