using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class IPrinter : MonoBehaviour
{
    public abstract void StartPrint(string text);
    public abstract void StopPrint();

    public abstract bool IsPrinted { get; }
}
