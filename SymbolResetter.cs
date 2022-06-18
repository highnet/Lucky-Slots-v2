using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SymbolResetter : MonoBehaviour
{
    private ActiveSymbols activeSymbols;
    private GameState gameState;

    private void Awake()
    {
        activeSymbols = GameObject.FindGameObjectWithTag("Active Symbols").GetComponent<ActiveSymbols>();
        gameState = GameObject.FindGameObjectWithTag("Game State").GetComponent<GameState>();

    }


    public void ResetActiveSymbols()
    {
        StartCoroutine(ResetActiveSymbolsCoroutine());
        StartCoroutine(TransitionToNextStateAfterSeconds(2.0f));
    }

    private IEnumerator TransitionToNextStateAfterSeconds(float seconds)
    {
        if (activeSymbols.GetActiveGameSymbols().Count > 0)
        {
            yield return new WaitForSeconds(seconds);

        } 
        gameState.SetTrigger("Resetted Symbols");
    }

    private IEnumerator ResetActiveSymbolsCoroutine()
    {
        List<GameObject> activeGameSymbols = activeSymbols.GetActiveGameSymbols(); 
        for(int i = 0; i < activeGameSymbols.Count; i++)
        {
            GameSymbol gameSymbol = activeGameSymbols[i].GetComponent<GameSymbol>();
            gameSymbol.SetTweenParameters(gameSymbol.GetResetPosition(), .5f - (i * .01f));
            gameSymbol.SetIsFakeSymbol(true);
            gameSymbol.SetForceTween(true);
            yield return new WaitForSeconds(.02f);


        }
    }
}
