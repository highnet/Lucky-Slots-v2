using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PayoutCalculator : MonoBehaviour
{
    private GameState gameState;
    private SpinGenerator spinGenerator;
    private PaylinePathGenerator paylinePathGenerator;
    private List<string> payoutTallies;
    private Dictionary<Symbol, List<List<Vector2>>> matchedPaylinesAnySize;
    private Dictionary<Symbol, List<List<Vector2>>> matchedPaylinesSize5;
    private Dictionary<Symbol, List<List<Vector2>>> matchedPaylinesSize4;
    private Dictionary<Symbol, List<List<Vector2>>> matchedPaylinesSize3;
    private Player player;

    private void Awake()
    {
        gameState = GameObject.FindGameObjectWithTag("Game State").GetComponent<GameState>();
        spinGenerator = GameObject.FindGameObjectWithTag("Spin Generator").GetComponent<SpinGenerator>();
        paylinePathGenerator = GameObject.FindGameObjectWithTag("Payline Path Generator").GetComponent<PaylinePathGenerator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        Reset();

    }

    public void Reset()
    {
        payoutTallies = new List<string>();
        matchedPaylinesAnySize = new Dictionary<Symbol, List<List<Vector2>>>();
        matchedPaylinesSize5 = new Dictionary<Symbol, List<List<Vector2>>>();
        matchedPaylinesSize4 = new Dictionary<Symbol, List<List<Vector2>>>();
        matchedPaylinesSize3 = new Dictionary<Symbol, List<List<Vector2>>>();

        for (int i = 0; i < 5; i++)
        {
            matchedPaylinesAnySize.Add((Symbol)i, new List<List<Vector2>> ());
            matchedPaylinesSize5.Add((Symbol)i, new List<List<Vector2>>());
            matchedPaylinesSize4.Add((Symbol)i, new List<List<Vector2>>());
            matchedPaylinesSize3.Add((Symbol)i, new List<List<Vector2>>());

        }
    }

    public Dictionary<Symbol, List<List<Vector2>>> GetWinnerPayoutPaths(int size)
    {
        if (size == 5)
        {
            return matchedPaylinesSize5;
        } else if (size == 4)
        {
            return matchedPaylinesSize4;
        } else if (size == 3)
        {
            return matchedPaylinesSize3;
        }
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

            foreach(List<Vector2> path in matchedPaylinesSize5[scanSymbol])
            {
                payoutTallies.Add("" + path.Count + " " + scanSymbol);
            }
            foreach (List<Vector2> path in matchedPaylinesSize4[scanSymbol])
            {
                payoutTallies.Add("" + path.Count + " "+ scanSymbol);
            }
            foreach (List<Vector2> path in matchedPaylinesSize3[scanSymbol])
            {
                payoutTallies.Add("" + path.Count + " " + scanSymbol);
            }

        }

        float multiplier = 0f;
        float bet = player.GetBet();

        foreach (string tally in payoutTallies)
        {
            Debug.Log(tally);
            switch (tally)
            {
                case ("3 Ten"):
                    multiplier += .25f;
                    break;
                case ("4 Ten"):
                    multiplier += 1f;
                    break;
                case ("5 Ten"):
                    multiplier += 5f;
                    break;
                case ("3 Jack"):
                    multiplier += .5f;
                    break;
                case ("4 Jack"):
                    multiplier += 2f;
                    break;
                case ("5 Jack"):
                    multiplier += 10f;
                    break;
                case ("3 Queen"):
                    multiplier += 1f;
                    break;
                case ("4 Queen"):
                    multiplier += 4f;
                    break;
                case ("5 Queen"):
                    multiplier += 20f;
                    break;
                case ("3 King"):
                    multiplier += 2f;
                    break;
                case ("4 King"):
                    multiplier += 8f;
                    break;
                case ("5 King"):
                    multiplier += 40f;
                    break;
                case ("3 Ace"):
                    multiplier += 4;
                    break;
                case ("4 Ace"):
                    multiplier += 12;
                    break;
                case ("5 Ace"):
                    multiplier += 56;
                    break;

            }

                
        }
        Debug.Log("Bet: " + bet);
        Debug.Log("Multiplier: " + multiplier);
        float winnings = bet * multiplier;
        player.AwardBalance(winnings);

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

