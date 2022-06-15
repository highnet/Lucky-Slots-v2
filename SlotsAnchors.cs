using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotsAnchors : MonoBehaviour
{
    [SerializeField]
    private GameObject symbolAnchorPrefab;
    [SerializeField]
    private GameObject startAnchorPrefab;
    [SerializeField]
    private GameObject endAnchorPrefab;
    private GameObject[,] symbolAnchors;
    private GameObject[] startAnchors;
    private GameObject[] endAnchors;
    private Vector3 spawnPoint;

    private void Awake()
    {
        spawnPoint = new Vector3(-3.5f, 3f, 0f);

        symbolAnchors = new GameObject[SlotsAttributes.GetNumberOfRows(), SlotsAttributes.GetNumberOfReels()];
        startAnchors = new GameObject[SlotsAttributes.GetNumberOfReels()];
        endAnchors = new GameObject[SlotsAttributes.GetNumberOfReels()];

        for (int i = 0; i < SlotsAttributes.GetNumberOfRows(); i++)
        {
            for (int j = 0; j < SlotsAttributes.GetNumberOfReels(); j++)
            {
                GameObject spawnedSymbolAnchor = GameObject.Instantiate(symbolAnchorPrefab, spawnPoint + new Vector3(j * SlotsAttributes.GetSymbolGridHorizontalDistance(), -i * SlotsAttributes.GetSymbolGridVerticalDistance(), 0f), Quaternion.identity);
                symbolAnchors[i, j] = spawnedSymbolAnchor;

                if (i == 0)
                {
                    GameObject spanwedStartAnchor = GameObject.Instantiate(startAnchorPrefab, spawnPoint + new Vector3(j * SlotsAttributes.GetSymbolGridHorizontalDistance(), -(i-1) * SlotsAttributes.GetSymbolGridVerticalDistance(), 0f), Quaternion.identity);
                    startAnchors[j] = spanwedStartAnchor;
                }
                if (i == SlotsAttributes.GetNumberOfRows() - 1)
                {
                    GameObject spawnedEndAnchor = GameObject.Instantiate(endAnchorPrefab, spawnPoint + new Vector3(j * SlotsAttributes.GetSymbolGridHorizontalDistance(), -(i + 1) * SlotsAttributes.GetSymbolGridVerticalDistance(), 0f), Quaternion.identity);
                    endAnchors[j] = spawnedEndAnchor;
                }
            }
        }
    }

    public GameObject[,] GetSymbolAnchors()
    {
        return symbolAnchors;
    }
    public GameObject[] GetStartAnchors()
    {
        return startAnchors;
    }
    public GameObject[] GetEndAnchors()
    {
        return endAnchors;
    }
}
