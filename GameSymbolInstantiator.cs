using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSymbolInstantiator : MonoBehaviour
{

    private Vector3 pooledSymbolsSpawnPoint;
    [SerializeField]
    private GameObject[] gameSymbolPrefabs;
    private GameSymbolPool gameSymbolPool;

    private void Awake()
    {
        gameSymbolPool = GameObject.FindGameObjectWithTag("Game Symbol Pool").GetComponent<GameSymbolPool>();
        pooledSymbolsSpawnPoint = new Vector3(50f, 50f, 0f);
        InsantiateAndPoolAllSymbols();
    }

    private void InsantiateAndPoolAllSymbols()
    {

        for (int i = 0; i < gameSymbolPrefabs.Length; i++)
        {
            for(int j = 0; j < 50; j++)
            {
                GameObject spawnedGameSymbol = GameObject.Instantiate(gameSymbolPrefabs[i], pooledSymbolsSpawnPoint, Quaternion.identity);
                gameSymbolPool.AddToPool((Symbol)i, spawnedGameSymbol);
            }
        }
    }
}
