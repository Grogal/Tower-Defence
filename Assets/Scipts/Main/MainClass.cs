using UnityEngine;
using System.Collections;

public enum GameState
{
    Play,
    Pause,
    Lose,
    Win
}

public class MainClass : MonoSingleton<MainClass>
{
    public GameState gameState;
    public int gameSpeed;
    public GameObject loseLevelGUI;
    public GameObject winLevelGUI;
    public GameObject mainGUI;
    // Use this for initialization
    void Start()
    {
        loseLevelGUI.SetActive(false);
        winLevelGUI.SetActive(false);
        gameSpeed = 1;
        gameState = GameState.Play;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState == GameState.Play)
        {
            Time.timeScale = gameSpeed;
        }
        else if (gameState == GameState.Pause)
        {
            Time.timeScale = 0;
        }
        else if (gameState == GameState.Win)
        {
            mainGUI.SetActive(false);
            winLevelGUI.SetActive(true);
            Time.timeScale = 0;
        }
        else if (gameState == GameState.Lose)
        {
            mainGUI.SetActive(false);
            loseLevelGUI.SetActive(true);
            Time.timeScale = 0;
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !BuildingControl.instance.buildTower && gameState != GameState.Win && gameState != GameState.Lose)
        {
            if (gameState == GameState.Pause)
                gameState = GameState.Play;
            else
                gameState = GameState.Pause;
        }
    }

    public void ReloadLevel()
    {
        Application.LoadLevel(Application.loadedLevelName);
    }

    public void ExitToMainMenu()
    {
        Application.LoadLevel("Level Manager");
    }
}
