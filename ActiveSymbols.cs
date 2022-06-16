using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSymbols : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> activeSymbols;
    private GameState gameState;
    private void Awake()
    {
        activeSymbols = new List<GameObject>();
        gameState = GameObject.FindGameObjectWithTag("Game State").GetComponent<GameState>();
    }

    public List<GameObject> GetActiveGameSymbols()
    {
        return activeSymbols;
    }

    public void AddToActiveSymbols(GameObject objectToAdd) 
    {
        activeSymbols.Add(objectToAdd);
    }
    public void RemoveFromActiveSymbols(GameObject objectToRemove)
    {
        activeSymbols.Remove(objectToRemove);
    }
    public bool HasActiveTween()
    {

        for (int i = 0; i < activeSymbols.Count; i++)
        {
            GameSymbol gameSymbol = activeSymbols[i].GetComponent<GameSymbol>();
            if (gameSymbol.GetTween() != null)
            {
                return true;
            }
        }
        return false;
    }

    private void Update()
    {
        gameState.SetBool("Active Symbols Has Active Tweens", HasActiveTween());
    }
}
