using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Symbol { Ten, Jack, Queen, King, Ace, Wild, Bonus};
public class SpinDatum
{
    private Symbol[,] spinnedSymbols;

    public SpinDatum()
    {
        spinnedSymbols = GenerateRoll();
        PrintSpinnedSymbols();
    }

    public Symbol[,] GetSpinnedSymbols()
    {
        return spinnedSymbols;
    }

    private void PrintSpinnedSymbols()
    {
        for (int i = 0; i < SlotsAttributes.GetNumberOfRows(); i++)
        {
            for (int j = 0; j < SlotsAttributes.GetNumberOfReels(); j++)
            {
                Debug.Log("(" + i + "," + j + "): " + spinnedSymbols[i, j]);
            }
        }
    }

    private Symbol[,] GenerateRoll()
    {
        Symbol[,] symbols = new Symbol[SlotsAttributes.GetNumberOfRows(), SlotsAttributes.GetNumberOfReels()];


        for(int i = 0; i < SlotsAttributes.GetNumberOfRows(); i++)
        {
            for (int j = 0; j < SlotsAttributes.GetNumberOfReels(); j++)
            {
                symbols[i, j] = (Symbol)UnityEngine.Random.Range(0, SlotsAttributes.GetNumberOfSymbols());
            }
        }
        return symbols;
    }
}
