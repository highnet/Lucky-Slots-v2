using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PayoutCalculator : MonoBehaviour
{
    private GameState gameState;
    private SpinGenerator spinGenerator;
    private PaylineSpawner paylineSpawner;

    private List<string> payoutTally;
    private void Awake()
    {
        gameState = GameObject.FindGameObjectWithTag("Game State").GetComponent<GameState>();
        spinGenerator = GameObject.FindGameObjectWithTag("Spin Generator").GetComponent<SpinGenerator>();
        paylineSpawner = GameObject.FindGameObjectWithTag("Payline Spawner").GetComponent<PaylineSpawner>();
        payoutTally = new List<string>();
    }

    public List<String> GetPayoutTally()
    {
        return payoutTally;
    }

    public void CalculatePayout()
    {
        payoutTally = new List<string>();
        SpinDatum spinDatum = spinGenerator.GetSpinDatum();
        List<Symbol[,]> spinnedSymbolsWithWildsReplaced = spinDatum.GetSpinnedSymbolsWithWildsReplaced();

        for(int i = 4; i >= 0; i--)
        {
            Symbol scanSymbol = (Symbol)i;

            for(int j = 0; j < 4; j++)
            {
                for(int k = 0; k < 3; k++)
                {
                    Debug.Log("============");
                    bool traversalTerminated = false;
                    List<Vector2> path = new List<Vector2>();
                    Vector2 scanVertex = new Vector2(j, k);
                     while (!traversalTerminated)
                    {
                        if (spinnedSymbolsWithWildsReplaced[i][(int)scanVertex.x,(int)scanVertex.y] == scanSymbol)
                        {
                            Debug.Log(scanSymbol + " Match Found, adding " + scanVertex + " to path and continuing traversal attempt");
                            path.Add(scanVertex);
                        } else
                        {
                            Debug.Log(scanSymbol + " Match not Found, terminating traversal attempt");

                            traversalTerminated = true;

                            break;
                        }

                        Vector2 goRightUp = new Vector2(scanVertex.x - 1, scanVertex.y + 1);
                        Vector2 goRight = new Vector2(scanVertex.x , scanVertex.y + 1);
                        Vector2 goRightDown = new Vector2(scanVertex.x + 1, scanVertex.y + 1);

                        if (goRight.x >= 0 && goRight.x <= 3 && goRight.y >= 0 && goRight.y <= 4 && spinnedSymbolsWithWildsReplaced[i][(int)goRight.x, (int)goRight.y] == scanSymbol){
                            scanVertex = goRight;
                        } else if (goRightUp.x >= 0 && goRightUp.x <= 3 && goRightUp.y >= 0 && goRightUp.y <= 4 && spinnedSymbolsWithWildsReplaced[i][(int)goRightUp.x, (int)goRightUp.y] == scanSymbol)
                        {
                            scanVertex = goRightUp;
                        }
                        else if (goRightDown.x >= 0 && goRightDown.x <= 3 && goRightDown.y >= 0 && goRightDown.y <= 4 && spinnedSymbolsWithWildsReplaced[i][(int)goRightDown.x, (int)goRightDown.y] == scanSymbol)
                        {
                            scanVertex = goRightDown;
                        }
                        else
                        {
                            Debug.Log("No traversal matches found, terminating traversal");
                            traversalTerminated = true;
                            break;
                        }
                    }
                    Debug.Log("============");

                }
            }
        }

    }
}
