using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PayoutCalculator : MonoBehaviour
{
    private GameState gameState;
    private SpinGenerator spinGenerator;
    private PaylinePathGenerator paylinePathGenerator;
    private Dictionary<Symbol, List<string>> payoutTallies  ;
    private Dictionary<Symbol, List<List<Vector2>>> matchedPaylinesAnySize;
    private Dictionary<Symbol, List<List<Vector2>>> matchedPaylinesSize5;
    private Dictionary<Symbol, List<List<Vector2>>> matchedPaylinesSize4;
    private Dictionary<Symbol, List<List<Vector2>>> matchedPaylinesSize3;

    private void Awake()
    {
        gameState = GameObject.FindGameObjectWithTag("Game State").GetComponent<GameState>();
        spinGenerator = GameObject.FindGameObjectWithTag("Spin Generator").GetComponent<SpinGenerator>();
        paylinePathGenerator = GameObject.FindGameObjectWithTag("Payline Path Generator").GetComponent<PaylinePathGenerator>();
        Reset();

    }

    public void Reset()
    {
        payoutTallies = new Dictionary<Symbol, List<string>>();
        matchedPaylinesAnySize = new Dictionary<Symbol, List<List<Vector2>>>();
        matchedPaylinesSize5 = new Dictionary<Symbol, List<List<Vector2>>>();
        matchedPaylinesSize4 = new Dictionary<Symbol, List<List<Vector2>>>();
        matchedPaylinesSize3 = new Dictionary<Symbol, List<List<Vector2>>>();

        for (int i = 0; i < 5; i++)
        {
            payoutTallies.Add((Symbol)i, new List<string>());
            matchedPaylinesAnySize.Add((Symbol)i, new List<List<Vector2>> ());
            matchedPaylinesSize5.Add((Symbol)i, new List<List<Vector2>>());
            matchedPaylinesSize4.Add((Symbol)i, new List<List<Vector2>>());
            matchedPaylinesSize3.Add((Symbol)i, new List<List<Vector2>>());

        }
    }

    public Dictionary<Symbol, List<List<Vector2>>> GetWinnerPayoutPaths()
    {
        return matchedPaylinesAnySize;
    }




    public void CalculatePayout()
    {

        Reset();

        SpinDatum spinDatum = spinGenerator.GetSpinDatum();
        List<Symbol[,]> spinnedSymbolsWithWildsReplaced = spinDatum.GetSpinnedSymbolsWithWildsReplaced();

        List<List<Vector2>> totalPayoutPaths = paylinePathGenerator.GetTotalPathsv2();


        for (int symbolID = 0; symbolID < 5; symbolID++)
        {
            Symbol scanSymbol = (Symbol)symbolID;
            Symbol[,] spinnedSymbols = spinnedSymbolsWithWildsReplaced[symbolID];


            foreach (List<Vector2> path in totalPayoutPaths)
            {
                bool pathSuccess = true;

                foreach (Vector2 vertex in path)
                {
                    if (spinnedSymbols[(int) vertex.x, (int) vertex.y] != scanSymbol)
                    {
                        pathSuccess = false;
                        break;
                    }
                }
                if (pathSuccess)
                {
                    matchedPaylinesAnySize[scanSymbol].Add(path);
                }

            }



            foreach(List<Vector2> path in matchedPaylinesAnySize[scanSymbol])
            {
                if (path.Count == 5)
                {
                    matchedPaylinesSize5[scanSymbol].Add(path);
                } else if (path.Count == 4)
                {
                    matchedPaylinesSize4[scanSymbol].Add(path);
                } else if (path.Count == 3)
                {
                    matchedPaylinesSize3[scanSymbol].Add(path);
                }

            }


            List<List<Vector2>> dirtyPaths = new List<List<Vector2>>();

            foreach(List<Vector2> path5 in matchedPaylinesSize5[scanSymbol])
            {
                foreach(List<Vector2> path4 in matchedPaylinesSize4[scanSymbol])
                {
                    if (NumberOfSharedVertices(path5,path4) == 4)
                    {
                        dirtyPaths.Add(path4);
                            
                    }
                }

                foreach (List<Vector2> path3 in matchedPaylinesSize3[scanSymbol])
                {
                    if (NumberOfSharedVertices(path5,path3) == 3)
                    {
                        dirtyPaths.Add(path3);
                    }
                }
            }

            foreach(List<Vector2> path4 in matchedPaylinesSize4[scanSymbol])
            {
                foreach (List<Vector2> path3 in matchedPaylinesSize3[scanSymbol])
                {
                    if (NumberOfSharedVertices(path4,path3) == 3)
                    {
                        dirtyPaths.Add(path3);
                    }
                }
            }

            foreach(List<Vector2> path in dirtyPaths)
            {
                if (matchedPaylinesSize4[scanSymbol].Contains(path))
                {
                    matchedPaylinesSize4[scanSymbol].Remove(path);
                }
                if (matchedPaylinesSize3[scanSymbol].Contains(path))
                {
                    matchedPaylinesSize3[scanSymbol].Remove(path);
                }
            }

            Debug.Log(scanSymbol + " paths length 5: " + matchedPaylinesSize5[scanSymbol].Count);
            Debug.Log(scanSymbol + " paths length 4: " + matchedPaylinesSize4[scanSymbol].Count);
            Debug.Log(scanSymbol + " paths length 3: " + matchedPaylinesSize3[scanSymbol].Count);

            foreach(List<Vector2> path in matchedPaylinesSize5[scanSymbol])
            {
                payoutTallies[scanSymbol].Add("" + path.Count + scanSymbol);
            }

        }


        // TODO: Award Balance according to Tally

        gameState.SetTrigger("Calculated Payout");

 
        }

    private int NumberOfSharedVertices(List<Vector2> path1, List<Vector2> path2)
    {
        int sharedVertices = 0;
        foreach(Vector2 vertex in path1)
        {
            if (path2.Contains(vertex))
            {
                sharedVertices++;
            }
        }
        return sharedVertices;
    }

}

