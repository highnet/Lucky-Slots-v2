using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Dictionary<Symbol, List<Vector2>> payoutReconstructedPaths = payoutCalculator.GetPayoutReconstructedPaths();

        for(int i = 0; i < 5; i++)
        {
            if (payoutReconstructedPaths[(Symbol) i].Count > 2)
            {

                GameObject paylineGO = paylineSpawner.FetchFromPaylinesPool();
                paylineSpawner.AddToActivePaylines(paylineGO);
                Payline payline = paylineGO.GetComponent<Payline>();
                payline.SetPath(payoutReconstructedPaths[(Symbol)i]);
                payline.RenderPayline();
            }
        }
        gameState.SetTrigger("Animated Paylines");

    }
}
