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
    private Vector3 resetPosition;
    [SerializeField]
    private float tweenDuration;
    [SerializeField]
    private bool isFakeSymbol;
    [SerializeField]
    private Tween tween;
    [SerializeField]
    private bool forceTween;

    private GameSymbolPool gameSymbolPool;
    private ActiveSymbols activeSymbols;

    private void Awake()
    {
        endPosition = new Vector3(-1f, -1f, -1f);
        tweenDuration = 0f;
        forceTween = false;
        gameSymbolPool = GameObject.FindGameObjectWithTag("Game Symbol Pool").GetComponent<GameSymbolPool>();
        activeSymbols = GameObject.FindGameObjectWithTag("Active Symbols").GetComponent<ActiveSymbols>();

    }

    internal Vector3 GetResetPosition()
    {
        return resetPosition;
    }

    public Tween GetTween()
    {
        return tween;
    }

    public void SetForceTween(bool predicate)
    {
        forceTween = predicate;
    }

    public void SetTweenDuration(float newTweenDuration)
    {
        tweenDuration = newTweenDuration;
    }

    private void Update()
    {
        if (tween == null && forceTween)
        {
            tween = this.transform.DOMove(endPosition, tweenDuration).OnComplete(TryResetSymbol);
        }
    }

    public Vector3 GetEndPosition()
    {
        return endPosition;
    }

    public bool IsFakeSymbol()
    {
        return isFakeSymbol;
    }

    public void SetIsFakeSymbol(bool predicate)
    {
        isFakeSymbol = predicate;
    }

    public void TryResetSymbol()
    {
        if (isFakeSymbol)
        {
            MoveToPosition(new Vector3(50f, 50f, 0f));
            gameSymbolPool.AddToPool(this.symbol, this.gameObject);
            endPosition = new Vector3(-1f, -1f, -1f);
            tweenDuration = 0f;
            isFakeSymbol = false;

        }
        forceTween = false;
        tween = null;
    }

    public void SetResetPosition(Vector3 newResetPosition)
    {
        resetPosition = newResetPosition;
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
