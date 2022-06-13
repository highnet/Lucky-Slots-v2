using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetAwaiter : MonoBehaviour
{
    private GameState gameState;
    private void Awake()
    {
        gameState = GameObject.FindGameObjectWithTag("Game State").GetComponent<GameState>();
    }

    public bool GetSpinInput()
    {
        bool input = Input.GetKeyUp(KeyCode.Return);
        return input;
    }
}
