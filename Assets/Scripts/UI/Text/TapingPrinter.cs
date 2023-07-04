using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class TapingPrinter : IPrinter
{
    [SerializeField]
    TextMeshProUGUI textbox;
    [SerializeField]
    float printSpeed;

    string currentText = "";
    float currenWidth = 0f;

    public override bool IsPrinted => currentText.Length == currenWidth;

    public override void StartPrint(string text)
    {
        currentText = text;
        currenWidth = 0;
    }

    public override void StopPrint()
    {
        currenWidth = currentText.Length;
    }

    private void FixedUpdate()
    {
        currenWidth += printSpeed * Time.deltaTime;
        currenWidth = Mathf.Min(currenWidth, currentText.Length);

        textbox.text = currentText.Substring(0, (int) currenWidth);
    }
}

