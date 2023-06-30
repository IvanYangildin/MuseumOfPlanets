using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    string startScene, menuScene;

    public void StartGame()
    {
        SceneLoader.LoadScene(startScene);
    }

    public void ContinueGame()
    {
        SceneLoader.TransitToScene(startScene);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void MainManu()
    {
        SceneLoader.TransitToScene(menuScene);
    }
}
