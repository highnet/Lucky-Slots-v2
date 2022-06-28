using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinningTextAnimator : MonoBehaviour
{

    private GameState gameState;
    private UI ui;

    private void Awake()
    {
        gameState = GameObject.FindGameObjectWithTag("Game State").GetComponent<GameState>();
        ui = GameObject.FindGameObjectWithTag("UI").GetComponent<UI>();
    }

    public void AnimateWinningText()
    {
        ui.UpdateWinningsText();
        ui.TweenWinningsText();
        StartCoroutine(TransitionToNextStateAfterSeconds(1.0f));
    }

    public IEnumerator TransitionToNextStateAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        gameState.SetTrigger("Animated Winning Text");
    }
}
