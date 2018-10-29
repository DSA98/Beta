using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicPooling : MonoBehaviour {

    //Instance of pooling script
    private static DynamicPooling poolInstance = null;
    public static DynamicPooling PoolInstance
    {
        get { return poolInstance; }
    }

    //Array of gameObjects for pooling
    [SerializeField]
    private GameObject[] gameObjects;

    //Pool size for pools. If dynamic is active, you can adjust each pool size, if not, all pools will adjust to one size (poolSize)
    [SerializeField]
    private int poolSize;
    [SerializeField]
    private int[] poolSizeDynamic;

    //Lists for pooling
    public List<GameObject>[] poolListArray;

    //Boolean to activate dynamic pool sizes
    [SerializeField]
    private bool dynamicPoolSize = false;

    //Initialize this script as the instance if there is no other object with it
    private void Awake()
    {
        if (poolInstance == null)
        {
            poolInstance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    //Initialize pools
    void Start () {
        if (!dynamicPoolSize)
        {
            poolListArray = new List<GameObject>[gameObjects.Length];
            for (int i = 0; i < poolListArray.Length; i++)
            {
                poolListArray[i] = new List<GameObject>();
            }
            for (int i = 0; i < poolListArray.Length; i++)
            {
                for (int j = 0; j < poolSize; j++)
                {
                    GameObject gameObjectClone = Instantiate(gameObjects[i]);
                    gameObjectClone.SetActive(false);
                    poolListArray[i].Add(gameObjectClone);
                }
            }
        }
        if (dynamicPoolSize)
        {
            poolListArray = new List<GameObject>[gameObjects.Length];
            for (int i = 0; i < poolListArray.Length; i++)
            {
                poolListArray[i] = new List<GameObject>();
            }
            for (int i = 0; i < poolListArray.Length; i++)
            {
                for (int j = 0; j < poolSizeDynamic[i]; j++)
                {
                    GameObject gameObjectClone = Instantiate(gameObjects[i]);
                    gameObjectClone.SetActive(false);
                    poolListArray[i].Add(gameObjectClone);
                }
            }
        }
	}

    public GameObject GetGameObjectFromPool(int _numberOfTheList)
    {
        if (poolListArray[_numberOfTheList].Count > 0)
        {
            return AllocateGameObjectFromList(_numberOfTheList);
        }
        else if (poolListArray[_numberOfTheList].Count <= 0)
        {
            ProduceGameObjectForPool(_numberOfTheList);
            return AllocateGameObjectFromList(_numberOfTheList);
        }
        return null;
    }

    public GameObject AllocateGameObjectFromList(int _numberOfTheList)
    {
        for (int i = 0; i < poolListArray[_numberOfTheList].Count; i++)
        {
            //In case of an inactive object in the pool
            if (!poolListArray[_numberOfTheList][i].activeInHierarchy)
            {
                //We return the pool object and we eliminate it from the list for it's use
                GameObject objectForPooling = poolListArray[_numberOfTheList][i];
                poolListArray[_numberOfTheList].Remove(poolListArray[_numberOfTheList][i]);
                return objectForPooling;
            }
        }
        return null;
    }

    public void ProduceGameObjectForPool(int _numberOfTheList)
    {
        GameObject cloneObjectPool = Instantiate(gameObjects[_numberOfTheList]);
        cloneObjectPool.SetActive(false);
        poolListArray[_numberOfTheList].Add(cloneObjectPool);
    }

    public void ReturnGameObjectToList(GameObject _gamObject, int _numberOfTheList)
    {
        _gamObject.SetActive(false);
        poolListArray[_numberOfTheList].Add(_gamObject);
    }
}
