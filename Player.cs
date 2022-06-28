using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private UI ui;

    [SerializeField]
    private float bet;
    [SerializeField]
    private float balance;
    [SerializeField]
    private float previousWinnings = 0f;
    [SerializeField]
    private float previousMultiplier = 0f;
    private void Awake()
    {
        ui = GameObject.FindGameObjectWithTag("UI").GetComponent<UI>();
    }

    public void AwardBalance(float award)
    {
        previousWinnings = award;
        balance += award;
        ui.UpdateBalanceText();
    }

    public void SetPreviousWinnings(float award)
    {
        previousWinnings = award;
    }

    public float GetPreviousWinnings()
    {
        return previousWinnings;
    }

    public float GetBet()
    {
        return bet;
    }

    public float GetBalance()
    {
        return balance;
    }

    public void PlaceBet()
    {
        balance -= bet;
        ui.UpdateBalanceText();
    }

    public bool CanSetBet()
    {
        return bet <= balance;
    }

    public void CycleBet()
    {
        switch (bet)
        {
            case (.1f):
                bet = .5f;
                break;
            case (.5f):
                bet = 1f;
                break;
            case (1f):
                bet = 5f;
                break;
            case (5f):
                bet = 10f;
                break;
            case (10f):
                bet = 50f;
                break;
            case (50f):
                bet = 100f;
                break;
            case (100f):
                bet = 500f;
                break;
            case (500f):
                bet = 1000f;
                break;
            case (1000f):
                bet = .1f;
                break;
        }
        ui.UpdateBetText();
            
    }
}
