using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaylineSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject paylinePrefab;
    [SerializeField]
    private List<GameObject> activePaylines;
    [SerializeField]
    private List<GameObject> paylinesPool;

    private void Awake()
    {
        activePaylines = new List<GameObject>();
        paylinesPool = new List<GameObject>();

        for(int i = 0; i < 379; i++)
        {
            GameObject paylineGO = GameObject.Instantiate(paylinePrefab,new Vector3(50f,50f,50f),Quaternion.identity);
            AddToPaylinesPool(paylineGO);
        }
    }

    public GameObject FetchFromPaylinesPool()
    {
        GameObject payline = paylinesPool[0];
        paylinesPool.RemoveAt(0);
        return payline;
    }

    public void AddToPaylinesPool(GameObject payline)
    {
        paylinesPool.Add(payline);
    }

    public void AddToActivePaylines(GameObject payline)
    {
        activePaylines.Add(payline);
    }

    public List<GameObject> GetActivePaylines()
    {
        return activePaylines;
    }

    public List<GameObject> GetPaylinesPool()
    {
        return paylinesPool;
    }

    public int ActivePaylinesCount()
    {
        return activePaylines.Count;
    }
}
