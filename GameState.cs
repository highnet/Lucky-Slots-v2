using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public Animator GetAnimator()
    {
        return animator;
    }
    public void SetBool(string name, bool predicate)
    {
        animator.SetBool(name, predicate);
    }

    public void SetTrigger(string triggerName)
    {
        animator.SetTrigger(triggerName);
    }

}
