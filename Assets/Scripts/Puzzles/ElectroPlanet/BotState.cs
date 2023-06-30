using UnityEngine;

public class BotState : MonoBehaviour
{
    [SerializeField]
    InputBot botInput;

    [SerializeField]
    Camera computerCamera;

    public bool IsControl { private set; get; }

    public void ControlBot(PlayerInteract controller)
    {
        IsControl = true;

        controller.gameObject.SetActive(false);
        computerCamera.gameObject.SetActive(true);
        botInput.enabled = true;
        botInput.Player = controller;
    }

    public void StopControlBot()
    {
        IsControl = false;

        if (botInput.Player != null)
            botInput.Player.gameObject.SetActive(true);
        if (botInput != null)
            botInput.enabled= false;
        if (computerCamera != null)
            computerCamera.gameObject.SetActive(false);
    }
}