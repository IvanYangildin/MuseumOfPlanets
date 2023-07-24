using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    Button continueButton, newgameButton, settingsButton, exitButton,
        backButton;
    [SerializeField]
    GameObject settingsObject;
    [SerializeField]
    GameControl gameControl;


    private void Awake()
    {
        OpenMain();
    }

    public void OpenMain()
    {
        settingsObject.gameObject.SetActive(false);
        settingsButton.gameObject.SetActive(true);
        backButton.gameObject.SetActive(false);

        continueButton.interactable = SceneLoader.IsSceneLoaded(gameControl.GameScene);
        newgameButton.interactable = true;
        settingsButton.interactable = true;
        exitButton.interactable = true;
    }

    public void OpenSettings()
    {
        settingsObject.gameObject.SetActive(true);
        settingsButton.gameObject.SetActive(false);
        backButton.gameObject.SetActive(true);

        continueButton.interactable = false;
        newgameButton.interactable = false;
        settingsButton.interactable = false;
        exitButton.interactable = false;
    }
}