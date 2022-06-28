using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WinningSymbolAnimator : MonoBehaviour
{
    private ActiveSymbols activeSymbols;
    private PayoutCalculator payoutCalculator;
    private GameState gameState;


    private void Awake()
    {
        activeSymbols = GameObject.FindGameObjectWithTag("Active Symbols").GetComponent<ActiveSymbols>();
        payoutCalculator = GameObject.FindGameObjectWithTag("Payout Calculator").GetComponent<PayoutCalculator>();
        gameState = GameObject.FindGameObjectWithTag("Game State").GetComponent<GameState>();

    }

    public IEnumerator AnimateWinningSymbolsCoroutine()
    {

        List<Vector2> uniqueVertices = new List<Vector2>();
        GameObject[,] symbols = activeSymbols.GetActiveGameSymbols();

        Dictionary<Symbol, List<List<Vector2>>> winnerPayoutPaths = payoutCalculator.GetWinnerPayoutPaths(0);

        for (int i = 0; i < 5; i++)
        {
            List<List<Vector2>> payoutPaths = winnerPayoutPaths[(Symbol)i];
            foreach (List<Vector2> path in payoutPaths)
            {
                foreach(Vector2 vertex in path)
                {
                    if (!uniqueVertices.Contains(vertex))
                    {
                        uniqueVertices.Add(vertex);
                    }
                }
            }

        }

        foreach(Vector2 vertex in uniqueVertices)
        {
            GameObject go = symbols[(int)vertex.x, (int)vertex.y];
            go.transform.DOPunchScale(new Vector3(.4f, .4f, 0f),.3f, 2, 0f);
            yield return new WaitForSeconds(.1f);
        }
    }
    public IEnumerator TransitionToNextStateAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        gameState.SetTrigger("Animated Winning Symbols");

    }

    public void AnimateWinningSymbols()
    {
        StartCoroutine(AnimateWinningSymbolsCoroutine());
        StartCoroutine(TransitionToNextStateAfterSeconds(.5f));

    }
}
