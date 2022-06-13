using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SlotsAttributes
{
    private const int numberOfRows = 4;
    private const int numberOfReels = 5;
    private const int numberOfSymbols = 7;
    private const float symbolGridVerticalDistance = 2.0f;
    private const float symbolGridHorizontalDistance = 2.8f;

    public static int GetNumberOfReels()
    {
        return numberOfReels;
    }
    public static int GetNumberOfRows()
    {
        return numberOfRows;
    }
    public static int GetNumberOfSymbols()
    {
        return numberOfSymbols;
    }
    public static float GetSymbolGridVerticalDistance()
    {
        return symbolGridVerticalDistance;
    }
    public static float GetSymbolGridHorizontalDistance()
    {
        return symbolGridHorizontalDistance;
    }
}
