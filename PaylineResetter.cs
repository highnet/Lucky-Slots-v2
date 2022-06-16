using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaylineResetter : MonoBehaviour
{
    private PaylineSpawner paylineSpawner;
    private GameState gameState;

    private void Awake()
    {
        paylineSpawner = GameObject.FindGameObjectWithTag("Payline Spawner").GetComponent<PaylineSpawner>();
        gameState = GameObject.FindGameObjectWithTag("Game State").GetComponent<GameState>();
    }

    public void ResetPaylines()
    {
        List<GameObject> activePaylines = paylineSpawner.GetActivePaylines();

        for(int i = 0; i < activePaylines.Count; i++)
        {
            activePaylines[i].GetComponent<Payline>().ResetPayline();
            paylineSpawner.AddToPaylinesPool(activePaylines[i]);
        }
        gameState.SetTrigger("Resetted Paylines");
    }
}
