using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PayoutCalculator : MonoBehaviour
{
    private GameState gameState;
    private SpinGenerator spinGenerator;
    private Symbol[,] spinnedSymbols;
    private Symbol scanSymbol;

    private Dictionary<Symbol, int> payoutTallies;
    private Dictionary<Symbol, Vector2> payoutStartingVertex;
    private Dictionary<Symbol, List<Vector2>> payoutReconstructedPaths;

    private void Awake()
    {
        gameState = GameObject.FindGameObjectWithTag("Game State").GetComponent<GameState>();
        spinGenerator = GameObject.FindGameObjectWithTag("Spin Generator").GetComponent<SpinGenerator>();
        Reset();

    }

    public void Reset()
    {
        payoutTallies = new Dictionary<Symbol, int>();
        payoutStartingVertex = new Dictionary<Symbol, Vector2>();
        payoutReconstructedPaths = new Dictionary<Symbol, List<Vector2>>();

        for (int i = 0; i < 5; i++)
        {
            payoutTallies.Add((Symbol)i, 0);
            payoutStartingVertex.Add((Symbol)i, new Vector2(-1f, -1f));
            payoutReconstructedPaths.Add((Symbol)i, new List<Vector2>());
        }
    }

    public Dictionary<Symbol, List<Vector2>> GetPayoutReconstructedPaths()
    {
        return payoutReconstructedPaths;
    }

    public Dictionary<Symbol, int> GetPayoutTally()
    {
        return payoutTallies;
    }

    public void FindHighestPayingVertices(List<Symbol[,]> spinnedSymbolsWithWildsReplaced)
    {
        for (int symbolID = 4; symbolID >= 0; symbolID--)
        {
            scanSymbol = (Symbol)symbolID;
            spinnedSymbols = spinnedSymbolsWithWildsReplaced[symbolID];

            for (int row = 0; row < 4; row++)
            {
                for (int reel = 0; reel < 5; reel++)
                {
                    int maxPathLength = 0;
                    Vector2 startVertex = new Vector2(row, reel);
                    if (spinnedSymbols[(int)startVertex.x, (int)startVertex.y] == scanSymbol)
                    {
                        maxPathLength = Mathf.Max(
                            TraversePathRec(1, new Vector2(startVertex.x, startVertex.y + 1)),
                            TraversePathRec(1, new Vector2(startVertex.x - 1, startVertex.y + 1)),
                            TraversePathRec(1, new Vector2(startVertex.x + 1, startVertex.y + 1))
                            );
                    }

                    if (maxPathLength > 2)
                    {
                        if (payoutTallies[scanSymbol] < maxPathLength)
                        {
                            payoutTallies[scanSymbol] = maxPathLength;
                            payoutStartingVertex[scanSymbol] = startVertex;
                        }
                    }
                }
            }
        }
    }
    private int TraversePathRec(int currentPathLength, Vector2 currentVertex)
    {
        if (currentVertex.x < 0 || currentVertex.x > 3 || currentVertex.y < 0 || currentVertex.y > 4 || spinnedSymbols[(int)currentVertex.x, (int)currentVertex.y] != scanSymbol)
        {
            return currentPathLength;
        }

        return Mathf.Max(
            TraversePathRec(currentPathLength + 1, new Vector2(currentVertex.x, currentVertex.y + 1)),
            TraversePathRec(currentPathLength + 1, new Vector2(currentVertex.x - 1, currentVertex.y + 1)),
            TraversePathRec(currentPathLength + 1, new Vector2(currentVertex.x + 1, currentVertex.y + 1)));
    }
    public void BruteForcePathFromVertexWithRuleset(int ruleset, List<Symbol[,]> spinnedSymbolsWithWildsReplaced, int symbolID)
    {
        Vector2 scanVertex = payoutStartingVertex[(Symbol)symbolID];
        List<Vector2> reconstructedPath = new List<Vector2>();

        bool traversalTerminated = false;
        while (!traversalTerminated)
        {
            reconstructedPath.Add(scanVertex);

            Vector2 goRightUp = new Vector2(scanVertex.x - 1, scanVertex.y + 1);
            Vector2 goRight = new Vector2(scanVertex.x, scanVertex.y + 1);
            Vector2 goRightDown = new Vector2(scanVertex.x + 1, scanVertex.y + 1);

            switch (ruleset)
            {
                case 0:

                    if (goRight.x >= 0 && goRight.x <= 3 && goRight.y >= 0 && goRight.y <= 4 && spinnedSymbolsWithWildsReplaced[symbolID][(int)goRight.x, (int)goRight.y] == (Symbol)symbolID)
                    {
                        scanVertex = goRight;
                    }
                    else if (goRightUp.x >= 0 && goRightUp.x <= 3 && goRightUp.y >= 0 && goRightUp.y <= 4 && spinnedSymbolsWithWildsReplaced[symbolID][(int)goRightUp.x, (int)goRightUp.y] == (Symbol)symbolID)
                    {
                        scanVertex = goRightUp;
                    }
                    else if (goRightDown.x >= 0 && goRightDown.x <= 3 && goRightDown.y >= 0 && goRightDown.y <= 4 && spinnedSymbolsWithWildsReplaced[symbolID][(int)goRightDown.x, (int)goRightDown.y] == (Symbol)symbolID)
                    {
                        scanVertex = goRightDown;
                    }
                    else
                    {
                        traversalTerminated = true;
                    }
                    break;
                case 1:

                    if (goRight.x >= 0 && goRight.x <= 3 && goRight.y >= 0 && goRight.y <= 4 && spinnedSymbolsWithWildsReplaced[symbolID][(int)goRight.x, (int)goRight.y] == (Symbol)symbolID)
                    {
                        scanVertex = goRight;
                    }
                    else if (goRightDown.x >= 0 && goRightDown.x <= 3 && goRightDown.y >= 0 && goRightDown.y <= 4 && spinnedSymbolsWithWildsReplaced[symbolID][(int)goRightDown.x, (int)goRightDown.y] == (Symbol)symbolID)
                    {
                        scanVertex = goRightDown;
                    }
                    else if (goRightUp.x >= 0 && goRightUp.x <= 3 && goRightUp.y >= 0 && goRightUp.y <= 4 && spinnedSymbolsWithWildsReplaced[symbolID][(int)goRightUp.x, (int)goRightUp.y] == (Symbol)symbolID)
                    {
                        scanVertex = goRightUp;
                    }
                    else
                    {
                        traversalTerminated = true;
                    }
                    break;
                case 2:
                    if (goRightUp.x >= 0 && goRightUp.x <= 3 && goRightUp.y >= 0 && goRightUp.y <= 4 && spinnedSymbolsWithWildsReplaced[symbolID][(int)goRightUp.x, (int)goRightUp.y] == (Symbol)symbolID)
                    {
                        scanVertex = goRightUp;
                    }
                    else if (goRight.x >= 0 && goRight.x <= 3 && goRight.y >= 0 && goRight.y <= 4 && spinnedSymbolsWithWildsReplaced[symbolID][(int)goRight.x, (int)goRight.y] == (Symbol)symbolID)
                    {
                        scanVertex = goRight;
                    }
                    else if (goRightDown.x >= 0 && goRightDown.x <= 3 && goRightDown.y >= 0 && goRightDown.y <= 4 && spinnedSymbolsWithWildsReplaced[symbolID][(int)goRightDown.x, (int)goRightDown.y] == (Symbol)symbolID)
                    {
                        scanVertex = goRightDown;
                    }
                    else
                    {
                        traversalTerminated = true;
                    }
                    break;
                case 3:
                    if (goRightUp.x >= 0 && goRightUp.x <= 3 && goRightUp.y >= 0 && goRightUp.y <= 4 && spinnedSymbolsWithWildsReplaced[symbolID][(int)goRightUp.x, (int)goRightUp.y] == (Symbol)symbolID)
                    {
                        scanVertex = goRightUp;
                    }
                    else if (goRightDown.x >= 0 && goRightDown.x <= 3 && goRightDown.y >= 0 && goRightDown.y <= 4 && spinnedSymbolsWithWildsReplaced[symbolID][(int)goRightDown.x, (int)goRightDown.y] == (Symbol)symbolID)
                    {
                        scanVertex = goRightDown;
                    }
                    else if (goRight.x >= 0 && goRight.x <= 3 && goRight.y >= 0 && goRight.y <= 4 && spinnedSymbolsWithWildsReplaced[symbolID][(int)goRight.x, (int)goRight.y] == (Symbol)symbolID)
                    {
                        scanVertex = goRight;
                    }
                    else
                    {
                        traversalTerminated = true;
                    }
                    break;
                case 4:
                    if (goRightDown.x >= 0 && goRightDown.x <= 3 && goRightDown.y >= 0 && goRightDown.y <= 4 && spinnedSymbolsWithWildsReplaced[symbolID][(int)goRightDown.x, (int)goRightDown.y] == (Symbol)symbolID)
                    {
                        scanVertex = goRightDown;
                    }
                    else if (goRight.x >= 0 && goRight.x <= 3 && goRight.y >= 0 && goRight.y <= 4 && spinnedSymbolsWithWildsReplaced[symbolID][(int)goRight.x, (int)goRight.y] == (Symbol)symbolID)
                    {
                        scanVertex = goRight;
                    }
                    else if (goRightUp.x >= 0 && goRightUp.x <= 3 && goRightUp.y >= 0 && goRightUp.y <= 4 && spinnedSymbolsWithWildsReplaced[symbolID][(int)goRightUp.x, (int)goRightUp.y] == (Symbol)symbolID)
                    {
                        scanVertex = goRightUp;
                    }
                    else
                    {
                        traversalTerminated = true;
                    }
                    break;
                case 5:
                    if (goRightDown.x >= 0 && goRightDown.x <= 3 && goRightDown.y >= 0 && goRightDown.y <= 4 && spinnedSymbolsWithWildsReplaced[symbolID][(int)goRightDown.x, (int)goRightDown.y] == (Symbol)symbolID)
                    {
                        scanVertex = goRightDown;
                    }
                    else if (goRightUp.x >= 0 && goRightUp.x <= 3 && goRightUp.y >= 0 && goRightUp.y <= 4 && spinnedSymbolsWithWildsReplaced[symbolID][(int)goRightUp.x, (int)goRightUp.y] == (Symbol)symbolID)
                    {
                        scanVertex = goRight;
                    }
                    else if (goRightDown.x >= 0 && goRightDown.x <= 3 && goRightDown.y >= 0 && goRightDown.y <= 4 && spinnedSymbolsWithWildsReplaced[symbolID][(int)goRightDown.x, (int)goRightDown.y] == (Symbol)symbolID)
                    {
                        scanVertex = goRightDown;
                    }
                    else
                    {
                        traversalTerminated = true;
                    }
                    break;
            }

        }
        if (payoutReconstructedPaths[(Symbol)symbolID].Count < reconstructedPath.Count)
        {
            payoutReconstructedPaths[(Symbol)symbolID] = reconstructedPath;
        }
    }

    public void CalculatePayout()
    {

        Reset();

        SpinDatum spinDatum = spinGenerator.GetSpinDatum();
        List<Symbol[,]> spinnedSymbolsWithWildsReplaced = spinDatum.GetSpinnedSymbolsWithWildsReplaced();

        FindHighestPayingVertices(spinnedSymbolsWithWildsReplaced);
        

        for (int symbolID = 0; symbolID < 5; symbolID++)
        {
            Debug.Log(" " + payoutTallies[(Symbol)symbolID] + " " + (Symbol)symbolID + " with starting vertex at " + payoutStartingVertex[(Symbol)symbolID]);
            if (payoutStartingVertex[(Symbol)symbolID] == new Vector2(-1f, -1f))
            {
                continue;
            }

            for(int ruleset = 0; ruleset < 6; ruleset++)
            {
                BruteForcePathFromVertexWithRuleset(ruleset, spinnedSymbolsWithWildsReplaced, symbolID);

            }

            // TODO: Award Balance according to Tally

            gameState.SetTrigger("Calculated Payout");

    }
        }




}

