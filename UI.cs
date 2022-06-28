using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using DG.Tweening;

public class UI : MonoBehaviour
{
    private Player player;
    [SerializeField]
    private TextMeshProUGUI betText;
    [SerializeField]
    private TextMeshProUGUI balanceText;
    [SerializeField]
    private TextMeshProUGUI winningsText;
    [SerializeField]
    private Button changeBetButton;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public void TweenWinningsText()
    {
        winningsText.rectTransform.DOPunchScale(new Vector3(.4f, .4f, 0f), .3f, 2, 0f);
    }

    public void MakeChangeBetButtonInteractable(bool predicate)
    {
        changeBetButton.interactable = predicate;
    }

    private void Start()
    {
        UpdateBetText();
        UpdateBalanceText();
    }

    public void UpdateWinningsText()
    {
        winningsText.text = "$ " + player.GetPreviousWinnings().ToString();
    }

    public void UpdateBetText()
    {
        betText.text = "BET $ " + player.GetBet();
    }

    public void UpdateBalanceText()
    {
        balanceText.text = "BALANCE $ " + player.GetBalance();
    }

    public void ResetWinningText()
    {
        winningsText.text = "";
    }
}
