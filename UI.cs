using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI : MonoBehaviour
{
    private Player player;
    [SerializeField]
    private TextMeshProUGUI betText;
    [SerializeField]
    private TextMeshProUGUI balanceText;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Start()
    {
        UpdateBetText();
        UpdateBalanceText();
    }

    public void UpdateBetText()
    {
        betText.text = "BET $ " + player.GetBet();
    }

    public void UpdateBalanceText()
    {
        balanceText.text = "BALANCE $ " + player.GetBalance();
    }
}
