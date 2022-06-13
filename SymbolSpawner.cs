using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SymbolSpawner : MonoBehaviour
{
    private GameSymbolPool gameSymbolPool;
    private SlotsAnchors slotsAnchors;
    private GameState gameState;
    private SpinGenerator spinGenerator;
    private bool spawningFakeSymbols;

    private void Awake()
    {
        gameSymbolPool = GameObject.FindGameObjectWithTag("Game Symbol Pool").GetComponent<GameSymbolPool>();
        slotsAnchors = GameObject.FindGameObjectWithTag("Slots Anchors").GetComponent<SlotsAnchors>();
        gameState = GameObject.FindGameObjectWithTag("Game State").GetComponent<GameState>();
        spinGenerator = GameObject.FindGameObjectWithTag("Spin Generator").GetComponent<SpinGenerator>();
    }

    public void SpawnRealSymbols()
    {
        StartCoroutine(SpawnRealSymbolsCoroutine());
        StartCoroutine(ProceedToNextStateAfterSeconds(2.0f));
    }

    private IEnumerator SpawnRealSymbolsCoroutine()
    {
        SpinDatum spinDatum = spinGenerator.GetSpinDatum();
        Symbol[,] spinnedSymbols = spinDatum.GetSpinnedSymbols();
        
        for (int i = SlotsAttributes.GetNumberOfRows()-1; i >= 0 ; i--)
        {
            for (int j = 0; j < SlotsAttributes.GetNumberOfReels(); j++)
            {
                yield return new WaitForSeconds(.04f);

                GameObject gameSymbolGO = gameSymbolPool.FetchFromPool(spinnedSymbols[i, j]);
                GameSymbol gameSymbol = gameSymbolGO.GetComponent<GameSymbol>();
                gameSymbol.MoveToPosition(slotsAnchors.GetStartAnchors()[j].transform.position);
                gameSymbol.SetTweenParameters(slotsAnchors.GetSymbolAnchors()[i, j].transform.position, .8f);

            }

        }
        
    }

    private IEnumerator ProceedToNextStateAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        gameState.SetTrigger("Spawned Real Symbols");

    }

    private IEnumerator SpawnFakeSymbolsCoroutine()
    {
        while (spawningFakeSymbols)
        {
            for (int i = 0; i < SlotsAttributes.GetNumberOfReels(); i++)
            {
                yield return new WaitForSeconds(.04f);

                GameObject gameSymbolGO = gameSymbolPool.FetchFromPool((Symbol)UnityEngine.Random.Range(0, SlotsAttributes.GetNumberOfSymbols()));
                GameSymbol gameSymbol = gameSymbolGO.GetComponent<GameSymbol>();
                gameSymbol.MoveToPosition(slotsAnchors.GetStartAnchors()[i].transform.position);
                gameSymbol.SetTweenParameters(slotsAnchors.GetEndAnchors()[i].transform.position, .8f);
                gameSymbol.SetIsFakeSymbol(true);


            }
        }
    }

    public void SpawnFakeSymbols()
    {
        spawningFakeSymbols = true;
        StartCoroutine(SpawnFakeSymbolsCoroutine());
        StartCoroutine(StopSpawningFakeSymbolsAfterSeconds(1.5f));
    }

    private IEnumerator StopSpawningFakeSymbolsAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        spawningFakeSymbols = false;
        gameState.SetTrigger("Spawned Fake Symbols");
    }
}
