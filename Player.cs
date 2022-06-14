using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float bet;
    [SerializeField]
    private float balance;


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
    }

    public bool CanSetBet()
    {
        return bet <= balance;
    }
}
