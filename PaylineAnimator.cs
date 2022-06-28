using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PaylineAnimator : MonoBehaviour
{
    private PayoutCalculator payoutCalculator;
    private GameState gameState;
    private PaylineSpawner paylineSpawner;

    private void Awake()
    {
        gameState = GameObject.FindGameObjectWithTag("Game State").GetComponent<GameState>();
        payoutCalculator = GameObject.FindGameObjectWithTag("Payout Calculator").GetComponent<PayoutCalculator>();
        paylineSpawner = GameObject.FindGameObjectWithTag("Payline Spawner").GetComponent<PaylineSpawner>();
    }
        public void AnimatePaylines()
    {
        Dictionary<Symbol, List<List<Vector2>>> winnerPayoutPaths5 = payoutCalculator.GetWinnerPayoutPaths(5);
        Dictionary<Symbol, List<List<Vector2>>> winnerPayoutPaths4 = payoutCalculator.GetWinnerPayoutPaths(4);
        Dictionary<Symbol, List<List<Vector2>>> winnerPayoutPaths3 = payoutCalculator.GetWinnerPayoutPaths(3);


        for (int i = 0; i < 5; i++)
        {
            foreach(List<Vector2> path in winnerPayoutPaths5[(Symbol)i])
            {
                GameObject paylineGO = paylineSpawner.FetchFromPaylinesPool();
                paylineSpawner.AddToActivePaylines(paylineGO);
                Payline payline = paylineGO.GetComponent<Payline>();
                payline.SetPath(path);
                payline.RenderPayline();
            }

            foreach (List<Vector2> path in winnerPayoutPaths4[(Symbol)i])
            {
                GameObject paylineGO = paylineSpawner.FetchFromPaylinesPool();
                paylineSpawner.AddToActivePaylines(paylineGO);
                Payline payline = paylineGO.GetComponent<Payline>();
                payline.SetPath(path);
                payline.RenderPayline();
            }
            foreach (List<Vector2> path in winnerPayoutPaths3[(Symbol)i])
            {
                GameObject paylineGO = paylineSpawner.FetchFromPaylinesPool();
                paylineSpawner.AddToActivePaylines(paylineGO);
                Payline payline = paylineGO.GetComponent<Payline>();
                payline.SetPath(path);
                payline.RenderPayline();
            }


        }
        gameState.SetTrigger("Animated Paylines");


    }


}
