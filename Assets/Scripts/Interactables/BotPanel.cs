using UnityEngine;

public class BotPanel : Interactable
{
    [SerializeField]
    BotState botMod;

    [SerializeField]
    string outMessage;

    [SerializeField]
    SatelliteTaker planet;

    [SerializeField]
    bool TestMode = false;

    public bool IsBotActive => planet.IsSatelliteAround || TestMode;

    protected override void interact(PlayerInteract playerInteract)
    {
        PlayerUI playerUI = playerInteract.GetComponent<PlayerUI>();
        if (planet.IsSatelliteAround || TestMode)
        {
            if (botMod.IsControl)
                botMod.StopControlBot();
            else
                botMod.ControlBot(playerInteract);

            if (playerUI != null)
                playerUI.UpdateText(botMod.IsControl ? outMessage : promtMessage);
        }
    }

}