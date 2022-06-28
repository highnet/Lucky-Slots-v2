using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSymbols : MonoBehaviour
{
    [SerializeField]
    private GameObject[,] activeSymbols; // TODO: change this to a 2d object array
    private GameState gameState;
    private void Awake()
    {
        activeSymbols = new GameObject[SlotsAttributes.GetNumberOfRows(), SlotsAttributes.GetNumberOfReels()];
        gameState = GameObject.FindGameObjectWithTag("Game State").GetComponent<GameState>();
    }

    public GameObject[,] GetActiveGameSymbols()
    {
        return activeSymbols;
    }

    public void AddToActiveSymbols(GameObject objectToAdd,int row, int reel) 
    {
        activeSymbols[row, reel] = objectToAdd;
    }
    public void RemoveFromActiveSymbols(int row, int reel)
    {
        activeSymbols[row, reel] = null;
    }

    public void Reset()
    {
        activeSymbols = new GameObject[SlotsAttributes.GetNumberOfRows(), SlotsAttributes.GetNumberOfReels()];

    }
    public bool HasActiveTween()
    {

        for (int i = 0; i < SlotsAttributes.GetNumberOfRows(); i++)
        {
            for(int j = 0; j < SlotsAttributes.GetNumberOfReels(); j++)
            {
                if (activeSymbols[i,j] != null)
                {
                    GameSymbol gameSymbol = activeSymbols[i, j].GetComponent<GameSymbol>();
                    if (gameSymbol.GetTween() != null)
                    {
                        return true;
                    }
                }

            }

        }
        return false;
    }

    private void Update()
    {
        gameState.SetBool("Active Symbols Has Active Tweens", HasActiveTween());
    }
}
