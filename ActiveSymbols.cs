using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSymbols : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> activeSymbols;
    private void Awake()
    {
        activeSymbols = new List<GameObject>();
    }

    public List<GameObject> GetActiveSymbols()
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

        for(int i = 0; i < activeSymbols.Count; i++)
        {
            GameSymbol gameSymbol = activeSymbols[i].GetComponent<GameSymbol>();
            if (gameSymbol.GetTween() != null)
            {
                return true;
            }
        }
        return false;
    }

}
