using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    public void LeaveGame()
    {
        Application.Quit();
    }

    public void GoMainGame()
    {
        if(Application.isMobilePlatform)SceneManager.LoadScene("MainMobile");
        else SceneManager.LoadScene("Main");
        
        GameManager.Instance.ChangeGameState(GameState.MainMenu);
    }

    public void GoPlayGame()
    {
        if(Application.isMobilePlatform)SceneManager.LoadScene("InPlayMobile");
        else SceneManager.LoadScene("InPlay");

        GameManager.Instance.ChangeGameState(GameState.InPlay);
    }

    public void ContinuePlayGame()
    {
        GameManager.Instance.ChangeGameState(GameState.InPlay);
    }
}
