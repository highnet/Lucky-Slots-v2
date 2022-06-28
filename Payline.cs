using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Payline : MonoBehaviour
{
    [SerializeField]
    private List<Vector2> path;
    [SerializeField]
    private LineRenderer lineRenderer;
    [SerializeField]
    private Symbol paylineSymbol;
    private SlotsAnchors slotsAnchors;
    private Renderer renderer;
    private void Awake()
    {
        slotsAnchors = GameObject.FindGameObjectWithTag("Slots Anchors").GetComponent<SlotsAnchors>();
        lineRenderer = GetComponent<LineRenderer>();
        path = new List<Vector2>();
        renderer = GetComponent<Renderer>();
    }

    public void SetPaylineSymbol(Symbol newPaylineSymbol)
    {
        paylineSymbol = newPaylineSymbol;
    }

    public Symbol GetPaylineSymbol()
    {
        return paylineSymbol;
    }

    public List<Vector2> GetPath()
    {
        return path;
    }

    public void SetPath(List<Vector2> newPath)
    {
        path = newPath;
    }

    public void RenderPayline()
    {
        lineRenderer.positionCount = path.Count;
        for(int i = 0; i < path.Count; i++)
        {
            lineRenderer.SetPosition(i, slotsAnchors.GetSymbolAnchors()[(int)path[i].x, (int)path[i].y].transform.position);
        }

        if (path.Count == 5)
        {
            renderer.material.color = Color.red;
        } else if (path.Count == 4)
        {
            renderer.material.color = Color.green;
        }
        else if (path.Count == 3)
        {
            renderer.material.color = Color.blue;

        }
    }

    public void ResetPayline()
    {
        path = new List<Vector2>();
        lineRenderer.positionCount = 0;
    }


}
