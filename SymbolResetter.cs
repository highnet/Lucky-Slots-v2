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
        StartCoroutine(TransitionToNextStateAfterSeconds(.5f));
    }

    private IEnumerator TransitionToNextStateAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        gameState.SetTrigger("Resetted Symbols");
    }

    private IEnumerator ResetActiveSymbolsCoroutine()
    {
        GameObject[,] activeGameSymbols = activeSymbols.GetActiveGameSymbols(); 
        for(int i = 0; i < SlotsAttributes.GetNumberOfReels(); i++)
        {
            for(int j = 0; j < SlotsAttributes.GetNumberOfRows(); j++)
            {
                if (activeGameSymbols[j,i] != null)
                {
                    GameSymbol gameSymbol = activeGameSymbols[j, i].GetComponent<GameSymbol>();
                    gameSymbol.SetTweenParameters(gameSymbol.GetResetPosition(), .5f - (.01f * i) - (0.05f * j));
                    gameSymbol.SetIsFakeSymbol(true);
                    gameSymbol.SetForceTween(true);
                }

                yield return new WaitForSeconds(.02f);
            }
            yield return new WaitForSeconds(.04f);

        }

        activeSymbols.Reset();
    }
}
