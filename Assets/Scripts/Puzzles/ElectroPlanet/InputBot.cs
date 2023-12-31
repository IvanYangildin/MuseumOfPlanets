﻿using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class WhyItWorksSoStupidOmg : UnityEvent<float>
{ }

public class InputBot : MonoBehaviour
{
    private PlayerInput botInput;
    private PlayerInput.OnFootActions onFoot;

    public PlayerInput BotInput => botInput;
    public PlayerInput.OnFootActions OnFoot => onFoot;

    [SerializeField]
    BotPanel botPanel;

    [SerializeField]
    PlayerLook botLook;
    [SerializeField]
    PlayerMotion botMotion;
    [SerializeField]
    BotLever leverHor, leverVert;

    [SerializeField]
    float botSencitivity;

    public PlayerInteract Player;

    public WhyItWorksSoStupidOmg OnVert = new ();
    public WhyItWorksSoStupidOmg OnHor = new ();

    private void Awake()
    {
        botInput = new PlayerInput();
        onFoot = botInput.OnFoot;

        onFoot.Interact.performed += ctx => botPanel.Interact(Player);
    }

    private void FixedUpdate()
    {
        Vector2 move = onFoot.Motion.ReadValue<Vector2>();
        leverHor.ProcessRot(move.x);
        leverVert.ProcessRot(move.y);

        if (Mathf.Sign(-leverVert.Angle) == Mathf.Sign(move.y))
            botMotion.ProcessMove(new Vector2(0, move.y));

        if (Mathf.Sign(-leverHor.Angle) == Mathf.Sign(move.x))
            botLook.ProcessLook(new Vector2(move.x, 0), botSencitivity);

        OnVert.Invoke(move.y);
        OnHor.Invoke(move.x);
    }

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        onFoot.Enable();
    }

    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
        onFoot.Disable();
    }
}