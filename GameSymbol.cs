using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class GameSymbol : MonoBehaviour
{
    [SerializeField]
    private Symbol symbol;
    [SerializeField]
    private Vector3 endPosition;
    [SerializeField]
    private float tweenDuration;
    private bool isFakeSymbol;

    private Tween tween;

    private GameSymbolPool gameSymbolPool;

    private void Awake()
    {
        endPosition = new Vector3(-1f, -1f, -1f);
        gameSymbolPool = GameObject.FindGameObjectWithTag("Game Symbol Pool").GetComponent<GameSymbolPool>();
    }

    private void Update()
    {
        if (tween == null && endPosition != new Vector3(-1f, -1f, -1f) && tweenDuration != 0)
        {
            tween = this.transform.DOMove(endPosition, tweenDuration).OnComplete(TryResetSymbol);
        }
    }

    public bool IsFakeSymbol()
    {
        return isFakeSymbol;
    }

    public void SetIsFakeSymbol(bool predicate)
    {
        isFakeSymbol = predicate;
    }

    private void TryResetSymbol()
    {
        if (isFakeSymbol)
        {
            tween = null;
            endPosition = new Vector3(-1f, -1f, -1f);
            MoveToPosition(new Vector3(50f, 50f, 0f));
            gameSymbolPool.AddToPool(this.symbol, this.gameObject);
        }
    }

    internal void MoveToPosition(Vector3 newPosition)
    {
        this.transform.position = newPosition;
    }

    internal void SetTweenParameters(Vector3 newEndPosition, float newTweenDuration)
    {
        endPosition = newEndPosition;
        tweenDuration = newTweenDuration;
    }

    public Symbol GetSymbol()
    {
        return symbol;
    }
}
