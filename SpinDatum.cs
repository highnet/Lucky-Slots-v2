using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Symbol { Ten, Jack, Queen, King, Ace, Wild, Bonus};
public class SpinDatum
{
    private Symbol[,] spinnedSymbolsWithWilds;
    private List<Symbol[,]> spinnedSymbolsReplacedWilds;
    public SpinDatum()
    {
        spinnedSymbolsWithWilds = GenerateRoll();
     //   PrintSpinnedSymbolsWithWilds();
        spinnedSymbolsReplacedWilds = ReplaceWilds();
   //     PrintSpinnedSymbolsWithWildsReplaced();
    }

    private void PrintSpinnedSymbolsWithWildsReplaced()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < SlotsAttributes.GetNumberOfRows(); j++)
            {
                for (int k = 0; k < SlotsAttributes.GetNumberOfReels(); k++)
                {
                    Debug.Log(i + "-> (" + j + "," + k + "): " + spinnedSymbolsReplacedWilds[i][j,k]);

                }
            }
        }
    }

    public List<Symbol[,]> GetSpinnedSymbolsWithWildsReplaced()
    {
        return spinnedSymbolsReplacedWilds;
    }

    public List<Symbol[,]> ReplaceWilds()
    {
        List<Symbol[,]> listOfSymbolsArrays = new List<Symbol[,]>();

        for (int i = 0; i < 5; i++)
        {
            listOfSymbolsArrays.Add(new Symbol[SlotsAttributes.GetNumberOfRows(), SlotsAttributes.GetNumberOfReels()]);
            for (int j = 0; j < SlotsAttributes.GetNumberOfRows(); j++)
            {
                for (int k = 0; k < SlotsAttributes.GetNumberOfReels(); k++)
                {
                    if (spinnedSymbolsWithWilds[j, k] == Symbol.Wild)
                    {
                        listOfSymbolsArrays[i][j, k] = (Symbol)i;
                    } else
                    {
                        listOfSymbolsArrays[i][j, k] = spinnedSymbolsWithWilds[j, k];

                    }
                }
            }
        }

        return listOfSymbolsArrays;
    }

    public Symbol[,] GetSpinnedSymbolsWithWilds()
    {
        return spinnedSymbolsWithWilds;
    }

    private void PrintSpinnedSymbolsWithWilds()
    {
        for (int i = 0; i < SlotsAttributes.GetNumberOfRows(); i++)
        {
            for (int j = 0; j < SlotsAttributes.GetNumberOfReels(); j++)
            {
                Debug.Log("(" + i + "," + j + "): " + spinnedSymbolsWithWilds[i, j]);
            }
        }
    }

    private Symbol[,] GenerateRoll()
    {
        int rng = 0;
        Symbol symbol = Symbol.Ten;
        Symbol[,] symbolsArray = new Symbol[SlotsAttributes.GetNumberOfRows(), SlotsAttributes.GetNumberOfReels()];
        int tenThreshold = 450;
        int jackThreshold = 550;
        int queenThreshold = 750;
        int kingThreshold = 880;
        int aceThrehsold = 970;
        int wildThreshold = 990;
        int bonusThreshold = 999;


        for(int i = 0; i < SlotsAttributes.GetNumberOfRows(); i++)
        {
            for (int j = 0; j < SlotsAttributes.GetNumberOfReels(); j++)
            {
                rng = UnityEngine.Random.Range(0, 999);
                Debug.Log(rng);
                if (rng < tenThreshold)
                {
                    symbol = Symbol.Ten;
                } else if (rng < jackThreshold)
                {
                    symbol = Symbol.Jack;
                } else if (rng < queenThreshold)
                {
                    symbol = Symbol.Queen;
                } else if (rng < kingThreshold)
                {
                    symbol = Symbol.King;
                } else if (rng < aceThrehsold)
                {
                    symbol = Symbol.Ace;
                } else if (rng < wildThreshold)
                {
                    symbol = Symbol.Wild;
                } else
                {
                    symbol = Symbol.Bonus;
                }
                Debug.Log(symbol);
                symbolsArray[i, j] = symbol;
            }
        }
        return symbolsArray;
    }
}
