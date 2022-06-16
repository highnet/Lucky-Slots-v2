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

    public void CalculatePayout()
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

        SpinDatum spinDatum = spinGenerator.GetSpinDatum();
        List<Symbol[,]> spinnedSymbolsWithWildsReplaced = spinDatum.GetSpinnedSymbolsWithWildsReplaced();

        for (int i = 4; i >= 0; i--)
        {
            scanSymbol = (Symbol)i;
            spinnedSymbols = spinnedSymbolsWithWildsReplaced[i];

            for (int j = 0; j < 4; j++)
            {
                for (int k = 0; k < 5; k++)
                {
                    int maxPathLength = 0;
                    Vector2 startVertex = new Vector2(j, k);
                    if (spinnedSymbols[(int)startVertex.x, (int)startVertex.y] == scanSymbol)
                    {
                        maxPathLength = Mathf.Max(
                            TraversePath(1, new Vector2(startVertex.x, startVertex.y + 1)),
                            TraversePath(1, new Vector2(startVertex.x - 1, startVertex.y + 1)),
                            TraversePath(1, new Vector2(startVertex.x + 1, startVertex.y + 1))
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

        for (int i = 0; i < 5; i++)
        {
            Debug.Log(" " + payoutTallies[(Symbol)i] + " " + (Symbol)i + " with starting vertex at " + payoutStartingVertex[(Symbol)i]);
            if (payoutStartingVertex[(Symbol)i] == new Vector2(-1f, -1f))
            {
                continue;
            }

            /////////////////////////////////////////////////
            Vector2 scanVertex = payoutStartingVertex[(Symbol)i];
            List<Vector2> reconstructedPath = new List<Vector2>();

            bool traversalTerminated = false;
            while (!traversalTerminated)
            {
                reconstructedPath.Add(scanVertex);

                Vector2 goRightUp = new Vector2(scanVertex.x - 1, scanVertex.y + 1);
                Vector2 goRight = new Vector2(scanVertex.x, scanVertex.y + 1);
                Vector2 goRightDown = new Vector2(scanVertex.x + 1, scanVertex.y + 1);

                if (goRight.x >= 0 && goRight.x <= 3 && goRight.y >= 0 && goRight.y <= 4 && spinnedSymbolsWithWildsReplaced[i][(int) goRight.x,(int)goRight.y] == (Symbol) i)
                {
                    scanVertex = goRight;
                } else if(goRightUp.x >= 0 && goRightUp.x <= 3 && goRightUp.y >= 0 && goRightUp.y <= 4 && spinnedSymbolsWithWildsReplaced[i][(int)goRightUp.x, (int)goRightUp.y] == (Symbol)  i)
                {
                    scanVertex = goRightUp;
                }
                else if (goRightDown.x >= 0 && goRightDown.x <= 3 && goRightDown.y >= 0 && goRightDown.y <= 4 && spinnedSymbolsWithWildsReplaced[i][(int)goRightDown.x, (int)goRightDown.y] == (Symbol) i)
                {
                    scanVertex = goRightDown;
                }
                else
                {
                    traversalTerminated = true;
                }
            }
            if (payoutReconstructedPaths[(Symbol)i].Count < reconstructedPath.Count)
            {
                payoutReconstructedPaths[(Symbol)i] = reconstructedPath;
            }
            ///////////////////////////////////////////////////////
            ///            /////////////////////////////////////////////////
            scanVertex = payoutStartingVertex[(Symbol)i];
             reconstructedPath = new List<Vector2>();

             traversalTerminated = false;
            while (!traversalTerminated)
            {
                reconstructedPath.Add(scanVertex);

                Vector2 goRightUp = new Vector2(scanVertex.x - 1, scanVertex.y + 1);
                Vector2 goRight = new Vector2(scanVertex.x, scanVertex.y + 1);
                Vector2 goRightDown = new Vector2(scanVertex.x + 1, scanVertex.y + 1);

                if (goRight.x >= 0 && goRight.x <= 3 && goRight.y >= 0 && goRight.y <= 4 && spinnedSymbolsWithWildsReplaced[i][(int)goRight.x, (int)goRight.y] == (Symbol)i)
                {
                    scanVertex = goRight;
                }
                else if (goRightDown.x >= 0 && goRightDown.x <= 3 && goRightDown.y >= 0 && goRightDown.y <= 4 && spinnedSymbolsWithWildsReplaced[i][(int)goRightDown.x, (int)goRightDown.y] == (Symbol)i)
                {
                    scanVertex = goRightDown;
                }
                else if (goRightUp.x >= 0 && goRightUp.x <= 3 && goRightUp.y >= 0 && goRightUp.y <= 4 && spinnedSymbolsWithWildsReplaced[i][(int)goRightUp.x, (int)goRightUp.y] == (Symbol)i)
                {
                    scanVertex = goRightUp;
                }
                else
                {
                    traversalTerminated = true;
                }
            }
            if (payoutReconstructedPaths[(Symbol)i].Count < reconstructedPath.Count)
            {
                payoutReconstructedPaths[(Symbol)i] = reconstructedPath;
            }
            ///////////////////////////////////////////////////////
            scanVertex = payoutStartingVertex[(Symbol)i];
            reconstructedPath = new List<Vector2>();

            traversalTerminated = false;
            while (!traversalTerminated)
            {
                reconstructedPath.Add(scanVertex);

                Vector2 goRightUp = new Vector2(scanVertex.x - 1, scanVertex.y + 1);
                Vector2 goRight = new Vector2(scanVertex.x, scanVertex.y + 1);
                Vector2 goRightDown = new Vector2(scanVertex.x + 1, scanVertex.y + 1);

                if (goRightUp.x >= 0 && goRightUp.x <= 3 && goRightUp.y >= 0 && goRightUp.y <= 4 && spinnedSymbolsWithWildsReplaced[i][(int)goRightUp.x, (int)goRightUp.y] == (Symbol)i)
                {
                    scanVertex = goRightUp;
                }
                else if (goRight.x >= 0 && goRight.x <= 3 && goRight.y >= 0 && goRight.y <= 4 && spinnedSymbolsWithWildsReplaced[i][(int)goRight.x, (int)goRight.y] == (Symbol)i)
                {
                    scanVertex = goRight;
                }
                else if (goRightDown.x >= 0 && goRightDown.x <= 3 && goRightDown.y >= 0 && goRightDown.y <= 4 && spinnedSymbolsWithWildsReplaced[i][(int)goRightDown.x, (int)goRightDown.y] == (Symbol)i)
                {
                    scanVertex = goRightDown;
                }
                else
                {
                    traversalTerminated = true;
                }
            }
            if (payoutReconstructedPaths[(Symbol)i].Count < reconstructedPath.Count)
            {
                payoutReconstructedPaths[(Symbol)i] = reconstructedPath;
            }
            ///////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////
            scanVertex = payoutStartingVertex[(Symbol)i];
            reconstructedPath = new List<Vector2>();

            traversalTerminated = false;
            while (!traversalTerminated)
            {
                reconstructedPath.Add(scanVertex);

                Vector2 goRightUp = new Vector2(scanVertex.x - 1, scanVertex.y + 1);
                Vector2 goRight = new Vector2(scanVertex.x, scanVertex.y + 1);
                Vector2 goRightDown = new Vector2(scanVertex.x + 1, scanVertex.y + 1);

                if (goRightUp.x >= 0 && goRightUp.x <= 3 && goRightUp.y >= 0 && goRightUp.y <= 4 && spinnedSymbolsWithWildsReplaced[i][(int)goRightUp.x, (int)goRightUp.y] == (Symbol)i)
                {
                    scanVertex = goRightUp;
                }
                else if (goRightDown.x >= 0 && goRightDown.x <= 3 && goRightDown.y >= 0 && goRightDown.y <= 4 && spinnedSymbolsWithWildsReplaced[i][(int)goRightDown.x, (int)goRightDown.y] == (Symbol)i)
                {
                    scanVertex = goRightDown;
                }
                else if (goRight.x >= 0 && goRight.x <= 3 && goRight.y >= 0 && goRight.y <= 4 && spinnedSymbolsWithWildsReplaced[i][(int)goRight.x, (int)goRight.y] == (Symbol)i)
                {
                    scanVertex = goRight;
                }
                else
                {
                    traversalTerminated = true;
                }
            }
            if (payoutReconstructedPaths[(Symbol)i].Count < reconstructedPath.Count)
            {
                payoutReconstructedPaths[(Symbol)i] = reconstructedPath;
            }
            ///////////////////////////////////////////////////////
            ///            ///////////////////////////////////////////////////////
            scanVertex = payoutStartingVertex[(Symbol)i];
            reconstructedPath = new List<Vector2>();

            traversalTerminated = false;
            while (!traversalTerminated)
            {
                reconstructedPath.Add(scanVertex);

                Vector2 goRightUp = new Vector2(scanVertex.x - 1, scanVertex.y + 1);
                Vector2 goRight = new Vector2(scanVertex.x, scanVertex.y + 1);
                Vector2 goRightDown = new Vector2(scanVertex.x + 1, scanVertex.y + 1);

                if (goRightDown.x >= 0 && goRightDown.x <= 3 && goRightDown.y >= 0 && goRightDown.y <= 4 && spinnedSymbolsWithWildsReplaced[i][(int)goRightDown.x, (int)goRightDown.y] == (Symbol)i)
                {
                    scanVertex = goRightDown;
                }
                else if (goRight.x >= 0 && goRight.x <= 3 && goRight.y >= 0 && goRight.y <= 4 && spinnedSymbolsWithWildsReplaced[i][(int)goRight.x, (int)goRight.y] == (Symbol)i)
                {
                    scanVertex = goRight;
                }
                else if (goRightUp.x >= 0 && goRightUp.x <= 3 && goRightUp.y >= 0 && goRightUp.y <= 4 && spinnedSymbolsWithWildsReplaced[i][(int)goRightUp.x, (int)goRightUp.y] == (Symbol)i)
                {
                    scanVertex = goRightUp;
                }
                else
                {
                    traversalTerminated = true;
                }
            }
            if (payoutReconstructedPaths[(Symbol)i].Count < reconstructedPath.Count)
            {
                payoutReconstructedPaths[(Symbol)i] = reconstructedPath;
            }
            ///////////////////////////////////////////////////////
            ///            ///            ///////////////////////////////////////////////////////
            scanVertex = payoutStartingVertex[(Symbol)i];
            reconstructedPath = new List<Vector2>();

            traversalTerminated = false;
            while (!traversalTerminated)
            {
                reconstructedPath.Add(scanVertex);

                Vector2 goRightUp = new Vector2(scanVertex.x - 1, scanVertex.y + 1);
                Vector2 goRight = new Vector2(scanVertex.x, scanVertex.y + 1);
                Vector2 goRightDown = new Vector2(scanVertex.x + 1, scanVertex.y + 1);

                if (goRightDown.x >= 0 && goRightDown.x <= 3 && goRightDown.y >= 0 && goRightDown.y <= 4 && spinnedSymbolsWithWildsReplaced[i][(int)goRightDown.x, (int)goRightDown.y] == (Symbol)i)
                {
                    scanVertex = goRightDown;
                }
                else if (goRightUp.x >= 0 && goRightUp.x <= 3 && goRightUp.y >= 0 && goRightUp.y <= 4 && spinnedSymbolsWithWildsReplaced[i][(int)goRightUp.x, (int)goRightUp.y] == (Symbol)i)
                {
                    scanVertex = goRight;
                }
                else if (goRightDown.x >= 0 && goRightDown.x <= 3 && goRightDown.y >= 0 && goRightDown.y <= 4 && spinnedSymbolsWithWildsReplaced[i][(int)goRightDown.x, (int)goRightDown.y] == (Symbol)i)
                {
                    scanVertex = goRightDown;
                }
                else
                {
                    traversalTerminated = true;
                }
            }
            if (payoutReconstructedPaths[(Symbol)i].Count < reconstructedPath.Count)
            {
                payoutReconstructedPaths[(Symbol)i] = reconstructedPath;
            }
            ///////////////////////////////////////////////////////

            Debug.Log("Reconstructed Path");
            for(int j = 0; j < payoutReconstructedPaths[(Symbol)i].Count; j++)
            {
                Debug.Log(payoutReconstructedPaths[(Symbol)i][j]);
            }
;        }

        // TODO: Award Balance according to Tally

        gameState.SetTrigger("Calculated Payout");

    }

    private int TraversePath(int currentPathLength, Vector2 currentVertex)
    {
        if (currentVertex.x < 0 || currentVertex.x > 3 || currentVertex.y < 0 || currentVertex.y > 4 || spinnedSymbols[(int)currentVertex.x, (int)currentVertex.y] != scanSymbol)
        {
            return currentPathLength;
        }

        return Mathf.Max(
            TraversePath(currentPathLength + 1, new Vector2(currentVertex.x, currentVertex.y + 1)),
            TraversePath(currentPathLength + 1, new Vector2(currentVertex.x - 1, currentVertex.y + 1)),
            TraversePath(currentPathLength + 1, new Vector2(currentVertex.x + 1, currentVertex.y + 1)));
    }


}

