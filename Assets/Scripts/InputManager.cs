using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInput.OnFootActions onFoot;

    [SerializeField]
    private PlayerMotion motion;
    [SerializeField]
    private PlayerLook look;
    [SerializeField]
    private PlayerInteract interact;
    [SerializeField]
    private PlayerHoldItem holdItem;
    [SerializeField]
    private MainMenu menu;

    void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;

        onFoot.Interact.performed += ctx => interact.OnPerformInteract();
        onFoot.Interact.canceled += ctx => interact.OnCancelInteract();
        onFoot.DropItem.performed += ctx => holdItem.Item?.ItemToDrop.Drop();
        onFoot.Escape.performed += ctx => menu.MainManu();
    }

    private void FixedUpdate()
    {
        motion.ProcessMove(onFoot.Motion.ReadValue<Vector2>());
    }

    private void LateUpdate()
    {
        look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        onFoot.Enable();
    }

    private void OnDisable()
    {
        onFoot.Disable();
    }
}
