using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetHandler : MonoBehaviour
{
    private GameState gameState;
    private Player player;
    private bool playerCanSetBet;

    private void Awake()
    {
        gameState = GameObject.FindGameObjectWithTag("Game State").GetComponent<GameState>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        playerCanSetBet = player.CanSetBet();
        gameState.SetBool("Player Can Place Bet", playerCanSetBet);

    }

    public void TryHandleBet()
    {
       if (playerCanSetBet)
        {
            player.PlaceBet();
            gameState.SetTrigger("Handled Bet");
        } else
        {
            gameState.SetTrigger("Not Enough Balance");

        }
    }
}
