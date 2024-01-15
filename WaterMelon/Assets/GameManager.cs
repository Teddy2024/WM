using UnityEngine;
using System;
// using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public GameState State{ get; private set; }
    public static event Action<GameState> OnGameStateChange;
    public bool GameIsPaused { get; private set; }
    PlayerController _player;

    private void Start() 
    {
        HandleMainMenu();
        ChangeGameState(GameState.MainMenu);
    }

    private void Update() 
    {
        Time.timeScale = GameIsPaused ? 0 : 1;
        DealEsc();
    }

    public PlayerController GetPlayer()
    {
        if (_player == null)
        {
            _player = FindObjectOfType<PlayerController>();
            if (_player == null)
            {
                Debug.LogError("Unable to find Player");
            }
        }
        return _player;
    }

    public void ChangeGameState(GameState newState)
    {
        if(State == newState)return;
        State = newState;
        switch (newState)
        {
            case GameState.MainMenu:
            HandleMainMenu();
            break;
            case GameState.InPlay:
            HandleInPlay();
            break;
            case GameState.ExitMenu:
            HandleExitMenu();
            break;
            case GameState.Lose:
            HandleLose();
            break;
        }
        OnGameStateChange?.Invoke(newState);
    }

    #region 各狀態處理
    private void HandleMainMenu()
    {
        GameIsPaused = false;
        AudioManager.Instance.PlayMainBGM();
    }
    private void HandleInPlay()
    {
        GameIsPaused = false;
    }
    private void HandleExitMenu()
    {
        GameIsPaused = true;
    }
    private void HandleLose()
    {
        GameIsPaused = true;
        UiManager.Instance.ChangePanelAlpha(0, 1);
    }
    #endregion

    void DealEsc()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(State == GameState.MainMenu)
            {
                // GameObject.Find("SettingButton").GetComponent<Button>().onClick?.Invoke();
                Application.Quit();
            }
            else if(State == GameState.InPlay)
            {
                ChangeGameState(GameState.ExitMenu);
            }
            else if(State == GameState.ExitMenu)
            {
                ChangeGameState(GameState.InPlay);
            }
        }
    }
}

[Serializable] public enum GameState
{
    MainMenu,
    InPlay,
    ExitMenu,
    Lose
}
