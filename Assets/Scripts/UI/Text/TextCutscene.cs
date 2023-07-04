using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class TextCutscene : MonoBehaviour
{
    private PlayerInput input;
    private InputAction click;

    [SerializeField]
    IPrinter printer;
    [SerializeField]
    List<string> textToPlay;
    [SerializeField]
    float pauseBeforeStart;

    int ind = 0;
    public bool IsFinished => (ind == textToPlay.Count) && printer.IsPrinted;

    public UnityEvent ClickOut = new UnityEvent();

    private void Awake()
    {
        input = new PlayerInput();
        click = input.UI.Click;

        click.performed += arg =>
        {
            if (arg.control.IsPressed())
            {
                if (IsFinished) 
                    ClickOut?.Invoke(); 
                else
                    PrintNext();
            }
        };
    }

    private void Start()
    {
        Timer timer = gameObject.AddComponent<Timer>();
        timer.TimeUp += PrintNext;
        timer.StartTimer(pauseBeforeStart);
    }

    public void PrintNext()
    {
        if (!printer.IsPrinted)
        {
            printer.StopPrint();
        }
        else if (ind < textToPlay.Count)
        {
            printer.StartPrint(textToPlay[ind]);
            ind++;
        }
    }

    private void OnEnable()
    {
        click.Enable();
    }

    private void OnDisable()
    {
        click.Disable();
    }
}
