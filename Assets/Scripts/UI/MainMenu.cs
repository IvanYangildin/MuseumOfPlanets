using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    string startScene, gameScene, menuScene, loadingScene;

    public void StartGame()
    {
        SceneLoader.LoadWithUnload(startScene, loadingScene, true);
    }

    public void ContinueGame()
    {
        SceneLoader.LoadWithUnload(gameScene, loadingScene, false);
    }

    public void ExitGame()
    {
        GlobalManager.ExitGame();
        Application.Quit();
    }

    public void MainManu()
    {
        SceneLoader.LoadWithDeactivate(menuScene, loadingScene, false);
    }
}
