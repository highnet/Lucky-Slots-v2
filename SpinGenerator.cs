using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinGenerator : MonoBehaviour
{
    private SpinDatum spinDatum;


    public SpinDatum GetSpinDatum()
    {
        return spinDatum;
    }

    public void GenerateSpin()
    {
        spinDatum = new SpinDatum();
    }
}
