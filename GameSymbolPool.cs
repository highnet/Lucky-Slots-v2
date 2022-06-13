using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSymbolPool : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> tensPool;
    [SerializeField]
    private List<GameObject> jacksPool;
    [SerializeField]
    private List<GameObject> queensPool;
    [SerializeField]
    private List<GameObject> kingsPool;
    [SerializeField]
    private List<GameObject> acesPool;
    [SerializeField]
    private List<GameObject> wildsPool;
    [SerializeField]
    private List<GameObject> bonusPool;

    public GameObject FetchFromPool(Symbol symbolType)
    {
        GameObject fetchedGO = null;
        int poolID = (int) symbolType;
        switch (poolID)
        {
            case 0:
                fetchedGO = tensPool[0];
                tensPool.RemoveAt(0); 
                break;
            case 1:
                fetchedGO = jacksPool[0];
                jacksPool.RemoveAt(0); 
                break;
            case 2:
                fetchedGO = queensPool[0];
                queensPool.RemoveAt(0); 
                break;
            case 3:
                fetchedGO = kingsPool[0];
                kingsPool.RemoveAt(0); 
                break;
            case 4:
                fetchedGO = acesPool[0];
                acesPool.RemoveAt(0); 
                break;
            case 5:
                fetchedGO = wildsPool[0];
                wildsPool.RemoveAt(0);
                break;
            case 6:
                fetchedGO = bonusPool[0];
                bonusPool.RemoveAt(0); 
                break;
        }
        return fetchedGO;
    }

   

    public void AddToPool(Symbol poolType, GameObject objectToAdd)
    {
        int poolID = (int) poolType;

        switch (poolID)
        {

            case 0:
                tensPool.Add(objectToAdd);
                break;
            case 1:
                jacksPool.Add(objectToAdd);
                break;
            case 2:
                queensPool.Add(objectToAdd);
                break;
            case 3:
                kingsPool.Add(objectToAdd);
                break;
            case 4:
                acesPool.Add(objectToAdd);
                break;
            case 5:
                wildsPool.Add(objectToAdd);
                break;
            case 6:
                bonusPool.Add(objectToAdd);
                break;
        }
    }

}
