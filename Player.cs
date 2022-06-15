using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float bet = 0.1f;
    [SerializeField]
    private float balance = 100f;
    private UI ui;

    private void Awake()
    {
        ui = GameObject.FindGameObjectWithTag("UI").GetComponent<UI>();
    }

    public void AwardBalance(float winnings)
    {
        balance += winnings;
        ui.UpdateBalanceText();
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
}
