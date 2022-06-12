using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetAwaiter : MonoBehaviour
{
    public bool GetSpinInput()
    {
        bool input = Input.GetKeyUp(KeyCode.Return);
        return input;
    }
}
