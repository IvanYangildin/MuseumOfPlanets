using System;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    [SerializeField]
    string startScene, gameScene, menuScene, loadingScene;
    public string StartScene => startScene;
    public string GameScene => gameScene;
    public string MenuScene => menuScene;
    public string LoadingScene => loadingScene;
    
    public event Action OnExit;

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
        if (OnExit != null) OnExit();
        DataManager.SaveData();
        Application.Quit();
    }

    public void MainManu()
    {
        SceneLoader.LoadWithDeactivate(menuScene, loadingScene, false);
    }
}
